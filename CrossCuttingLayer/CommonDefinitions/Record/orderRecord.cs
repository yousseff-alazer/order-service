using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Linq;

namespace Connect4Sports.CommonDefinitions.Records
{
    public class orderRecord
    {
        public long id { get; set; }
        public decimal? totalCost { get; set; }
        public string sport { get; set; }
        public long providerId { get; set; }
        public long packageId { get; set; }
        public string playerId { get; set; }
        public bool? isPaid { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int? numberOfSession { get; set; }
        public string ProviderType { get; set; }
        public string ProviderName { get; set; }
        public string ProviderObject { get; set; }
        public DateTime? EndOfLife { get; set; }
        public bool? IsMonthly { get; set; }
        public int? Period { get; set; }
        public int? ValidityDays { get; set; }
        public string PlayerObject { get; set; }
        public long? SportId { get; set; }
        public string UserImageUrl { get; set; }
        public string UserName { get; set; }
        public string PackageTypeName { get; set; }
        public DateTime? ExpireAt { get; set; }
        public string ProviderUserId { get; set; }
        public string PackageObject { get; set; }
        public string SportObject { get; set; }
        public string PaymentUrl { get; set; }
        //for flutter
        public string BackUrl { get; set; }

        public string CountryId { get; set; }
        public string CountryObject { get; set; }
        public string OfferId { get; set; }
        public string OfferObject { get; set; }
        public decimal? Vat { get; set; }
        public string Discount { get; set; }
        public decimal? Price { get; set; }

        public int TrainingTypeId { get; set; }
        public int? ZoneId { get; set; }
        public long? PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceAddress { get; set; }
        public string PlaceObject { get; set; }
        public string TrainingTypeName { get; set; }
        public List<string> SlotArray { get; set; }
        public bool? IsVirtual { get; set; }
        public string ZoneObject { get; set; }
        public decimal? TransportationFees { get; set; }


    }

}
