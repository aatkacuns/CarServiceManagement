using System;

namespace CarServiceManagement.Models
{
    public class WorkList
    {
        public  int WorkId { get; set; }
        public int WorkTypeId { get; set; }
        public string WorkProblemDescription { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public int WorkerId { get; set; }
        public DateTime WorkDate { get; set; }
        public decimal ElapsedWorkTime { get; set; }
        public decimal PriceForHumanWork { get; set; }
        public decimal PriceForDetails { get; set; }
        public decimal TotalPrice { get; set; }
        public string OtherDetails { get; set; }
    }
}