using InternTracker.Models.Entities;

namespace InternTracker.Models.DTO
{
    public class InternListDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string InternStartDateStr { get; set; }
        public string InternEndDateStr { get; set; }
        public int TotalInternshipDay { get; set; }
        public string SchoolTypeStr { get; set; }


        public InternListDTO(Intern intern)
        {
            if (intern == null)
            {
                throw new ArgumentNullException(nameof(intern));
            }
            this.FullName=intern.FirstName+" "+intern.LastName;
            this.TotalInternshipDay = (intern.InternEndDate - intern.InternStartDate).Days;
            this.Email=intern.Email;
            this.Phone=intern.Phone;
            this.SchoolTypeStr = intern.SchoolType.ToString();
            this.Id = intern.Id;
            this.InternStartDateStr = intern.InternStartDate.ToString("yyyy-MM-dd");
            this.InternEndDateStr = intern.InternEndDate.ToString("yyyy-MM-dd");
        }
    }
}
