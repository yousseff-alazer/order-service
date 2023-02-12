using System;
using System.Collections.Generic;

namespace CrossCuttingLayer.DAL.DB
{
    public partial class Payment
    {
        public long Id { get; set; }
        public string PaymentMethod { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long TransactionId { get; set; }
        public long OrderId { get; set; }
        public string PlayerId { get; set; }
    }
}
