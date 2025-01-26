using CsharpFinalProject.Interfaces;

namespace CsharpFinalProject.Implementations;

public class GetValueService : IGetValueService
{
    public string GetStringInputValue(){
        string value;
        do
        {
            value = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(value));

        return value;
    }

    public int GetIntInputValue(){
        if(!int.TryParse(Console.ReadLine(), out int num))
            throw new Exception("Invalid input");

        return num;
    }

    public double GetDoubleInputValue(){
        if(!double.TryParse(Console.ReadLine(), out double num))
            throw new Exception("Invalid input");

        return num;
    }
}
