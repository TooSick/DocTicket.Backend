using DocTicket.Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocTicket.Backend.DAL.EF.EntitiesConfig
{
    public class PolyclinicConfig : BaseEntityConfig<Polyclinic>
    {
        public override void Configure(EntityTypeBuilder<Polyclinic> builder)
        {
            //builder.Property(p => p.Title).IsRequired();

            //builder.Property(p => p.Title).IsRequired();

            //builder.Property(p => p.Address).IsRequired();

            //builder.Property(p => p.WorkingHours).IsRequired();
            
            //builder.HasMany(p => p.Doctors)
            //    .WithOne(d => d.Polyclinic)
            //    .HasForeignKey(fk => fk.PolyclinicId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
