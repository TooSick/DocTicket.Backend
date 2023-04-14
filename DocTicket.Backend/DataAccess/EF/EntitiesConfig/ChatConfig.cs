using DocTicket.Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocTicket.Backend.DAL.EF.EntitiesConfig
{
    public class ChatConfig : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            throw new NotImplementedException();
        }
    }
}
