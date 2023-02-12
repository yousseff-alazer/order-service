using Connect4Sports.CommonDefinitions.Records;
using Connect4Sports.Order.API.CommonDefinitions.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Sports.CommonDefinitions.Requests
{
    public class paymentRequest : BaseRequest
    {
        public paymentRecord paymentRecord { get; set; }

    
}
}