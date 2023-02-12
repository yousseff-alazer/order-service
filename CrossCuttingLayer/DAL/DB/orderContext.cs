using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CrossCuttingLayer.DAL.DB
{
    public partial class orderContext : DbContext
    {
        public orderContext()
        {
        }

        public orderContext(DbContextOptions<orderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DpoPaymentIntegration> DpoPaymentIntegrations { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<VerifyTokenReponse> VerifyTokenReponses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=c4s-mysql-dev.cec7g9j6m57h.us-east-1.rds.amazonaws.com;port=3306;database=order;user id=admin;password=0XQjoDAO4QV164IvR1cT;treattinyasboolean=false", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<DpoPaymentIntegration>(entity =>
            {
                entity.ToTable("dpo_payment_integration");

                entity.HasIndex(e => e.TransToken, "TransToken_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ccdapproval)
                    .HasMaxLength(255)
                    .HasColumnName("CCDapproval");

                entity.Property(e => e.CompanyRef).HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.PaymentId).HasColumnName("paymentId");

                entity.Property(e => e.PnrId)
                    .HasMaxLength(255)
                    .HasColumnName("PnrID");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ResultExplanation).HasMaxLength(255);

                entity.Property(e => e.TransId)
                    .HasMaxLength(255)
                    .HasColumnName("TransID");

                entity.Property(e => e.TransRef)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TransToken).IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.RequestUrl).HasMaxLength(150);

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BackUrl).HasMaxLength(700);

                entity.Property(e => e.CountryId).HasMaxLength(100);

                entity.Property(e => e.CountryObject).HasColumnType("json");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.Property(e => e.Discount).HasMaxLength(45);

                entity.Property(e => e.EndOfLife)
                    .HasColumnType("datetime")
                    .HasColumnName("endOfLife");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.IsMonthly)
                    .IsRequired()
                    .HasColumnName("isMonthly")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsPaid)
                    .IsRequired()
                    .HasColumnName("isPaid")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsVirtual)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.NumberOfSession).HasColumnName("numberOfSession");

                entity.Property(e => e.OfferId).HasMaxLength(100);

                entity.Property(e => e.OfferObject).HasColumnType("json");

                entity.Property(e => e.PackageId).HasColumnName("packageId");

                entity.Property(e => e.PackageObject).HasColumnType("json");

                entity.Property(e => e.PackageTypeName).HasMaxLength(100);

                entity.Property(e => e.PaymentUrl).HasMaxLength(700);

                entity.Property(e => e.Period).HasColumnName("period");

                entity.Property(e => e.PlaceAddress).HasMaxLength(445);

                entity.Property(e => e.PlaceName).HasMaxLength(245);

                entity.Property(e => e.PlaceObject).HasColumnType("json");

                entity.Property(e => e.PlayerId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("playerId");

                entity.Property(e => e.PlayerObject)
                    .HasColumnType("json")
                    .HasColumnName("playerObject");

                entity.Property(e => e.Price).HasPrecision(18, 2);

                entity.Property(e => e.ProviderId).HasColumnName("providerId");

                entity.Property(e => e.ProviderName)
                    .HasMaxLength(255)
                    .HasColumnName("providerName");

                entity.Property(e => e.ProviderObject)
                    .HasColumnType("json")
                    .HasColumnName("providerObject");

                entity.Property(e => e.ProviderType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("providerType");

                entity.Property(e => e.ProviderUserId).HasMaxLength(450);

                entity.Property(e => e.SlotArray).HasMaxLength(445);

                entity.Property(e => e.Sport)
                    .HasMaxLength(255)
                    .HasColumnName("sport");

                entity.Property(e => e.SportId).HasColumnName("sportId");

                entity.Property(e => e.SportObject).HasColumnType("json");

                entity.Property(e => e.TotalCost)
                    .HasPrecision(18, 2)
                    .HasColumnName("totalCost");

                entity.Property(e => e.TrainingTypeId).HasDefaultValueSql("'1'");

                entity.Property(e => e.TrainingTypeName).HasMaxLength(245);

                entity.Property(e => e.TransportationFees)
                    .HasPrecision(18, 2)
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.Property(e => e.UserImageUrl).HasMaxLength(600);

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.ValidityDays).HasColumnName("validityDays");

                entity.Property(e => e.Vat)
                    .HasPrecision(18, 2)
                    .HasColumnName("VAT")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.ZoneObject).HasColumnType("json");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("paymentMethod");

                entity.Property(e => e.PlayerId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("playerId");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.TransactionId).HasColumnName("transactionId");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            });

            modelBuilder.Entity<VerifyTokenReponse>(entity =>
            {
                entity.ToTable("verify_token_reponse");

                entity.Property(e => e.AccRef).HasMaxLength(45);

                entity.Property(e => e.CustomerAddress).HasMaxLength(45);

                entity.Property(e => e.CustomerCity).HasMaxLength(45);

                entity.Property(e => e.CustomerCountry).HasMaxLength(45);

                entity.Property(e => e.CustomerCredit).HasMaxLength(45);

                entity.Property(e => e.CustomerCreditType).HasMaxLength(45);

                entity.Property(e => e.CustomerName).HasMaxLength(45);

                entity.Property(e => e.CustomerPhone).HasMaxLength(45);

                entity.Property(e => e.CustomerZip).HasMaxLength(45);

                entity.Property(e => e.FraudAlert).HasMaxLength(45);

                entity.Property(e => e.FraudExplnation).HasMaxLength(45);

                entity.Property(e => e.MobilePaymentRequest).HasMaxLength(45);

                entity.Property(e => e.Result).HasMaxLength(45);

                entity.Property(e => e.ResultExplanation).HasMaxLength(45);

                entity.Property(e => e.TransactionAmount).HasPrecision(10, 3);

                entity.Property(e => e.TransactionApproval).HasMaxLength(45);

                entity.Property(e => e.TransactionCurrency).HasMaxLength(45);

                entity.Property(e => e.TransactionFinalAmount).HasMaxLength(45);

                entity.Property(e => e.TransactionFinalCurrency).HasMaxLength(45);

                entity.Property(e => e.TransactionNetAmount).HasPrecision(10, 3);

                entity.Property(e => e.TransactionRollingReserveAmount).HasMaxLength(45);

                entity.Property(e => e.TransactionRollingReserveDate).HasMaxLength(6);

                entity.Property(e => e.TransactionSettlementDate).HasMaxLength(6);

                entity.Property(e => e.TransactionToken).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
