using System.Collections.Generic;

namespace InternTracker.Services
{
    public class SchoolService
    {
        public List<string> GetSchools()
        {
            return new List<string>
            {
                "Ankara Üniversitesi",
                "İstanbul Teknik Üniversitesi",
                "Boğaziçi Üniversitesi",
                "Ege Üniversitesi",
                "Hacettepe Üniversitesi",
                "Bülent Ecevit Üniversitesi",
                "Orta Doğu Teknik Üniversitesi",
                "Dokuz Eylüll Üniversitesi",
                "Celal Bayar Üniversitesi"
            };
        }
    }
}
