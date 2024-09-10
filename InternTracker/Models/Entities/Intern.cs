using InternTracker.Models.Enums;

namespace InternTracker.Models.Entities

{
    public class Intern
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; } 
        public DateTime BirthDate { get; set; } 
        public DateTime InternStartDate { get; set; } 
        public DateTime InternEndDate { get; set; } 
        public SchoolTypeEnum SchoolType { get; set; } 
        public string SchoolName { get; set; } 
        public string AcademicMajor { get; set; } 
    }
}
