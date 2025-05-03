using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet9_ketnoigiaothuong.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDatabase_B2BTrading : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK__Category__Parent__403A8C7D",
                        column: x => x.ParentCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessSector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Representative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDocuments",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDocuments", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK__CompanyDo__Compa__398D8EEE",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerCompanyID = table.Column<int>(type: "int", nullable: true),
                    BuyerCompanyID = table.Column<int>(type: "int", nullable: true),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisputeResolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DigitalSignature = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK__Contract__BuyerC__52593CB8",
                        column: x => x.BuyerCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK__Contract__Seller__5165187F",
                        column: x => x.SellerCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__UserAccou__Compa__3C69FB99",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "InvestmentRounds",
                columns: table => new
                {
                    RoundID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvestmentRate = table.Column<double>(type: "float", nullable: true),
                    InvestmentAmount = table.Column<double>(type: "float", nullable: true),
                    ProfitCommitment = table.Column<double>(type: "float", nullable: true),
                    PlannedStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlannedEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Progress = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualRevenue = table.Column<double>(type: "float", nullable: true),
                    ActualProfit = table.Column<double>(type: "float", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentRounds", x => x.RoundID);
                    table.ForeignKey(
                        name: "FK__Investmen__Contr__59063A47",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ContractID");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payment__Contrac__5DCAEF64",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ContractID");
                });

            migrationBuilder.CreateTable(
                name: "PeriodicTransactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportFile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicTransactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK__PeriodicT__Contr__5629CD9C",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ContractID");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderCompanyID = table.Column<int>(type: "int", nullable: true),
                    ReceiverCompanyID = table.Column<int>(type: "int", nullable: true),
                    ContractID = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK__Review__Contract__66603565",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ContractID");
                    table.ForeignKey(
                        name: "FK__Review__Receiver__656C112C",
                        column: x => x.ReceiverCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK__Review__SenderCo__6477ECF3",
                        column: x => x.SenderCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentID);
                    table.ForeignKey(
                        name: "FK__Shipment__Contra__619B8048",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ContractID");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK__Notificat__UserI__6E01572D",
                        column: x => x.UserID,
                        principalTable: "UserAccounts",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<double>(type: "float", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    ApprovalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedByUserUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_UserAccounts_ApprovedByUserUserID",
                        column: x => x.ApprovedByUserUserID,
                        principalTable: "UserAccounts",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Product__Categor__440B1D61",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__Product__Company__4316F928",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistories",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformedByUserID = table.Column<int>(type: "int", nullable: true),
                    ActionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistories", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK__Transacti__Contr__6A30C649",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ContractID");
                    table.ForeignKey(
                        name: "FK__Transacti__Perfo__6B24EA82",
                        column: x => x.PerformedByUserID,
                        principalTable: "UserAccounts",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "QuotationRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerCompanyID = table.Column<int>(type: "int", nullable: true),
                    SellerCompanyID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK__Quotation__Buyer__47DBAE45",
                        column: x => x.BuyerCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK__Quotation__Produ__49C3F6B7",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK__Quotation__Selle__48CFD27E",
                        column: x => x.SellerCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "QuotationResponses",
                columns: table => new
                {
                    ResponseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: true),
                    BuyerCompanyID = table.Column<int>(type: "int", nullable: true),
                    SellerCompanyID = table.Column<int>(type: "int", nullable: true),
                    ProposedPrice = table.Column<double>(type: "float", nullable: true),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationResponses", x => x.ResponseID);
                    table.ForeignKey(
                        name: "FK__Quotation__Buyer__4D94879B",
                        column: x => x.BuyerCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK__Quotation__Reque__4CA06362",
                        column: x => x.RequestID,
                        principalTable: "QuotationRequests",
                        principalColumn: "RequestID");
                    table.ForeignKey(
                        name: "FK__Quotation__Selle__4E88ABD4",
                        column: x => x.SellerCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_CompanyID",
                table: "CompanyDocuments",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BuyerCompanyID",
                table: "Contracts",
                column: "BuyerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SellerCompanyID",
                table: "Contracts",
                column: "SellerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentRounds_ContractID",
                table: "InvestmentRounds",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ContractID",
                table: "Payments",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicTransactions_ContractID",
                table: "PeriodicTransactions",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ApprovedByUserUserID",
                table: "Products",
                column: "ApprovedByUserUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyID",
                table: "Products",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequests_BuyerCompanyID",
                table: "QuotationRequests",
                column: "BuyerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequests_ProductID",
                table: "QuotationRequests",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequests_SellerCompanyID",
                table: "QuotationRequests",
                column: "SellerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationResponses_BuyerCompanyID",
                table: "QuotationResponses",
                column: "BuyerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationResponses_RequestID",
                table: "QuotationResponses",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationResponses_SellerCompanyID",
                table: "QuotationResponses",
                column: "SellerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ContractID",
                table: "Reviews",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReceiverCompanyID",
                table: "Reviews",
                column: "ReceiverCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SenderCompanyID",
                table: "Reviews",
                column: "SenderCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ContractID",
                table: "Shipments",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_ContractID",
                table: "TransactionHistories",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_PerformedByUserID",
                table: "TransactionHistories",
                column: "PerformedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_CompanyID",
                table: "UserAccounts",
                column: "CompanyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDocuments");

            migrationBuilder.DropTable(
                name: "InvestmentRounds");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PeriodicTransactions");

            migrationBuilder.DropTable(
                name: "QuotationResponses");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "TransactionHistories");

            migrationBuilder.DropTable(
                name: "QuotationRequests");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
