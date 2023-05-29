using AutoMapper;
using DocTicket.Backend.EF;
using DocTicket.Backend.Models;
using DocTicket.Backend.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Security.Claims;

namespace DocTicket.Backend.Services
{
    public class TicketService
    {
        private readonly DocTicketDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;


        public TicketService(DocTicketDBContext context, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }


        public async Task<OrderTicketViewModel> OrderTicket(int ticketId)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Doctor).ThenInclude(d => d.Department).ThenInclude(d => d.Polyclinic)
                .Include(t => t.AppUser)
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
            ticket.AppUserId = userId;

            await _context.SaveChangesAsync();

            return _mapper.Map<Ticket, OrderTicketViewModel>(ticket);
        }

        public async Task<IEnumerable<OrderTicketViewModel>> List()
        {
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
            var tickets = await _context.Tickets.Include(t => t.Doctor).Where(t => t.AppUserId == userId).ToListAsync();

            return _mapper.Map<IEnumerable<Ticket>, IEnumerable<OrderTicketViewModel>>(tickets);
        }

        public async Task CancelTicket(int ticketId)
        {
            var ticket = await _context.Tickets.Include(t => t.Offers).FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket.Offers.Any())
                _context.Offers.RemoveRange(ticket.Offers);
            
            ticket.AppUserId = null;
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderTicketViewModel>> Exchange(int anotherUserTicketId)
        {
            var anotherUserTicket = await _context.Tickets
                .Include(t => t.Doctor)
                .FirstOrDefaultAsync(t => t.Id == anotherUserTicketId);
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

            CultureInfo cultureInfo = new CultureInfo("ru-Ru");
            var currentUserTickets = await _context.Tickets
                .Include(t => t.Doctor).Where(t => t.AppUserId == userId).ToListAsync();
            var currentUserTicket = currentUserTickets
                .FirstOrDefault(t => t.DoctorId == anotherUserTicket.DoctorId
                    && t.ReceptionTime.ToString("M", cultureInfo) == anotherUserTicket.ReceptionTime.ToString("M", cultureInfo));

            return _mapper.Map<List<Ticket>, List<OrderTicketViewModel>>(new List<Ticket>
            {
                currentUserTicket,
                anotherUserTicket,
            });
        }

        //private void SendTicketByEmail(Ticket ticket)
        //{
        //    var pdfFile = GeneratePDFTicket(ticket);
        //}

        //private byte[] GeneratePDFTicket(Ticket ticket)
        //{
        //    var pdfDocument = new iTextSharp.text.Document();
        //    var memoryStream = new MemoryStream();
        //    var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, memoryStream);

        //    pdfDocument.Open();

        //    pdfDocument.Add(new iTextSharp.text.Paragraph($"Медицинское учреждение: {ticket.Doctor.Department.Polyclinic.Title}"));
        //    pdfDocument.Add(new iTextSharp.text.Paragraph($"Врач: {ticket.Doctor.LastName} {ticket.Doctor.FirstName} {ticket.Doctor.Patronymic}"));
        //    pdfDocument.Add(new iTextSharp.text.Paragraph($"Время приема: {ticket.ReceptionTime.ToString("dd MMMM HH:mm", new CultureInfo("ru-Ru"))}"));

        //    pdfDocument.Close();

        //    var pdfBytes = memoryStream.ToArray();

        //    return pdfBytes;
        //}

        //private void SendEmailWithAttachment(string recipientEmail, string subject, string body, byte[] attachmentData, string attachmentFileName)
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Отправитель", "sender@example.com"));
        //    message.To.Add(new MailboxAddress("Получатель", recipientEmail));
        //    message.Subject = subject;

        //    var builder = new BodyBuilder();
        //    builder.TextBody = body;

        //    // Добавление вложения PDF файла
        //    var attachment = builder.Attachments.Add(attachmentFileName, new MemoryStream(attachmentData));
        //    attachment.ContentType.MediaType = MimeTypes.Application.Pdf;

        //    message.Body = builder.ToMessageBody();

        //    using (var client = new SmtpClient())
        //    {
        //        // Настройка параметров SMTP-сервера
        //        client.Connect("smtp.example.com", 587, SecureSocketOptions.StartTls);
        //        client.Authenticate("yourusername", "yourpassword");

        //        // Отправка письма
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}
    }
}
