﻿namespace DocTicket.Backend.Models
{
    public class Doctor : EntityWithPhoto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public string Specialization { get; set; } = null!;


        public virtual List<Ticket> Tickets { get; set; } = new();

        //public virtual List<AppointmentTime> AppointmentTimes { get; set; } = new();

        public virtual int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;
    }
}
