namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class QuotationRequestContract
    {
        public record CreateQuotationRequest(int BuyerCompanyID, int SellerCompanyID, int ProductID,
            int Quantity, string DeliveryTime, string AdditionalRequest, DateTime CreatedDate);
        
        public record ReponseQuotationRequest(int RequestID, int BuyerCompanyID, int SellerCompanyID, int ProductID,
            int Quantity, string DeliveryTime, string AdditionalRequest, DateTime CreatedDate);
    }
}
