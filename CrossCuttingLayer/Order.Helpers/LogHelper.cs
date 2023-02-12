
using CrossCuttingLayer.DAL.DB;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Connect4Sports.Order.API.Order.Helpers
{

    public class LogHelper
    {
        private static readonly orderContext _context;
        static LogHelper()
        {
            _context = new orderContext();
        }
        public static void LogException(string controller, string action, string request, Exception ex)
        {
            var log = new Log
            {
                Request = request,
                RequestUrl = controller + "/" + action,
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
            };
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

    }
}