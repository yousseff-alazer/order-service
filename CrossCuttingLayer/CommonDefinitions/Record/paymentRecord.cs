using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Linq;

namespace Connect4Sports.CommonDefinitions.Records
{
    public class paymentRecord
    {
       



        public long id { get; set; }


        public string paymentMethod { get; set; }


        public bool status { get; set; }
		

        public DateTime createdAt { get; set; }
		

		
        public DateTime updatedAt { get; set; }
	



        public long transactionId { get; set; }


        public long orderId { get; set; }


        public string playerId { get; set; }

    }

}
