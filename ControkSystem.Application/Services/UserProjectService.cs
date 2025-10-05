using ControkSystem.Domain.Model;
using ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Application.DTOs;

namespace ControkSystem.Application.Services;

public class UserProjectService
{
    private readonly IUserProjectRepository _userProjectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProjectrepository _projectRepository;

    public UserProjectService(
        IUserProjectRepository userProjectRepository,
        IUserRepository userRepository,
        IProjectrepository projectRepository)
    {
        _userProjectRepository = userProjectRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
    }

    public async Task<UserProjectResponse> AddUserToProjectAsync(AddUserToProjectRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (user == null)
            throw new ArgumentException($"Пользователь с {request.UserId} не найден");

        if (project == null)
            throw new ArgumentException($"Проект с  {request.ProjectId} не найден");

        if (await _userProjectRepository.ExistsAsync(request.UserId, request.ProjectId))
            throw new InvalidOperationException("Пользователь уже добавлен в проект");

        var userProject = await _userProjectRepository.CreateUserProjectAsync(request.UserId, request.ProjectId);

        return new UserProjectResponse
        {
            UserId = userProject.IdUser,
            ProjectId = userProject.IdProject,
            CreatedAt = DateTime.UtcNow
        };
    }

    public async Task RemoveUserFromProjectAsync(RemoveUserFromProjectRequest request)
    {
        await _userProjectRepository.DeleteUserProjectAsync(request.UserId, request.ProjectId);
    }

    public async Task<UserProjectsResponse> GetUserProjectsAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new ArgumentException($"Пользователь {userId} не найден");

        var userProjects = await _userProjectRepository.GetByUserIdAsync(userId);
        
        var projectIds = userProjects.Select(up => up.IdProject).ToList();
        
        var projectTasks = projectIds.Select(projectId => _projectRepository.GetByIdAsync(projectId));
        var projectsArray = await Task.WhenAll(projectTasks);
        var projects = projectsArray.Where(p => p != null).ToList();

        return new UserProjectsResponse
        {
            UserId = user.Id,
            Projects = projects.Select(p => new ProjectInfoDto
            {
                Id = p.Id,
                Name = p.Name,
                CreatedAt = p.CreatedAt
            }).ToList()
        };
    }

    public async Task<ProjectUsersResponse> GetProjectUsersAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null)
            throw new ArgumentException($"Проект с {projectId} не найден");

        var projectUsers = await _userProjectRepository.GetByProjectIdAsync(projectId);
        
        var users = (await Task.WhenAll(
            projectUsers.Select(pu => _userRepository.GetByIdAsync(pu.IdUser))
        )).Where(u => u != null).ToList();

        return new ProjectUsersResponse
        {
            ProjectId = project.Id,
            Users = users.Select(u => new UserInfoDto
            {
                Id = u.Id
            }).ToList()
        };
    }
}