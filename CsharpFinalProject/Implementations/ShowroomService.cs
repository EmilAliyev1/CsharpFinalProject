using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CsharpFinalProject.Data.DTO.Showroom;
using CsharpFinalProject.Data.DTO.User;
using CsharpFinalProject.Data.Model;
using CsharpFinalProject.Interfaces;

namespace CsharpFinalProject.Implementations;

public class ShowroomService : IShowroomService
{
    private List<Showroom> _showrooms { get; set; } = new List<Showroom>();

    public ShowroomService(){
        var json = File.ReadAllText("./Data/Showrooms.json");

        if (json.Length > 0)
            _showrooms = JsonSerializer.Deserialize<List<Showroom>>(json)!;
    }

    public void CreateShowroom(ShowroomDto showroomDto){
        if (!ValidateService.ValidateShowroomName(showroomDto))
            throw new Exception("Invalid characters inputed");

        string json = File.ReadAllText("./Data/Showrooms.json");

        if (json.Length > 0)
            _showrooms = JsonSerializer.Deserialize<List<Showroom>>(json)!;

        _showrooms.Add(MapToShowroom(showroomDto));

        var jsonString = JsonSerializer.Serialize(_showrooms);

        File.WriteAllText("./Data/Showrooms.json", jsonString);

        Console.WriteLine("Showroom created successfully");
    }

    public void CreateCar(CarDto carDto, int index)
    {
        _showrooms[index].Cars.Add(MapToCar(carDto));
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

    public void WriteShowrooms(){
        Console.WriteLine("----------------------------------------------------------");
        for (int i = 0; i < _showrooms.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {_showrooms[i]}");

        }
        Console.WriteLine("----------------------------------------------------------");
    }
}
