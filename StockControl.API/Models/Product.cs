namespace StockControl.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal AverageCostPrice { get; set; }
        public string? PartNumber { get; set; }
    }
}
