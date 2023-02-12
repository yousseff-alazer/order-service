using Connect4Sports.BL.Services;
using Connect4Sports.Order.API.BL.Services;
using CrossCuttingLayer.CommonDefinitions.Record;
using CrossCuttingLayer.DAL.DB;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Reservation.CommonDefinitions.Records;

namespace Connect4Sports.Order.API.Consumers
{
    public class PlayerRMQConsumer : IConsumer<PlayerRMQRecord>
    {
        private readonly orderContext _context;
        private readonly IOptions<AppSettingsRecord> _appSettings;

        public PlayerRMQConsumer(IOptions<AppSettingsRecord> appSettings)
        {
            _appSettings = appSettings;
            _context = new orderContext(BaseService.GetDBContextConnectionOptions(_appSettings.Value.DatabaseSettings.ConnectionString));
        }
        public async Task Consume(ConsumeContext<PlayerRMQRecord> context)
        {
            var data = context.Message;
            if (data != null)
            {
                var PlayerResponse = orderService.EditPlayerInfo(_context, data);
            }
        }
    }
}
