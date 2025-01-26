using CsharpFinalProject.Data.DTO.Showroom;

namespace CsharpFinalProject.Interfaces;

public interface IShowroomService
{
    void CreateShowroom(ShowroomDto showroomDto);
    void CreateCar(CarDto carDto);
    void EditCar(int carIndex, CarDto carDto);
    void DeleteCar(int carIndex);
    void SellCar(SaleDto saleDto);
    void WriteAllShowrooms();
}
