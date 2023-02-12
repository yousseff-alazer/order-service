using Connect4Sports.CommonDefinitions.Records;
using Connect4Sports.Order.API.CommonDefinitions.Responses;
using System.Collections.Generic;

namespace Connect4Sports.CommonDefinitions.Responses
{
    public class paymentResponse : BaseResponse
    {

        public List<paymentRecord> paymentRecords { get; set; }
    }
}