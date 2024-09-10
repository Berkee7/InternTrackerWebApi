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
        public int SchoolTypeStr { get; set; } 
    }
}
