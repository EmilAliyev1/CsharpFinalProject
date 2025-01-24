using CsharpFinalProject.Data.DTO.Showroom;

namespace CsharpFinalProject.Interfaces;

public interface IShowroomService
{
    void CreateShowroom(ShowroomDto showroomDto);
    void CreateCar(CarDto carDto);
    void EditCar(CarDto carDto, int carIndex);
    void DeleteCar(int carIndex);
    void SellCar(SaleDto saleDto, int userIndex);
    void WriteAllShowrooms();
}
