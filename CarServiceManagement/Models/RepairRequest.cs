namespace CarServiceManagement.Models
{
    public class RepairRequest
    {
        public int RepairRequestId { get; set; }
        public int CarModelId { get; set; }
        public string RepairRequestDescription { get; set; }
        public string MobilePhone { get; set; }
        public int StatusId { get; set; }
    }
}