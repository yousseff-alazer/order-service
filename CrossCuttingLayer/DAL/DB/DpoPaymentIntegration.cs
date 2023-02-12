using System;
using System.Collections.Generic;

namespace CrossCuttingLayer.DAL.DB
{
    public partial class DpoPaymentIntegration
    {
        public long Id { get; set; }
        public string Result { get; set; }
        public string ResultExplanation { get; set; }
        public long OrderId { get; set; }
        public long? PaymentId { get; set; }
        public string TransToken { get; set; }
        public string TransRef { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string TransId { get; set; }
        public string Ccdapproval { get; set; }
        public string PnrId { get; set; }
        public string CompanyRef { get; set; }
    }
}
