using Microsoft.AspNetCore.Authorization;
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

    [HttpPatch]
    [Authorize(Policy = "UpdateDefect")]
    public async Task<ActionResult<DefectDto>> UpdateAsync(Guid id, UpdateDefectDto defectDto)
    {
        try
        {
            var defect = await _defectServices.UpdateDefect(id, defectDto);
            return Ok(new
            {
                messege = "Успешно добавлен",
                UpDef = defect
            });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    
    [HttpDelete("{id}")] 
    public async Task<IActionResult> DeleteDefect(Guid id) 
    {
        var deleted = await _defectServices.DeleteDefectAsync(id);
        if (!deleted)
        {
            return NotFound($"Не найден");
        }
        return NoContent(); 
    }
}