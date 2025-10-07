namespace ControkSystem.Application.DTOs
{
    // Входные данные
    public class AddUserToProjectRequest
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class RemoveUserFromProjectRequest
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }

    // Выходные данные
    public class UserProjectResponse
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UserProjectsResponse
    {
        public Guid UserId { get; set; }
        public List<ProjectInfoDto> Projects { get; set; } = new();
    }

    public class ProjectUsersResponse
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<UserInfoDto> Users { get; set; } = new();
    }

    public class ProjectInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Login { get; set; }
    }
}