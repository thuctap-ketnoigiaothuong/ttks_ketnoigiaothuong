namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class QuotationResponseContract
    {
        public record CreateQuotationResponse(int RequestID, int BuyerCompanyID, int SellerCompanyID,
            double ProposedPrice, string DeliveryTime, string ShippingMethod, string Terms, DateTime CreatedDate);

        public record UpdateQuotationResponse(double ProposedPrice, string DeliveryTime,
            string ShippingMethod, string Terms);

        public record ReponseQuotationResponse(int ResponseID, int RequestID, int BuyerCompanyID, int SellerCompanyID,
            double ProposedPrice, string DeliveryTime, string ShippingMethod, string Terms, DateTime CreatedDate);
    }
}