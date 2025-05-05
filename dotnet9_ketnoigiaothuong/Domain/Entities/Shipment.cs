using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class Shipment
    {
        [Key]
        public int ShipmentID { get; set; }
        public int? ContractID { get; set; }
        public string? Status { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Description { get; set; }

        public virtual Contract? Contract { get; set; }
    }
}
