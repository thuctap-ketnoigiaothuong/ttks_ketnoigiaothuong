namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class CompanyContract
    {
        public record CompanyContact(string Email, string PhoneNumber);
        public record CompanyViewModel(string CompanyName, string TaxCode, string BusinessSector, string Address, string Representative, string Email, string PhoneNumber);
        public record ResponseCompany(string CompanyName, string TaxCode, string BusinessSector, string Address, string Representative, string Email, string PhoneNumber, DateTime RegistrationDate, string VerificationStatus, string LegalDocuments);
        public record FullResponseCompany(int CompanyID, string CompanyName, string TaxCode, string BusinessSector, string Address, string Representative, string Email, string PhoneNumber, DateTime RegistrationDate, string VerificationStatus, string LegalDocuments);
    }
}
