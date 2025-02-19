namespace CsharpFinalProject.Data.Model;

public class Car
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Make { get; set; } // Brand of the car
    public string Model { get; set; } 
    public DateTime Year { get; set; } // dd-MM-yyyy
}