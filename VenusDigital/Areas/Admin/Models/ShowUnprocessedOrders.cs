using System;

namespace VenusDigital.Areas.Admin.Models
{
    public class ShowUnprocessedOrders
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int PosPostalInformationId { get; set; }
        public int DetailId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string? Color { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public decimal? TotalPriceWithCoupon { get; set; }
        public bool AppliedCoupon { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentTraceCode { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsReferred { get; set; }
        public string? ReferredDescription { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductMainPrice { get; set; }
        public decimal ProductOnSalePrice { get; set; }
    }
}
