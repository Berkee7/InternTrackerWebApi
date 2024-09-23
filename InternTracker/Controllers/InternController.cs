using InternTracker.Data;
using InternTracker.Models.DTO;
using InternTracker.Models.Entities;
using InternTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly HolidayService _holidayService;
        private readonly InternService _internService;

        public InternController(ApplicationDbContext dbContext, HolidayService holidayService, InternService internService)
        {
            this.dbContext = dbContext;
            _holidayService = holidayService;
            _internService = internService;
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

            // SetAgain fonksiyonu artık InternService üzerinden çağrılıyor
            newInternEndDate = _internService.SetAgain(newInternStartDate, newInternEndDate, internHolidayDates);
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
