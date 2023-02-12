using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCuttingLayer.CommonDefinitions.Record
{
    public class PackagePriceRecord
    {
        public double price { get; set; }
        public bool isSuccess { get; set; }
        public decimal? TransportFee { get; set; }

    }



    public class Root
    {
        public PackagePriceRecord packagePriceRecord { get; set; }
    }

}
