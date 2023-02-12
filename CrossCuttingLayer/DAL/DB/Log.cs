using System;
using System.Collections.Generic;

namespace CrossCuttingLayer.DAL.DB
{
    public partial class Log
    {
        public long Id { get; set; }
        public string RequestUrl { get; set; }
        public string Request { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
