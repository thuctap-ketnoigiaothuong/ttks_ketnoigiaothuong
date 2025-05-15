using System;

namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class CompanyContract
    {
        public record CompanyContact(string Email, string PhoneNumber);
        public record CompanyViewModel(string CompanyName, string TaxCode, string BusinessSector, string Address, string Representative, string Email, string PhoneNumber);
        public record ResponseCompany(string CompanyName, string TaxCode, string BusinessSector, string Address, string Representative, string Email, string PhoneNumber, DateTime RegistrationDate, string VerificationStatus, string LegalDocuments);
        public record FullResponseCompany(int CompanyID, string CompanyName, string TaxCode, string BusinessSector, string Address, string Representative, string Email, string PhoneNumber, DateTime? RegistrationDate, string VerificationStatus, string LegalDocuments);
        
        public class CreateCompanyModel
        {
            public string CompanyName { get; set; }
            public string TaxCode { get; set; }
            public string BusinessSector { get; set; }
            public string Address { get; set; }
            public string Representative { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string LegalDocuments { get; set; }
        }

        public class UpdateCompanyModel
        {
            public string CompanyName { get; set; }
            public string TaxCode { get; set; }
            public string BusinessSector { get; set; }
            public string Address { get; set; }
            public string Representative { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string LegalDocuments { get; set; }
            public string VerificationStatus { get; set; }
        }

        public class CompanyListItem
        {
            public int CompanyID { get; set; }
            public string CompanyName { get; set; }
            public string BusinessSector { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string VerificationStatus { get; set; }
        }
    }
}
