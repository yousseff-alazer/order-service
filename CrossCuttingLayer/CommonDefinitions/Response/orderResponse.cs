using Connect4Sports.CommonDefinitions.Records;
using Connect4Sports.Order.API.CommonDefinitions.Responses;
using System.Collections.Generic;

namespace Connect4Sports.CommonDefinitions.Responses
{
    public class orderResponse : BaseResponse
    {

        public List<orderRecord> orderRecords { get; set; }
    }
}