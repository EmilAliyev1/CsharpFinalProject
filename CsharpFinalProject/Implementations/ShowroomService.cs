using System.Text.Json;
using CsharpFinalProject.Data.DTO.Showroom;
using CsharpFinalProject.Data.Model;
using CsharpFinalProject.Interfaces;

namespace CsharpFinalProject.Implementations;

public class ShowroomService : IShowroomService
{
    private List<Showroom> _showrooms { get; set; } = new List<Showroom>();
    private int CurrentShowroomIndex { get; set; } = -1;
    private readonly UserService? _userService = new UserService();

    public ShowroomService(){
        var json = File.ReadAllText("./Data/Showrooms.json");

        if (json.Length > 0)
            _showrooms = JsonSerializer.Deserialize<List<Showroom>>(json)!;
    }

    public void SetTheCurrentShowroomIndex(int index){
        if(index > _showrooms.Count - 1 || index < 0)
            throw new Exception("The inputed index is out of range, input valid index");
        
        CurrentShowroomIndex = index;
    }

    public void CreateShowroom(ShowroomDto showroomDto){
        if (!ValidateService.ValidateShowroomName(showroomDto))
            throw new Exception("Invalid characters inputed");

        CurrentShowroomIndex = _showrooms.Count - 1;

        string json = File.ReadAllText("./Data/Showrooms.json");

        if (json.Length > 0)
            _showrooms = JsonSerializer.Deserialize<List<Showroom>>(json)!;

        _showrooms.Add(MapToShowroom(showroomDto));

        SaveShowroomsToFile();

        Console.WriteLine("Showroom created successfully");
    }

    public void CreateCar(CarDto carDto)
    {
        if (CurrentShowroomIndex == -1)
            throw new Exception("There is no showroom to add the car in, create new showroom or switch to the existing one");
        if (_showrooms[CurrentShowroomIndex].Cars.Count == _showrooms[CurrentShowroomIndex].CarCapacity)
            throw new Exception("You exceded the estimated amount of capacity settled in the showroom");

        _showrooms[CurrentShowroomIndex].Cars.Add(MapToCar(carDto));

        SaveShowroomsToFile();

        Console.WriteLine($"Car added to showroom with ID: [{_showrooms[CurrentShowroomIndex].Id}] successfuly");
    }

    public void EditCar(CarDto carDto, int carIndex)
    {
        if (CurrentShowroomIndex == -1)
            throw new Exception("There is no showroom to edit the car in, create new showroom or switch to the existing one");

        _showrooms[CurrentShowroomIndex].Cars[carIndex] = MapToCar(carDto);

        SaveShowroomsToFile();

        Console.WriteLine($"Car edited to showroom with ID: [{_showrooms[CurrentShowroomIndex].Id}] successfuly");
    }

    public void DeleteCar(int carIndex)
    {
        if (CurrentShowroomIndex == -1)
            throw new Exception("There is no showroom to delete the car in, create new showroom or switch to the existing one");

        _showrooms[CurrentShowroomIndex].Cars.RemoveAt(carIndex);

        SaveShowroomsToFile();

        Console.WriteLine($"Car deleted in showroom with ID: [{_showrooms[CurrentShowroomIndex].Id}] successfuly");
    }

    public void SellCar(SaleDto saleDto, int userIndex){
        Guid userId = _userService.GetUserIdByIndex(userIndex);

        _showrooms[CurrentShowroomIndex].Sales.Add(MapToSale(saleDto, userId));

        SaveShowroomsToFile();

        Console.WriteLine($"Car selled to showroom with ID: [{_showrooms[CurrentShowroomIndex].Id}] successfuly");
    }

    private Showroom MapToShowroom(ShowroomDto showroomDto) { 
        return new Showroom { 
            Name = showroomDto.Name, 
            CarCapacity = showroomDto.CarCapacity,
            Cars = []
        }; 
    }

    private Car MapToCar(CarDto carDto){
        return new Car {
            Model = carDto.Model,
            Make = carDto.Make,
            Year = carDto.Year
        };
    }

    private Sale MapToSale(SaleDto saleDto, Guid userId){
        return new Sale {
            SaleDate = saleDto.SaleDate,
            Price = saleDto.price,
            ShowroomId = _showrooms[CurrentShowroomIndex].Id,
            UserId = userId
        };
    }

    private void SaveShowroomsToFile(){
        var jsonString = JsonSerializer.Serialize(_showrooms);

        File.WriteAllText("./Data/Showrooms.json", jsonString);
    }

    public void WriteAllShowrooms(){
        Console.WriteLine("----------------------------------------------------------");
        for (int i = 0; i < _showrooms.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {_showrooms[i].Name}\tId = [{_showrooms[i].Id}]");

        }
        Console.WriteLine("----------------------------------------------------------");
    }

    // public void WriteAllCars
}
