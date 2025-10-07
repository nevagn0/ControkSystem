
namespace ControkSystem.Application.DTOs;
public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Progres { get; set; } = null!;
}

public class ProjectCreateDto
{
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Progres { get; set; } = null!;
    public ICollection<DefectDto> Defects { get; set; } = null!;
    
}
public class Updateproject
{
    public string Name { get; set; } = null!;
    public string Progres { get; set; } = null!;
}