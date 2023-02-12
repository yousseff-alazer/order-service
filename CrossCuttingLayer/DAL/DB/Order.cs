using System;
using System.Collections.Generic;

namespace CrossCuttingLayer.DAL.DB
{
    public partial class Order
    {
        public long Id { get; set; }
        public decimal TotalCost { get; set; }
        public string Sport { get; set; }
        public long ProviderId { get; set; }
        public long PackageId { get; set; }
        public string PlayerId { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? NumberOfSession { get; set; }
        public string ProviderType { get; set; }
        public string ProviderName { get; set; }
        public string ProviderObject { get; set; }
        public DateTime? EndOfLife { get; set; }
        public bool? IsMonthly { get; set; }
        public int? Period { get; set; }
        public int? ValidityDays { get; set; }
        public string PlayerObject { get; set; }
        public long? SportId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
        public string PackageTypeName { get; set; }
        public DateTime? ExpireAt { get; set; }
        public string ProviderUserId { get; set; }
        public string PackageObject { get; set; }
        public string SportObject { get; set; }
        public string PaymentUrl { get; set; }
        public string BackUrl { get; set; }
        public string CountryId { get; set; }
        public string CountryObject { get; set; }
        public string OfferId { get; set; }
        public string OfferObject { get; set; }
        public decimal? Vat { get; set; }
        public string Discount { get; set; }
        public decimal Price { get; set; }
        public int TrainingTypeId { get; set; }
        public int? ZoneId { get; set; }
        public long? PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceAddress { get; set; }
        public string PlaceObject { get; set; }
        public string TrainingTypeName { get; set; }
        public string SlotArray { get; set; }
        public bool? IsVirtual { get; set; }
        public string ZoneObject { get; set; }
        public decimal? TransportationFees { get; set; }
    }
}
