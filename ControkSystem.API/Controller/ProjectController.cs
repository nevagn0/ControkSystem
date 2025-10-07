using Microsoft.AspNetCore.Mvc;
using ControkSystem.Application.DTOs;
using ControkSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace ControkSystem.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectServices _projectServices;

    public ProjectController(ProjectServices projectServices)
    {
        _projectServices = projectServices;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(ProjectCreateDto dto)
    {
        var project = await _projectServices.CreateProjectAsync(dto);
        return Ok(project);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<ProjectDto>>> GetProjects(Guid id)
    {
        var project = await _projectServices.GetByIdASync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        var project = await _projectServices.DeleteProjectAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "AddUsersToProject")] 
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] Updateproject updateDto)
    {
        try
        {
            var result = await _projectServices.UpdateASync(id, updateDto);
            return Ok(new
            {
                message = "Проект успешно обновлен",
                project = result
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
}