using Connect4Sports.BL.Services;
using Connect4Sports.CommonDefinitions.Records;
using Connect4Sports.CommonDefinitions.Requests;
using Connect4Sports.Order.API.BL.Services;
using CrossCuttingLayer.CommonDefinitions.Record;
using CrossCuttingLayer.DAL.DB;
using MassTransit;
using Microsoft.Build.Experimental.ProjectCache;
using Microsoft.Extensions.Options;

namespace Connect4Sports.Order.API.Consumers
{
    public class OrderPaidConsumer : IConsumer<orderRecord>
    {
        private readonly IOptions<AppSettingsRecord> appSettings;
        private readonly orderContext _context;
        public OrderPaidConsumer(IOptions<AppSettingsRecord> app)
        {
            appSettings = app;
            _context = new orderContext(BaseService.GetDBContextConnectionOptions(appSettings.Value.DatabaseSettings.ConnectionString));
        }
        public async Task Consume(ConsumeContext<orderRecord> context)
        {
            var data = context.Message;
            if(data != null && data.id > 0 && data.isPaid != null ) 
            {
                var request = new orderRequest();
                request.orderRecord = data;

                if(data.ValidityDays != null)
                    request.orderRecord.ExpireAt = DateTime.Now.AddDays((double)request.orderRecord.ValidityDays);
                
                request._context =_context;
                var orderResponse = orderService.Editorder(request);
                //return orderResponse;
            }
        }
    }
}
