using Microsoft.AspNetCore.Mvc;
using ControkSystem.Application.Services;
using ControkSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ControkSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProjectsController : ControllerBase
    {
        private readonly UserProjectService _userProjectService;

        public UserProjectsController(UserProjectService userProjectService)
        {
            _userProjectService = userProjectService;
        }

        [HttpPost("add-user")]
        [Authorize(Policy = "AddUsersToProject")] 
        public async Task<IActionResult> AddUserToProject([FromBody] AddUserToProjectRequest request)
        {
            try
            {
                var result = await _userProjectService.AddUserToProjectAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("remove-user")]
        public async Task<IActionResult> RemoveUserFromProject([FromBody] RemoveUserFromProjectRequest request)
        {
            try
            {
                await _userProjectService.RemoveUserFromProjectAsync(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("user/{userId}/projects")]
        public async Task<IActionResult> GetUserProjects(Guid userId)
        {
            try
            {
                var result = await _userProjectService.GetUserProjectsAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("project/{projectId}/users")]
        public async Task<IActionResult> GetProjectUsers(Guid projectId)
        {
            try
            {
                var result = await _userProjectService.GetProjectUsersAsync(projectId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}