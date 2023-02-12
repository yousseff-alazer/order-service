using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCuttingLayer.CommonDefinitions.Record
{
    public class OfferAPIRecord
    {
        public int Id { get; set; }

        public DateTime Creationdate { get; set; }
        public bool Isdeleted { get; set; }
        public DateTime? Validfrom { get; set; }
        public DateTime? Validto { get; set; }
        public DateTime? Modificationdate { get; set; }
        public string Discount { get; set; }
        public bool? Status { get; set; }
        public string Purpose { get; set; }
        public string Imageurl { get; set; }
        public long? Maxusagecount { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public string LanguageId { get; set; }
        public bool? Valid { get; set; } // for filter
        public bool? AddUse { get; set; }// for add count
        public string? ProviderType { get; set; }
        public long? ProviderTypeId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }

        public List<string> ObjectIds { get; set; }
        public List<int> IdList { get; set; }

        public long? Usedcount { get; set; }
        public string ProviderTypeName { get; set; } = null!;
        public string ProviderTypePhoto { get; set; } = null!;
        public string ObjectUrl { get; set; }

        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public int? ActionTypeId { get; set; }

        public string ConstantType { get; set; }

        public string ActionType { get; set; }
        public int? OfferTypeId { get; set; }
        public string OfferType { get; set; }





        public bool? Ispercentage { get; set; }
        public string? CountryId { get; set; }
        public string? CountryObject { get; set; }
    }

}
