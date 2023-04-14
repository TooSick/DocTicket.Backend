using DocTicket.Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocTicket.Backend.DAL.EF.EntitiesConfig
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            throw new NotImplementedException();
        }
    }
}
