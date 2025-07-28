using System;

namespace ABCCarTraders.Models
{
    public class CarPart
    {
        public int PartID { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int CategoryID { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CategoryName { get; set; }
    }
}