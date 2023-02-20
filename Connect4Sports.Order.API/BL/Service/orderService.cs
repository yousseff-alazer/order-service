using Connect4Sports.BL.Services.Managers;
using Connect4Sports.CommonDefinitions.Records;
using Connect4Sports.CommonDefinitions.Requests;
using Connect4Sports.CommonDefinitions.Responses;
//using Connect4Sports.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Connect4Sports.Order.API.BL.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reservation.CommonDefinitions.Records;
using CrossCuttingLayer.DAL.DB;
using CrossCuttingLayer.CommonDefinitions.Enums;
using Connect4Sports.Order.API.Order.Helpers;
using System.Reflection.Metadata;
using static MassTransit.ValidationResultExtensions;
using CrossCuttingLayer.CommonDefinitions.Record;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Connect4Sports.BL.Services
{
    public class orderService : BaseService
    {
        //private static string ChallengeBaseUrl = "http://34.192.91.241:94/api/Challenge/GetPackagePrice";
        //private static string VenueBaseUrl = "http://34.192.91.241:91/api/Packs/GetPackagePrice";
        //private static string CoachBaseUrl = "http://34.192.91.241:85/api/Coaches/GetPackagePrice";
        //private static string NutritionistBaseUrl = "http://34.192.91.241:86/api/Packs/GetPackagePrice";
        //private static string PhysiotherapistBaseUrl = "http://34.192.91.241:88/api/Packs/GetPackagePrice";



        #region orderServices
        public static orderResponse Listorder(orderRequest request)
        {
            var res = new orderResponse();
            RunBase(request, res, (orderRequest req) =>
            {
                try
                {
                    var query = request._context.Orders.Select(p => new orderRecord
                    {

                        id = p.Id,
                        totalCost = p.TotalCost,
                        sport = p.Sport,
                        providerId = p.ProviderId,
                        packageId = p.PackageId,
                        playerId = p.PlayerId,
                        isPaid = p.IsPaid ?? false,
                        createdAt = p.CreatedAt,
                        updatedAt = p.UpdatedAt,
                        numberOfSession = p.NumberOfSession != null ? p.NumberOfSession : 0,
                        ProviderType = p.ProviderType,
                        ProviderObject = p.ProviderObject,
                        EndOfLife = p.EndOfLife,
                        IsMonthly = p.IsMonthly ?? false,
                        Period = p.Period,
                        ValidityDays = p.ValidityDays,
                        PlayerObject = p.PlayerObject,
                        ProviderName = p.ProviderName,
                        SportId = p.SportId,
                        UserName = p.UserName,
                        UserImageUrl = p.UserImageUrl,
                        PackageTypeName = p.PackageTypeName,
                        ExpireAt = p.ExpireAt,
                        ProviderUserId = p.ProviderUserId,
                        PackageObject = p.PackageObject,
                        SportObject = p.SportObject,
                        BackUrl = p.BackUrl,
                        PaymentUrl = p.PaymentUrl,
                        CountryId = p.CountryId,
                        CountryObject = p.CountryObject,
                        OfferObject = p.OfferObject,
                        OfferId = p.OfferId,
                        Discount = p.Discount,
                        Vat = p.Vat,
                        TrainingTypeId=p.TrainingTypeId,
                        TrainingTypeName=p.TrainingTypeName,
                        PlaceId = p.PlaceId,
                         PlaceAddress = p.PlaceAddress,
                         PlaceName = p.PlaceName,
                         PlaceObject = p.PlaceObject,
                         IsVirtual = p.IsVirtual,
                         Price = p.Price,
                        SlotArray = !string.IsNullOrWhiteSpace(p.SlotArray) ? new List<string> { p.SlotArray } : null,
                        ZoneId = p.ZoneId,
                         ZoneObject = p.ZoneObject,
                         TransportationFees=p.TransportationFees
                         

                    });


                    if (request.orderRecord != null)
                        query = orderServiceManager.ApplyFilter(query, request.orderRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn ?? "id", request.IsDesc);


                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.orderRecords = query.ToList();
                    foreach(var item in res.orderRecords)
                    {
                        if (!string.IsNullOrWhiteSpace(item.OfferObject))
                        {
                             var myDeserializedClass = JsonConvert.DeserializeObject<OfferAPIRecord>(item.OfferObject);

                        }
                    }

                    res.Message = HttpStatusCode.OK.ToString();
                    res.Success = true;
                    res.StatusCode = HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message;
                    res.Success = false;
                    //LogHelper.LogException(ex.Message, ex.StackTrace);
                }
                return res;
            });
            return res;
        }
        //  public static orderResponse Deleteorder(orderRequest request)
        //{

        //    var res = new orderResponse();
        //    RunBase(request, res, (orderRequest req) =>
        //    {
        //        try
        //        {
        //            var model = request.orderRecord;
        //            var order = request._context.Orders.FirstOrDefault(c =>  c.id == model.id);
        //            if (order != null)
        //            {
        //                //update Agency IsDeleted
        //                request._context.SaveChanges();

        //                res.Message = MessageKey.DeletedSuccessfully.ToString();
        //                res.Success = true;
        //                res.StatusCode = HttpStatusCode.OK;
        //            }
        //            else
        //            {
        //                res.Message = MessageKey.InvalidDate.ToString();
        //                res.Success = false;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            res.Message = ex.Message;
        //            res.Success = false;
        //            //LogHelper.LogException(ex.Message, ex.StackTrace);
        //        }
        //        return res;
        //    });
        //    return res;
        //}


        public static orderResponse Addorder(orderRequest request, Microsoft.Extensions.Options.IOptions<AppSettingsRecord> _appSettings)
        {

            var res = new orderResponse();
            RunBase(request, res, (orderRequest req) =>
            {
                try
                {
                    var PackagePriceRecord = new PackagePriceRecord();
                    var model = request.orderRecord;
                    if ((model.SportId == null || model.SportId == 0 || model.providerId == 0 /*|| model.packageId == 0*/ || string.IsNullOrWhiteSpace(model.ProviderType)) && model.ProviderType != nameof(ProviderType.VENUES))
                    {
                        res.Message = "something missing order not added";
                        res.Success = false;
                        //res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        //if(model.PackageTypeName!= null && model.PackageTypeName.ToLower().Contains("membership"))
                        //{
                        //    if (IsPackageExpire(request))
                        //    {
                        //        res.Message = "you are already subscribe on " + model.PackageTypeName + " Package";
                        //        res.Success = false;
                        //        //res.StatusCode = HttpStatusCode.OK;
                        //        return res;
                        //    }
                        //}

                        if (model.ProviderType == nameof(ProviderType.CHALLENGE))
                            PackagePriceRecord = GetPaidPrice(model.providerId, model.ProviderType, model.numberOfSession, model.IsMonthly.Value, _appSettings,null,0,0,0,0);
                        else
                        {
                            if(model.SlotArray!=null&&model.SlotArray.Count()>0)
                            {
                                PackagePriceRecord = GetPaidPrice(model.packageId, model.ProviderType, model.numberOfSession, model.IsMonthly.Value, _appSettings, model.ZoneId,model.SlotArray.Count(),model.providerId,model.SportId,model.TrainingTypeId);
                            }
                            else
                            {
                                PackagePriceRecord = GetPaidPrice(model.packageId, model.ProviderType, model.numberOfSession, model.IsMonthly.Value, _appSettings, model.ZoneId,0,0,0, model.TrainingTypeId);
                            }
                        }

                        decimal? vat = 0;
                        if (PackagePriceRecord.price > 0 && !string.IsNullOrEmpty(model.CountryObject))
                        {
                            vat = GetVat(model.CountryObject, PackagePriceRecord.price);

                        }
                        decimal? discount = 0;
                        if (PackagePriceRecord.price > 0 && !string.IsNullOrEmpty(model.OfferObject))
                        {
                            discount = GetDiscount(model.OfferObject, PackagePriceRecord.price);

                        }

                        if (PackagePriceRecord.isSuccess)
                        {
                            if (PackagePriceRecord.price > 0)
                            {
                                model.Price = (decimal?)PackagePriceRecord.price;
                                model.TransportationFees = PackagePriceRecord.TransportFee;
                                if (vat != null)
                                {
                                    model.Vat = vat;
                                    model.totalCost = model.Price + vat;
                                    if (model.TransportationFees != null)
                                    {
                                        model.totalCost = model.totalCost + model.TransportationFees;
                                    }
                                }
                                else
                                {
                                    model.totalCost = model.Price;
                                    if (model.TransportationFees != null)
                                    {
                                        model.totalCost = model.totalCost + model.TransportationFees;
                                    }
                                }
                                if (discount != null)
                                {
                                    model.Discount = discount.ToString();
                                    model.totalCost = model.totalCost - discount;
                                }
                            }

                            //CreatePaymentToken(request._context,model);
                            var order = orderServiceManager.AddOrEditorder(model);
                            request._context.Orders.Add(order);
                            request._context.SaveChanges();
                            try
                            {
                                //order.TotalCost = 0.5M;
                                CreatePaymentToken(request._context, order, _appSettings.Value.PaymentToken);
                                //CreatePaymentToken(request._context, order);
                            }
                            catch (Exception ex)
                            {
                                res.Message = "something went wrong ,Please try again";
                                res.Success = false;
                                var jsonRequest = System.Text.Json.JsonSerializer.Serialize(request);
                                LogHelper.LogException("Order", "AddOrder", jsonRequest, ex);
                                //return res;
                            }
                            res.Message = "Added Successfully and you will pay " + order.TotalCost;
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                            var orderRecord = new orderRecord()
                            {

                                id = order.Id,
                                totalCost = order.TotalCost,
                                sport = order.Sport,
                                providerId = order.ProviderId,
                                packageId = order.PackageId,
                                playerId = order.PlayerId,
                                isPaid = order.IsPaid ?? false,
                                createdAt = order.CreatedAt,
                                updatedAt = order.UpdatedAt,
                                numberOfSession = order.NumberOfSession,
                                ProviderType = order.ProviderType,
                                ProviderName = order.ProviderName,
                                ProviderObject = order.ProviderObject,
                                EndOfLife = order.EndOfLife,
                                IsMonthly = order.IsMonthly ?? false,
                                Period = order.Period,
                                ValidityDays = order.ValidityDays,
                                PlayerObject = order.PlayerObject,
                                SportId = order.SportId,
                                BackUrl = order.BackUrl,
                                PaymentUrl = order.PaymentUrl,
                                CountryId = order.CountryId,
                                OfferId = order.OfferId,
                                Discount = order.Discount,
                                Price = order.Price,
                                Vat = order.Vat,
                                CountryObject=order.CountryObject,
                                PackageObject=order.PackageObject,
                                TrainingTypeId = order.TrainingTypeId,
                                TrainingTypeName = order.TrainingTypeName,
                                PlaceId = order.PlaceId,
                                PlaceAddress = order.PlaceAddress,
                                PlaceName = order.PlaceName,
                                PlaceObject = order.PlaceObject,
                                IsVirtual = order.IsVirtual,
                                SlotArray = !string.IsNullOrWhiteSpace(order.SlotArray) ? new List<string> { order.SlotArray } : null,
                                ZoneId = order.ZoneId,
                                ZoneObject = order.ZoneObject

                            };
                            res.orderRecords = new List<orderRecord>
                            {
                                orderRecord
                            };
                        }
                        else
                        {
                            res.Message = $"something went wrong , missing data + pack ={PackagePriceRecord.TransportFee}{PackagePriceRecord.price}{PackagePriceRecord.isSuccess}";
                            res.Success = false;
                        }

                    }
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message;
                    res.Success = false;
                    //LogHelper.LogException(ex.Message, ex.StackTrace);
                }
                return res;
            });
            return res;
        }


        private static void CreatePaymentToken(orderContext context, CrossCuttingLayer.DAL.DB.Order model, PaymentToken paymentTokenRecord)
        {
            try
            {

                var createPaymentTokenRecord = new CreatePaymentTokenRecord();
                createPaymentTokenRecord.API3G = new API3G();
                createPaymentTokenRecord.API3G.Transaction = new Transaction();
                createPaymentTokenRecord.API3G.Services = new CrossCuttingLayer.CommonDefinitions.Record.Services();
                createPaymentTokenRecord.API3G.Services.Service = new Service();
                var playerObject = JsonConvert.DeserializeObject<PlayerRMQRecord>(model.PlayerObject);
                if (playerObject != null)
                {
                    createPaymentTokenRecord.API3G.Transaction.customerEmail = playerObject.Email;
                    createPaymentTokenRecord.API3G.Transaction.customerFirstName = playerObject.FirstName;
                    createPaymentTokenRecord.API3G.Transaction.customerLastName = playerObject.LastName;
                    //createPaymentTokenRecord.API3G.Transaction.customerPhone = playerObject.MobileNumber;
                    createPaymentTokenRecord.API3G.Transaction.customerAddress = playerObject.Location;
                }
                createPaymentTokenRecord.API3G.Transaction.PaymentAmount = (double)model.TotalCost;
                createPaymentTokenRecord.API3G.CompanyToken = paymentTokenRecord.CompanyToken;
                createPaymentTokenRecord.API3G.Request = paymentTokenRecord.Request;
                createPaymentTokenRecord.API3G.Transaction.PaymentAmount = Convert.ToDouble((string.Format("{0:0.00}", model.TotalCost)));
                createPaymentTokenRecord.API3G.Transaction.PaymentCurrency = nameof(Currency.AED);
                createPaymentTokenRecord.API3G.Transaction.CompanyRef = paymentTokenRecord.CompanyRef;
                createPaymentTokenRecord.API3G.Transaction.RedirectURL = paymentTokenRecord.RedirectURL;//TODO
                createPaymentTokenRecord.API3G.Transaction.BackURL = model.BackUrl;//TODO
                createPaymentTokenRecord.API3G.Transaction.CompanyRefUnique = paymentTokenRecord.CompanyRefUnique;
                createPaymentTokenRecord.API3G.Transaction.PTL = paymentTokenRecord.PTL;
                createPaymentTokenRecord.API3G.Services.Service.ServiceType = paymentTokenRecord.ServiceType;
                createPaymentTokenRecord.API3G.Services.Service.ServiceDescription = paymentTokenRecord.ServiceDescription;
                createPaymentTokenRecord.API3G.Services.Service.ServiceDate = "2022/12/20 19:00";//TODO
                var json = JsonConvert.SerializeObject(createPaymentTokenRecord);
                var Res = UIHelper.AddXMLRequestToServiceApi(paymentTokenRecord.DirectPayUrl, json);

                var result = Res.Content.ReadAsStringAsync().Result;
                var jsonRes = UIHelper.XmlToJsonRoot(result);
                var dpoPaymentIntegrationRecord = JsonConvert.DeserializeObject<CreatePaymentTokenResponse>(jsonRes);
                //var dpoPaymentIntegration = new DpoPaymentIntegration()
                //{
                //    CreatedAt = DateTime.Now,
                //    OrderId = model.Id,
                //    Result = dpoPaymentIntegrationRecord.API3G.Result,
                //    ResultExplanation= dpoPaymentIntegrationRecord.API3G.ResultExplanation,
                //    TransRef= dpoPaymentIntegrationRecord.API3G.TransRef,
                //    TransToken= dpoPaymentIntegrationRecord.API3G.TransToken,
                //};
                context.DpoPaymentIntegrations.Add(new DpoPaymentIntegration()
                {
                    CreatedAt = DateTime.Now,
                    OrderId = model.Id,
                    Result = dpoPaymentIntegrationRecord.API3G.Result,
                    ResultExplanation = dpoPaymentIntegrationRecord.API3G.ResultExplanation,
                    TransRef = dpoPaymentIntegrationRecord.API3G.TransRef,
                    TransToken = dpoPaymentIntegrationRecord.API3G.TransToken,
                });
                var url = paymentTokenRecord.PaymentUrl + dpoPaymentIntegrationRecord.API3G.TransToken;
                model.PaymentUrl = url;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public static orderResponse Editorder(orderRequest request)
        {

            var res = new orderResponse();
            RunBase(request, res, (orderRequest req) =>
            {
                try
                {
                    var model = request.orderRecord;
                    var order = request._context.Orders.Find(model.id);
                    if (order != null)
                    {
                        //update whole Agency
                        order = orderServiceManager.AddOrEditorder(request.orderRecord, order);
                        request._context.SaveChanges();

                        res.Message = "UpdatedSuccessfully";
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid";
                        res.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message;
                    res.Success = false;
                    //LogHelper.LogException(ex.Message, ex.StackTrace);
                }
                return res;
            });
            return res;
        }

        public static bool EditPlayerInfo(orderContext _context, PlayerRMQRecord record)
        {
            try
            {
                var playerList = _context.Orders.Where(s => s.PlayerId == record.Id).ToList();

                Parallel.ForEach(playerList, player =>
                {
                    player.UserImageUrl = record.ImageUrl;
                    player.UserName = record.Name;
                });
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private static bool IsPackageExpire(orderRequest request)
        {
            var model = request.orderRecord;
            var IsExpire = request._context.Orders.Any(s => s.PackageTypeName == model.PackageTypeName
                                                         && s.ProviderId == model.providerId
                                                         && s.ProviderType == model.ProviderType
                                                         && s.ExpireAt > DateTime.UtcNow
                                                         && s.PlayerId == model.playerId
                                                         && s.IsPaid.Value);

            return IsExpire;
        }
        public static PackagePriceRecord GetPaidPrice(long packageId, string providerType, int? numberOfSessions, bool isMonthly, Microsoft.Extensions.Options.IOptions<AppSettingsRecord> _appSettings, int? zoneId, int slotCount, long providerId, long? sportId, int trainingTypeId)
        {
            var packagePriceRecord = new PackagePriceRecord();
            string result = string.Empty;
            string Url = string.Empty;
            if (numberOfSessions == null)
            {
                numberOfSessions = 0;
            }
            if (zoneId == null)
            {
                zoneId = 0;
            }
            switch (providerType)
            {
                case nameof(ProviderType.CHALLENGE):
                    Url = _appSettings.Value.Urls.Challenge + "?challenegeId=" + packageId;
                    result = UIHelper.CreateRequest(Url, HttpMethod.Get, Url).Content.ReadAsStringAsync().Result;
                    break;

                case nameof(ProviderType.COACH):
                    //zoneId
                    //int slotCount, long providerId, long? sportId
                    Url = _appSettings.Value.Urls.Coach + "?packageId=" + packageId + "&numberOfSessions=" + numberOfSessions + "&isMonthly=" + isMonthly + "&zoneId=" + zoneId + "&slotCount=" + slotCount + "&providerId=" + providerId + "&sportId=" + sportId + "&trainingTypeId=" + trainingTypeId;
                    result = UIHelper.CreateRequest(Url, HttpMethod.Get, Url).Content.ReadAsStringAsync().Result;
                    break;

                case nameof(ProviderType.VENUES):
                    Url = _appSettings.Value.Urls.Venue + "?packageId=" + packageId + "&numberOfSessions=" + numberOfSessions;
                    result = UIHelper.CreateRequest(Url, HttpMethod.Get, Url).Content.ReadAsStringAsync().Result;
                    break;

                case nameof(ProviderType.PHYSIOTHERAPIST):
                    Url = _appSettings.Value.Urls.Physiotherapist + "?packageId=" + packageId + "&numberOfSessions=" + numberOfSessions + "&isMonthly=" + isMonthly + "&zoneId=" + zoneId + "&slotCount=" + slotCount + "&providerId=" + providerId + "&sportId=" + sportId + "&trainingTypeId=" + trainingTypeId;
                    result = UIHelper.CreateRequest(Url, HttpMethod.Get, Url).Content.ReadAsStringAsync().Result;
                    break;

                case nameof(ProviderType.NUTRITIONIST):
                    Url = _appSettings.Value.Urls.Nutritionist + "?packageId=" + packageId + "&numberOfSessions=" + numberOfSessions + "&isMonthly=" + isMonthly + "&zoneId=" + zoneId + "&slotCount=" + slotCount + "&providerId=" + providerId + "&sportId=" + sportId + "&trainingTypeId=" + trainingTypeId;
                    result = UIHelper.CreateRequest(Url, HttpMethod.Get, Url).Content.ReadAsStringAsync().Result;
                    break;

                default:
                    break;

            }

            var root = JsonConvert.DeserializeObject<Root>(result);
            return root.packagePriceRecord;
        }
        public static decimal? GetVat(string countryObject, double price)
        {
            if (!string.IsNullOrWhiteSpace(countryObject) && price > 0)
            {
                var countryObj = JsonConvert.DeserializeObject<CountryRecord>(countryObject);
                if (countryObj.IsPercentageVat != null)
                {
                    if (countryObj.IsPercentageVat != null)
                    {
                        var vat = ((decimal)price * countryObj.Vat) / 100;
                        return vat;
                    }
                    else
                    {
                        return countryObj.Vat;
                    }
                }
            }
            return 0;
        }
        public static decimal? GetDiscount(string offerObject, double price)
        {
            if (!string.IsNullOrWhiteSpace(offerObject) && price > 0)
            {
                var offerObj = JsonConvert.DeserializeObject<OfferAPIRecord>(offerObject);
                if (offerObj.Ispercentage != null&&!string.IsNullOrWhiteSpace(offerObj.Discount))
                {
                    var dis = Convert.ToDecimal(offerObj.Discount);
                    var pr = (decimal)price;
                    if (offerObj.Ispercentage.Value == true)
                    {
                        var discount = (pr * dis) / 100;
                        return discount;
                    }
                    else if(pr>dis)
                    {
                        return Convert.ToDecimal(offerObj.Discount);
                    }
                }
            }
            return 0;
        }

        //c.Discount > 0 ? c.Price - (c.Price* c.Discount) / 100 : 0,
        #endregion
    }
}





