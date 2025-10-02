namespace ControkSystem.Application.DTOs;

public class DefectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public string DeadLine { get; set; } = null!;
    public string Pictures { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Comm { get; set; }= null!;
    public Guid IdProject { get; set; }
    public Guid UserId { get; set; }
    
}

public class CreateDefectDto
{
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public string DeadLine { get; set; } = null!;
    public string Pictures { get; set; } = null!;
    public string Status { get; set; } = null!;
    
    public Guid IdProject { get; set; }
    public Guid UserId { get; set; }
    
    public string Comm { get; set; }= null!;
}