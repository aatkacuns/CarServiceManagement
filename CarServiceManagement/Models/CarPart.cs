namespace CarServiceManagement.Models
{
    public class CarPart
    {
        public int PartId { get; set; }
        public int PartTypeId { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public int CarModelId { get; set; }
        public string Color { get; set; }
        public decimal PartPurchasePrice { get; set; }
        public decimal PartRetailPrice { get; set; }
        
    }
}