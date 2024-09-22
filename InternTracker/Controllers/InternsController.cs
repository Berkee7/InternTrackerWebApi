using InternTracker.Data;
using InternTracker.Models.DTO;
using InternTracker.Models.Entities;
using InternTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace InternTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly HolidayService _holidayService;

        public InternsController(ApplicationDbContext dbContext, HolidayService holidayService )
        {
            this.dbContext = dbContext;
            _holidayService = holidayService;
        }
        [HttpGet]
        public IActionResult GetAllInterns()
        {
            // Veritabanından tüm stajyerleri çekin
            var allInterns = dbContext.Interns.ToList();

            // Her bir Intern nesnesini InternListDTO nesnesine dönüştürdüm

            var allInternsDTO = allInterns.Select(intern => new InternListDTO(intern)).ToList();

            return Ok(allInternsDTO);
        }



        /* [HttpGet] NORMAL GET METODU
         public IActionResult GetAllInterns()
         {
            var allInterns= dbContext.Interns.ToList();
             return Ok(allInterns);
         }*/

        [HttpGet]
        [Route("{Id:guid}")]
        public IActionResult GetInternsById(Guid Id) {

            var Intern = dbContext.Interns.Find(Id);
            if (Intern == null)
            {

                return NotFound();
            }
            return Ok(Intern);
        }

        /*[HttpPost]
        public IActionResult AddIntern(InternDTO ınternDTO)
        {
            var InternEntity = new Intern()
            {
                FirstName = ınternDTO.FirstName,
                LastName = ınternDTO.LastName,
                Email = ınternDTO.Email,
                Phone = ınternDTO.Phone,
                InternStartDate = ınternDTO.InternStartDate,
                InternEndDate = ınternDTO.InternEndDate,
                SchoolName = ınternDTO.SchoolName,
                AcademicMajor = ınternDTO.AcademicMajor,
                BirthDate = ınternDTO.BirthDate,
                SchoolType=ınternDTO.SchoolTypeStr
            };
            dbContext.Interns.Add(InternEntity);
            dbContext.SaveChanges();
            return Ok(InternEntity);
        }*/

        [HttpPost]
        public async Task<IActionResult> AddIntern2([FromBody] InternDTO internDTO)
        {
            if (internDTO == null)
            {
                return BadRequest("Intern data is null.");
            }


            // Yeni stajyerin başlangıç ve bitiş tarihlerini al
            DateTime newInternStartDate = internDTO.InternStartDate;
            DateTime newInternEndDate = internDTO.InternEndDate;


            // Çakışan stajyerleri kontrol et
            var conflictingInterns = await dbContext.Interns.Where(i => !(newInternStartDate >= i.InternEndDate || newInternEndDate <= i.InternStartDate)).ToListAsync();

            if (conflictingInterns.Any())
            {
                return Conflict("Staj Dönemleri çakışıyor. Lütfen başka aralıklar seçiniz!");
            }

            List<DateTime> internHolidayDates = await _holidayService.GetPublicHolidaysAsync();

            newInternEndDate = SetAgain(newInternStartDate, newInternEndDate, internHolidayDates);
            internDTO.InternEndDate = newInternEndDate;

            var intern = new Intern
            {
                FirstName = internDTO.FirstName,
                LastName = internDTO.LastName,
                Email = internDTO.Email,
                Phone = internDTO.Phone,
                BirthDate = internDTO.BirthDate,
                InternStartDate = internDTO.InternStartDate,
                InternEndDate = internDTO.InternEndDate,
                SchoolType = internDTO.SchoolTypeStr,
                SchoolName = internDTO.SchoolName,
                AcademicMajor = internDTO.AcademicMajor
            };



            dbContext.Interns.Add(intern);
            dbContext.SaveChanges();

            return Ok(intern);
        }

        public static DateTime SetAgain(DateTime startDate, DateTime endDate, List<DateTime> holidays)
        {
            DateTime adjustedEndDate = endDate;
            var counter = 0;
            DateTime currentDate = startDate.AddDays(1);

            while (currentDate <= adjustedEndDate)
            {
                
                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(currentDate.Date))
                {
                    
                    adjustedEndDate = adjustedEndDate.AddDays(1);
                }
                if (currentDate.DayOfWeek==DayOfWeek.Tuesday || currentDate.DayOfWeek==DayOfWeek.Friday)
                {
                    counter++;

                }
                
                currentDate = currentDate.AddDays(1);
            }

            Console.WriteLine("HomeOffice Gün Sayısı: "+ counter);
            return adjustedEndDate;
        }




        [HttpPut]
        [Route("{Id:guid}")]
        public IActionResult UpdateInterns(Guid Id,UpdateInternDTO updateInternDTO)
        {
            var Intern = dbContext.Interns.Find(Id);
            if (Intern is null)
            {
                return NotFound();
            }
            Intern.FirstName = updateInternDTO.FirstName;
            Intern.LastName= updateInternDTO.LastName;
            Intern.SchoolName= updateInternDTO.SchoolName;
            Intern.InternStartDate= updateInternDTO.InternStartDate;
            Intern.InternEndDate= updateInternDTO.InternEndDate;
            Intern.Email= updateInternDTO.Email;
            Intern.BirthDate= updateInternDTO.BirthDate;
            Intern.AcademicMajor= updateInternDTO.AcademicMajor;
            Intern.SchoolName = updateInternDTO.SchoolName;
            Intern.SchoolType = updateInternDTO.SchoolTypeStr;
            dbContext.SaveChanges();
            return Ok(Intern);
        }
        [HttpDelete]
        [Route("{Id:guid}")]
        public IActionResult DeleteInterns(Guid Id) {
            var Intern=dbContext.Interns.Find(Id);
            if(Intern is null)
            {

                return NotFound();
            }
            dbContext.Interns.Remove(Intern);
            dbContext.SaveChanges();
            return Ok(Intern);
        }
    }
}
