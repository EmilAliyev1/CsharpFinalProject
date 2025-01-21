using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpFinalProject.Data.DTO.Showroom;

namespace CsharpFinalProject.Interfaces;

public interface IShowroomService
{
    void CreateShowroom(ShowroomDto showroomDto);
    void CreateCar(CarDto carDto, int index);
    void WriteShowrooms();
}
