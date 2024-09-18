using InternTracker.Models.Entities;
using InternTracker.Models.Enums;

namespace InternTracker.Models.DTO
{
    public class InternDTO
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; } 
        public DateTime BirthDate { get; set; } 
        public DateTime InternStartDate { get; set; } 
        public DateTime InternEndDate { get; set; } 
        public SchoolTypeEnum SchoolTypeStr { get; set; } 
        public string SchoolName { get; set; } 
        public string AcademicMajor { get; set; }


    }
}
