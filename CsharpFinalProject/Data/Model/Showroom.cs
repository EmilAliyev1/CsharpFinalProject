using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsharpFinalProject.Data.Model;
public class Showroom
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string Name { get; set; } 
    public List<Car> Cars { get; set; } 
    public List<Sale> Sales { get; set; }
    public Guid UserId { get; set; }
    public int CarCapacity { get; set; } // машин не может быть больше чем CarCapacity 
    public int CarCount => Cars.Count; // количество машин в салоне 
    public int SalesCount => Sales.Count;
}