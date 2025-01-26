using System.Runtime.CompilerServices;
using System.Text.Json;
using CsharpFinalProject.Data.DTO.Showroom;
using CsharpFinalProject.Data.Model;
using CsharpFinalProject.Enums;
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

        Console.WriteLine("Switched to the showroom successfuly");
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

    public void EditCar(int carIndex, CarDto carDto)
    {
        if (CurrentShowroomIndex == -1)
            throw new Exception("There is no showroom to edit the car in, create new showroom or switch to the existing one");
        if (carIndex < 0 || carIndex >= _showrooms[CurrentShowroomIndex].Cars.Count)
            throw new Exception("Invalid index");

        _showrooms[CurrentShowroomIndex].Cars[carIndex] = MapToCar(carDto);

        SaveShowroomsToFile();

        Console.WriteLine($"Car edited to showroom with ID: [{_showrooms[CurrentShowroomIndex].Id}] successfuly");
    }

    public void DeleteCar(int carIndex)
    {
        if (CurrentShowroomIndex == -1)
            throw new Exception("There is no showroom to delete the car in, create new showroom or switch to the existing one");
        if (carIndex < 0 || carIndex >= _showrooms[CurrentShowroomIndex].Cars.Count)
            throw new Exception("Invalid index");

        _showrooms[CurrentShowroomIndex].Cars.RemoveAt(carIndex);

        SaveShowroomsToFile();

        Console.WriteLine($"Car deleted in showroom with ID: [{_showrooms[CurrentShowroomIndex].Id}] successfuly");
    }

    public void SellCar(SaleDto saleDto){
        if (saleDto.carIndex < 0 || saleDto.carIndex >= _showrooms[CurrentShowroomIndex].Sales.Count)
            throw new Exception("Invalid index");
        
        _showrooms[CurrentShowroomIndex].Sales.Add(MapToSale(saleDto));

        SaveShowroomsToFile();

        Console.WriteLine($"Car selled to showroom with ID: [{_showrooms[CurrentShowroomIndex].Id}] successfuly");
    }

    public void SortSalesByDate(int val, int sortTypeIndex)
    {
        if (CurrentShowroomIndex == -1)
            throw new Exception("There is no showroom to sort the sales in, create new showroom or switch to the existing one");

        List<Sale> sortedSales = [];
        DateTime comparisonDate = new DateTime();
        
        switch ((SortType)sortTypeIndex)
        {
            case SortType.DAY:
                comparisonDate = DateTime.Today.AddDays(-val);
                break;
            case SortType.WEEK:
                comparisonDate = DateTime.Today.AddDays(-7 * val);
                break;
            case SortType.MONTH:
                comparisonDate = DateTime.Today.AddMonths(-val);
                break;
            case SortType.YEAR:
                comparisonDate = DateTime.Today.AddYears(-val);
                break;
        }
        
        for (int j = 0; j < _showrooms[CurrentShowroomIndex].Sales.Count; j++)
        {
            if (_showrooms[CurrentShowroomIndex].Sales[j].SaleDate > comparisonDate)
            {
                sortedSales.Add(_showrooms[CurrentShowroomIndex].Sales[j]);
            }
        }

        Console.WriteLine("----------------------------------------------------------");

        for (int i = 0; i < sortedSales.Count; i++)
            Console.WriteLine($"{i + 1} - Price of the car: {sortedSales[i].Price} |\t| Date of the Sale: \"{sortedSales[i].SaleDate}\"");

        Console.WriteLine("----------------------------------------------------------");
    }

    public void SortCarsByModel(string model){
        if (CurrentShowroomIndex == -1)
            throw new Exception("There is no showroom to sort cars in, create new showroom or switch to the existing one");

        List<Car> sortedCars = [];
        
        foreach (var car in _showrooms[CurrentShowroomIndex].Cars)
        {
            if (car.Model.ToLower() == model.ToLower())
                sortedCars.Add(car);
        }

        Console.WriteLine("----------------------------------------------------------");

        for (int i = 0; i < sortedCars.Count; i++)
            Console.WriteLine($"{i + 1} - The brand of the car: {sortedCars[i].Make} |\t| The model of the car: {sortedCars[i].Model} |\t| The date of the car: \"{sortedCars[i].Year}\"");
        
        Console.WriteLine("----------------------------------------------------------");
    }

    private Showroom MapToShowroom(ShowroomDto showroomDto) {
        return new Showroom { 
            Name = showroomDto.Name, 
            CarCapacity = showroomDto.CarCapacity,
            Cars = [],
            Sales = [],
            UserId = _userService.CurrentUser.Id
        }; 
    }

    private Car MapToCar(CarDto carDto){
        return new Car {
            Model = carDto.Model,
            Make = carDto.Make,
            Year = carDto.Year
        };
    }

    private Sale MapToSale(SaleDto saleDto){
        return new Sale {
            SaleDate = saleDto.SaleDate,
            Price = saleDto.price,
            ShowroomId = _showrooms[CurrentShowroomIndex].Id,
            UserId = _userService.CurrentUser.Id,
            CarId = _showrooms[CurrentShowroomIndex].Cars[saleDto.carIndex].Id
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
            Console.WriteLine($"{i + 1} - {_showrooms[i].Name} |\t| Id = [{_showrooms[i].Id}]");

        }
        Console.WriteLine("----------------------------------------------------------");
    }

    public void WriteAllCars()
    {
        if (CurrentShowroomIndex == -1)
            throw new Exception("There is no showroom to show the statistics of the cars, create new showroom or switch to the existing one");
        Console.WriteLine("----------------------------------------------------------");

        for (int i = 0; i < _showrooms[CurrentShowroomIndex].Cars.Count; i++)
            Console.WriteLine($"{i + 1} - Brand: {_showrooms[CurrentShowroomIndex].Cars[i].Make} |\t| Model: {_showrooms[CurrentShowroomIndex].Cars[i].Model} |\t| Year: \"{_showrooms[CurrentShowroomIndex].Cars[i].Year}\" |\t| Id = [{_showrooms[CurrentShowroomIndex].Cars[i].Id}]");

        Console.WriteLine("----------------------------------------------------------");
    }
}
