namespace CsharpFinalProject.Data.Model;

public class User
{
    // автоматически генерируется новый Guid для уникальности 
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string? Username { get; set; } 
    public string? Password { get; set; } 
}