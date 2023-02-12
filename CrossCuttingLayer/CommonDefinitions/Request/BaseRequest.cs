using CrossCuttingLayer.DAL.DB;
using CrossCuttingLayer.DAL.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4Sports.Order.API.CommonDefinitions.Requests
{
    public class BaseRequest
    {
        public orderContext _context;

        public  int DefaultPageSize = 80;

        public bool IsDesc { get; set; }

        public string OrderByColumn { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        //public string LanguageId { get; set; }
        public string BaseUrl { get; set; }
        public string Name { get; set; }
        public long VendorId { get; set; }
    }
}