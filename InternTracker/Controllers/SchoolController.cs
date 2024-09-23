using Microsoft.AspNetCore.Mvc;
using InternTracker.Services;
using System.Collections.Generic;

[Route("api/school")] // SchoolController.cs in school kısmını alır her zaman.
[ApiController]
public class SchoolController : ControllerBase
{
    private readonly SchoolService _schoolService;

    public SchoolController(SchoolService schoolService)
    {
        _schoolService = schoolService; // Servis örneğini oluştur
    }

    [HttpGet]
    public IActionResult GetSchools()
    {
        List<string> schools = _schoolService.GetSchools(); // Okul listesini al
        return Ok(schools);
    }
}
