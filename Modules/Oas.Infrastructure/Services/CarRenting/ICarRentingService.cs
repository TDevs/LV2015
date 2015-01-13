using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;

namespace Oas.Infrastructure.Services
{
    public interface ICarRentingService
    {

        #region Car Category

        IQueryable<CarCategory> GetCarCategories();

        CarCategory GetCarCategory(Guid carCategoryId);

        OperationStatus AddCarCategory(CarCategory carCategory);

        OperationStatus UpdateCarCategory(CarCategory carCategory);

        OperationStatus DeleteCarCategory(Guid carCategoryId);

        #endregion 

        #region Car Model

        IQueryable<CarModel> GetCarModels();

        CarModel GetCarModel(Guid carModelId);

        OperationStatus AddCarModel(CarModel carModel);

        OperationStatus UpdateCarModel(CarModel carModel);

        OperationStatus DeleteCarModel(Guid carModelId);

        #endregion 

        #region Car

        IQueryable<Car> GetCars();

        IQueryable<Car> SearchCar(CarCriteria criteria);

        Car GetCar(Guid carId);

        OperationStatus AddCar(Car car);

        OperationStatus UpdateCar(Car car);

        OperationStatus DeleteCar(Guid carId);

        #endregion 
    }
}
