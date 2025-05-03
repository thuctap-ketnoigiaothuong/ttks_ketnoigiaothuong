using dotnet9_ketnoigiaothuong.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet9_ketnoigiaothuong.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyDocument> CompanyDocuments { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<QuotationRequest> QuotationRequests { get; set; }
        public DbSet<QuotationResponse> QuotationResponses { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PeriodicTransaction> PeriodicTransactions { get; set; }
        public DbSet<InvestmentRound> InvestmentRounds { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category to ParentCategory relationship
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(p => p.SubCategories)
                .HasForeignKey(c => c.ParentCategoryID)
                .HasConstraintName("FK__Category__Parent__403A8C7D");

            // Company to CompanyDocument relationship
            modelBuilder.Entity<CompanyDocument>()
                .HasOne(cd => cd.Company)
                .WithMany(c => c.CompanyDocuments)
                .HasForeignKey(cd => cd.CompanyID)
                .HasConstraintName("FK__CompanyDo__Compa__398D8EEE");

            // Contract to BuyerCompany relationship
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.BuyerCompany)
                .WithMany(b => b.BuyerContracts)
                .HasForeignKey(c => c.BuyerCompanyID)
                .HasConstraintName("FK__Contract__BuyerC__52593CB8");

            // Contract to SellerCompany relationship
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.SellerCompany)
                .WithMany(s => s.SellerContracts)
                .HasForeignKey(c => c.SellerCompanyID)
                .HasConstraintName("FK__Contract__Seller__5165187F");

            // InvestmentRound to Contract relationship
            modelBuilder.Entity<InvestmentRound>()
                .HasOne(ir => ir.Contract)
                .WithMany(c => c.InvestmentRounds)
                .HasForeignKey(ir => ir.ContractID)
                .HasConstraintName("FK__Investmen__Contr__59063A47");

            // Notification to User relationship
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserID)
                .HasConstraintName("FK__Notificat__UserI__6E01572D");

            // Payment to Contract relationship
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Contract)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.ContractID)
                .HasConstraintName("FK__Payment__Contrac__5DCAEF64");

            // PeriodicTransaction to Contract relationship
            modelBuilder.Entity<PeriodicTransaction>()
                .HasOne(pt => pt.Contract)
                .WithMany(c => c.PeriodicTransactions)
                .HasForeignKey(pt => pt.ContractID)
                .HasConstraintName("FK__PeriodicT__Contr__5629CD9C");

            // Product to Category relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .HasConstraintName("FK__Product__Categor__440B1D61");

            // Product to Company relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CompanyID)
                .HasConstraintName("FK__Product__Company__4316F928");

            // QuotationRequest to BuyerCompany relationship
            modelBuilder.Entity<QuotationRequest>()
                .HasOne(qr => qr.BuyerCompany)
                .WithMany(c => c.QuotationRequestsAsBuyer)
                .HasForeignKey(qr => qr.BuyerCompanyID)
                .HasConstraintName("FK__Quotation__Buyer__47DBAE45");

            // QuotationRequest to Product relationship
            modelBuilder.Entity<QuotationRequest>()
                .HasOne(qr => qr.Product)
                .WithMany(p => p.QuotationRequests)
                .HasForeignKey(qr => qr.ProductID)
                .HasConstraintName("FK__Quotation__Produ__49C3F6B7");

            // QuotationRequest to SellerCompany relationship
            modelBuilder.Entity<QuotationRequest>()
                .HasOne(qr => qr.SellerCompany)
                .WithMany(c => c.QuotationRequestsAsSeller)
                .HasForeignKey(qr => qr.SellerCompanyID)
                .HasConstraintName("FK__Quotation__Selle__48CFD27E");

            // QuotationResponse to BuyerCompany relationship
            modelBuilder.Entity<QuotationResponse>()
                .HasOne(qr => qr.BuyerCompany)
                .WithMany(c => c.QuotationResponsesAsBuyer)
                .HasForeignKey(qr => qr.BuyerCompanyID)
                .HasConstraintName("FK__Quotation__Buyer__4D94879B");

            // QuotationResponse to Request relationship
            modelBuilder.Entity<QuotationResponse>()
                .HasOne(qr => qr.QuotationRequest)
                .WithMany(r => r.QuotationResponses)
                .HasForeignKey(qr => qr.RequestID)
                .HasConstraintName("FK__Quotation__Reque__4CA06362");

            // QuotationResponse to SellerCompany relationship
            modelBuilder.Entity<QuotationResponse>()
                .HasOne(qr => qr.SellerCompany)
                .WithMany(c => c.QuotationResponsesAsSeller)
                .HasForeignKey(qr => qr.SellerCompanyID)
                .HasConstraintName("FK__Quotation__Selle__4E88ABD4");

            // Review to Contract relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Contract)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.ContractID)
                .HasConstraintName("FK__Review__Contract__66603565");

            // Review to ReceiverCompany relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.ReceiverCompany)
                .WithMany(c => c.ReviewsAsReceiver)
                .HasForeignKey(r => r.ReceiverCompanyID)
                .HasConstraintName("FK__Review__Receiver__656C112C");

            // Review to SenderCompany relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.SenderCompany)
                .WithMany(c => c.ReviewsAsSender)
                .HasForeignKey(r => r.SenderCompanyID)
                .HasConstraintName("FK__Review__SenderCo__6477ECF3");

            // Shipment to Contract relationship
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Contract)
                .WithMany(c => c.Shipments)
                .HasForeignKey(s => s.ContractID)
                .HasConstraintName("FK__Shipment__Contra__619B8048");

            // TransactionHistory to Contract relationship
            modelBuilder.Entity<TransactionHistory>()
                .HasOne(th => th.Contract)
                .WithMany(c => c.TransactionHistories)
                .HasForeignKey(th => th.ContractID)
                .HasConstraintName("FK__Transacti__Contr__6A30C649");

            // TransactionHistory to PerformedByUser relationship
            modelBuilder.Entity<TransactionHistory>()
                .HasOne(th => th.PerformedByUser)
                .WithMany(u => u.TransactionHistories)
                .HasForeignKey(th => th.PerformedByUserID)
                .HasConstraintName("FK__Transacti__Perfo__6B24EA82");

            // UserAccount to Company relationship
            modelBuilder.Entity<UserAccount>()
                .HasOne(ua => ua.Company)
                .WithMany(c => c.UserAccounts)
                .HasForeignKey(ua => ua.CompanyID)
                .HasConstraintName("FK__UserAccou__Compa__3C69FB99");

        }
    }
}
