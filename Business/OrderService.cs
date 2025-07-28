using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;
using System.Collections.Generic;
using System.Linq;

namespace ABCCarTraders.Business
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository;
        private readonly CarRepository carRepository;
        private readonly CarPartRepository carPartRepository;

        public OrderService()
        {
            orderRepository = new OrderRepository();
            carRepository = new CarRepository();
            carPartRepository = new CarPartRepository();
        }

        public bool CreateCarOrder(int customerId, int carId, int quantity = 1)
        {
            var cars = carRepository.GetAllCars();
            var car = cars.FirstOrDefault(c => c.CarID == carId && c.IsAvailable);
            
            if (car == null)
                return false;

            var order = new Order
            {
                CustomerID = customerId,
                TotalAmount = car.Price * quantity,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ItemType = ItemType.Car,
                        ItemID = carId,
                        Quantity = quantity,
                        Price = car.Price,
                        ItemName = $"{car.Brand} {car.Model} ({car.Year})"
                    }
                }
            };

            return orderRepository.CreateOrder(order);
        }

        public bool CreatePartOrder(int customerId, int partId, int quantity = 1)
        {
            var parts = carPartRepository.GetAllCarParts();
            var part = parts.FirstOrDefault(p => p.PartID == partId && p.IsAvailable && p.StockQuantity >= quantity);
            
            if (part == null)
                return false;

            var order = new Order
            {
                CustomerID = customerId,
                TotalAmount = part.Price * quantity,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ItemType = ItemType.Part,
                        ItemID = partId,
                        Quantity = quantity,
                        Price = part.Price,
                        ItemName = $"{part.PartName} - {part.Brand}"
                    }
                }
            };

            return orderRepository.CreateOrder(order);
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            return orderRepository.GetOrdersByCustomer(customerId);
        }

        public List<Order> GetAllOrders()
        {
            return orderRepository.GetAllOrders();
        }

        public bool CancelOrder(int orderId, int customerId)
        {
            return orderRepository.DeleteOrder(orderId);
        }

        public decimal CalculateOrderTotal(List<OrderItem> items)
        {
            return items.Sum(item => item.Price * item.Quantity);
        }
    }
}