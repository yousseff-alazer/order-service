using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrossCuttingLayer.DAL.DB;
using Connect4Sports.CommonDefinitions.Requests;
using Connect4Sports.BL.Services;
using CrossCuttingLayer.DAL.DB;
using CrossCuttingLayer.CommonDefinitions.Record;
using Connect4Sports.Order.API.Order.Helpers;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace Connect4Sports.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly orderContext _context;
        private readonly IOptions<AppSettingsRecord> _appSettings;

        public OrdersController(orderContext context, IOptions<AppSettingsRecord> appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        [HttpPost]
        [Route("Listorder")]
        public ActionResult Listorder([FromBody] orderRequest request)
        {
            request._context = _context;

            var orderResponse = orderService.Listorder(request);
            return Ok(new
            {
                orderResponse
            });
        }

        //AddXMLRequestToServiceApi
        [HttpGet]
        [Route("CreatePaymentToken")]
        public ActionResult CreatePaymentToken()
        {
            var createPaymentTokenRecord = new CreatePaymentTokenRecord();
            createPaymentTokenRecord.API3G=new API3G();
            createPaymentTokenRecord.API3G.Transaction = new Transaction();
            createPaymentTokenRecord.API3G.Services = new Services();
            createPaymentTokenRecord.API3G.Services.Service = new Service();
            createPaymentTokenRecord.API3G.CompanyToken = "50EC6AD4-1257-452E-8F35-B27EDA0CB14C";
            createPaymentTokenRecord.API3G.Request = "createToken";
            createPaymentTokenRecord.API3G.Transaction.PaymentAmount  = Math.Round(0.50, 2);
            createPaymentTokenRecord.API3G.Transaction.PaymentCurrency = "USD";
            createPaymentTokenRecord.API3G.Transaction.CompanyRef = "KDIEOM";
            createPaymentTokenRecord.API3G.Transaction.RedirectURL = "http://34.192.91.241:89/api/Payments/AddPaymentTransaction";
            createPaymentTokenRecord.API3G.Transaction.BackURL = "https://www.google.com";
            createPaymentTokenRecord.API3G.Transaction.CompanyRefUnique = 0;
            createPaymentTokenRecord.API3G.Transaction.PTL = 96;
            createPaymentTokenRecord.API3G.Services.Service.ServiceType = 48565;
            createPaymentTokenRecord.API3G.Services.Service.ServiceDescription = "Flight from Nairobi to Diani";
            createPaymentTokenRecord.API3G.Services.Service.ServiceDate = "2022/12/20 19:00";
            var json =JsonConvert.SerializeObject(createPaymentTokenRecord);
            var Res = UIHelper.AddXMLRequestToServiceApi("https://secure.3gdirectpay.com/API/v6/", json);
            try
            {
                var result = Res.Content.ReadAsStringAsync().Result;
                var jsonRes = UIHelper.XmlToJsonRoot(result);
                var dpoPaymentIntegrationRecord = JsonConvert.DeserializeObject<CreatePaymentTokenResponse>(jsonRes);
                var url = $"https://secure.3gdirectpay.com/payv3.php?ID={dpoPaymentIntegrationRecord.API3G.TransToken}";
                _context.DpoPaymentIntegrations.Add(new DpoPaymentIntegration()
                {
                    CreatedAt = DateTime.Now,
                    OrderId = 500,
                    Result = dpoPaymentIntegrationRecord.API3G.Result,
                    ResultExplanation = dpoPaymentIntegrationRecord.API3G.ResultExplanation,
                    TransRef = dpoPaymentIntegrationRecord.API3G.TransRef,
                    TransToken = dpoPaymentIntegrationRecord.API3G.TransToken,
                });

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
            }
            return Ok("Done");
        }

        //[HttpGet]
        //[Route("AddPaymentTransaction")]
        //public ActionResult AddPaymentTransaction(string TransID,string CCDapproval,string PnrID,
        //    string TransactionToken,string CompanyRef)
        //{
        //   var paymentToken=  _context.DpoPaymentIntegrations.FirstOrDefault(c => c.TransToken == TransactionToken&&c.TransId==null);
        //    if(paymentToken!=null)
        //    {
        //        paymentToken.TransId = TransID;
        //        paymentToken.Ccdapproval = CCDapproval;
        //        paymentToken.PnrId = PnrID;
        //        paymentToken.CompanyRef = CompanyRef;
        //        _context.SaveChanges();
        //        return base.Content("<div>Payment Done Successfully</div>", "text/html");
        //    }
        //    return base.Content("<div>Transaction Token Not Found</div>", "text/html");
        //}

        [HttpPost]
        [Route("Addorder")]
        public ActionResult Addorder([FromBody] orderRequest request)
        {
            request._context = _context;

            var orderResponse = orderService.Addorder(request, _appSettings);
            return Ok(new
            {
                orderResponse
            });
        }






        [HttpPost]
        [Route("Editorder")]
        public ActionResult Editorder([FromBody] orderRequest request)
        {
            request._context = _context;

            var orderResponse = orderService.Editorder(request);
            return Ok(new
            {
                orderResponse
            });
        }

        //// GET: api/Orders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CrossCuttingLayer.DAL.DB.Order>>> GetOrders()
        //{
        //    return await _context.Orders.ToListAsync();
        //}

        //// GET: api/Orders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<CrossCuttingLayer.DAL.DB.Order>> GetOrder(long id)
        //{
        //    var order = await _context.Orders.FindAsync(id);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return order;
        //}

        //// PUT: api/Orders/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrder(long id, CrossCuttingLayer.DAL.DB.Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(order).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Orders
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<CrossCuttingLayer.DAL.DB.Order>> PostOrder(CrossCuttingLayer.DAL.DB.Order order)
        //{
        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        //}

        //// DELETE: api/Orders/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrder(long id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool OrderExists(long id)
        //{
        //    return _context.Orders.Any(e => e.Id == id);
        //}
    }
}
