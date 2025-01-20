using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CsharpFinalProject.Data.DTO.User;
using CsharpFinalProject.Data.Model;

namespace CsharpFinalProject.Implementations;

public class ShowroomService
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

    private Showroom MapToShowroom(ShowroomDto showroomDto) { 
        return new Showroom { 
            Name = showroomDto.Name, 
            CarCapacity = showroomDto.CarCapacity
        }; 
    }
}
