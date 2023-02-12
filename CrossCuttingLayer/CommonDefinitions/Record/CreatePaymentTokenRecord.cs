using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCuttingLayer.CommonDefinitions.Record
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class CreatePaymentTokenResponse
    {
        [JsonProperty("?xml")]
        public Xml Xml { get; set; }
        public API3G API3G { get; set; }
    }

    public class Xml
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("@encoding")]
        public string Encoding { get; set; }
    }


    public class API3G
    {
        public string CompanyToken { get; set; }
        public string Request { get; set; }
        public Transaction Transaction { get; set; }
        public Services Services { get; set; }
        public string Result { get; set; }
        public string ResultExplanation { get; set; }
        public string TransToken { get; set; }
        public string TransRef { get; set; }

    }

    public class CreatePaymentTokenRecord
    {
        public API3G API3G { get; set; }

    }

    public class Service
    {
        public int ServiceType { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceDate { get; set; }
    }

    public class Services
    {
        public Service Service { get; set; }
    }

    public class Transaction
    {
        public double PaymentAmount { get; set; }
        public string PaymentCurrency { get; set; }
        public string CompanyRef { get; set; }
        public string RedirectURL { get; set; }
        public string BackURL { get; set; }
        public int CompanyRefUnique { get; set; }
        public int PTL { get; set; }
        public string customerEmail { get; set; }
        public string customerFirstName { get; set; }
        public string customerLastName { get; set; }
        public string customerPhone { get; set; }
        public string customerAddress { get; set; }
    }


}
