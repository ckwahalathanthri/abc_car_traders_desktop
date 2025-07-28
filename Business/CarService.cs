using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using System;
using System.Collections.Generic;

namespace ABCCarTraders.Business
{
    public class CarService
    {
        private readonly CarRepository carRepository;

        public CarService()
        {
            carRepository = new CarRepository();
        }

        public List<Car> GetAvailableCars()
        {
            return carRepository.GetAllCars().FindAll(c => c.IsAvailable);
        }

        public List<Car> SearchCars(string brand = "", string model = "", int? yearFrom = null, int? yearTo = null, 
            decimal? priceFrom = null, decimal? priceTo = null, int categoryId = 0)
        {
            var cars = carRepository.SearchCars("", categoryId);
            
            // Apply additional filters
            if (!string.IsNullOrEmpty(brand))
                cars = cars.FindAll(c => c.Brand.Contains(brand, StringComparison.OrdinalIgnoreCase));
                
            if (!string.IsNullOrEmpty(model))
                cars = cars.FindAll(c => c.Model.Contains(model, StringComparison.OrdinalIgnoreCase));
                
            if (yearFrom.HasValue)
                cars = cars.FindAll(c => c.Year >= yearFrom.Value);
                
            if (yearTo.HasValue)
                cars = cars.FindAll(c => c.Year <= yearTo.Value);
                
            if (priceFrom.HasValue)
                cars = cars.FindAll(c => c.Price >= priceFrom.Value);
                
            if (priceTo.HasValue)
                cars = cars.FindAll(c => c.Price <= priceTo.Value);

            return cars;
        }

        public bool AddCar(Car car)
        {
            if (ValidateCar(car))
            {
                return carRepository.AddCar(car);
            }
            return false;
        }

        public bool UpdateCar(Car car)
        {
            if (ValidateCar(car))
            {
                return carRepository.UpdateCar(car);
            }
            return false;
        }

        public bool DeleteCar(int carId)
        {
            return carRepository.DeleteCar(carId);
        }

        private bool ValidateCar(Car car)
        {
            if (string.IsNullOrWhiteSpace(car.Brand) || string.IsNullOrWhiteSpace(car.Model))
                return false;
                
            if (car.Year < 1900 || car.Year > DateTime.Now.Year + 1)
                return false;
                
            if (car.Price <= 0)
                return false;

            return true;
        }
    }
}