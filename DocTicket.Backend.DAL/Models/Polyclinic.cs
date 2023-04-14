namespace DocTicket.Backend.DAL.Models
{
    public class Polyclinic : EntityWithPhoto
    {
        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? GeneralInformation { get; set; }


        public virtual List<Doctor> Doctors { get; set; } = new();

        public virtual List<Department> Departments { get; set; } = new();

        public virtual List<RegistryNumber> RegistryNumbers { get; set; } = new();

        public virtual List<WorkingHour> WorkingHours { get; set; } = new();
    }
}
