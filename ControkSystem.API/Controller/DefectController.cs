using Microsoft.AspNetCore.Mvc;

namespace ControkSystem.API.Controller;
using ControkSystem.Application.DTOs;
using ControkSystem.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class DefectController : ControllerBase
{
    private readonly DefectServices _defectServices;

    public DefectController(DefectServices defectServices)
    {
        _defectServices = defectServices;
    }

    [HttpPost]
    public async Task<ActionResult<DefectDto>> CreateAsync(CreateDefectDto defectDto)
    {
        var defect = await _defectServices.CreateDefect(defectDto);
        return Ok(defect);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<DefectDto>>> GetDefectByIdAsync(Guid id)
    {
        var defect = await _defectServices.GetDefectById(id);
        if (defect == null)
        {
            return NotFound();
        }
        return Ok(defect);
    }

    [HttpDelete]
    public async Task<ActionResult<List<DefectDto>>> DeleteAsync(Guid id)
    {
        var defect = await _defectServices.DeleteDefectAsync(id);
        if (!defect)
        {
            return NotFound();
        }
        return NoContent();
    }
}