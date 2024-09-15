using InternTracker.Data;
using InternTracker.Models.DTO;
using InternTracker.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public InternsController(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllInterns()
        {
            // Veritabanından tüm stajyerleri çekin
            var allInterns = dbContext.Interns.ToList();

            // Her bir Intern nesnesini InternListDTO nesnesine dönüştürün
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
        [HttpPost]
        public IActionResult AddIntern2([FromBody] InternDTO internDTO)
        {
            if (internDTO == null)
            {
                return BadRequest("Intern data is null.");
            }

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


    }
}
