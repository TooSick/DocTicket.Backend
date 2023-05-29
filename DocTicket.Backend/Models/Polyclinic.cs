namespace DocTicket.Backend.Models
{
    public class Polyclinic : EntityWithPhoto
    {
        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? GeneralInformation { get; set; }


        public virtual List<Department>? Departments { get; set; }

        public virtual List<RegistryNumber>? RegistryNumbers { get; set; }

        public virtual List<WorkingHour>? WorkingHours { get; set; }
    }
}
