using Connect4Sports.CommonDefinitions.Records;
using System;
using System.Linq;
using System.Security.Policy;


namespace Connect4Sports.BL.Services.Managers
{
    public class orderServiceManager
    {
        public static CrossCuttingLayer.DAL.DB.Order AddOrEditorder(orderRecord p, CrossCuttingLayer.DAL.DB.Order order = null)
        {
            if (order == null)
{
order =new CrossCuttingLayer.DAL.DB.Order();
}
            if (p.providerId >0)
            {
                order.ProviderId = p.providerId;
            }
            if (p.numberOfSession > -1)
            {
                order.NumberOfSession = p.numberOfSession;
            }
            if (p.packageId > 0)
            {
                order.PackageId = p.packageId;
            }
            if (p.SportId > 0)
            {
                order.SportId = p.SportId;
            }
            if (p.totalCost!=null&&p.totalCost>0)
            {
                order.TotalCost = p.totalCost.Value;

            }
            if (p.Vat != null && p.Vat > 0)
            {
                order.Vat = p.Vat.Value;

            }
            if (p.Price != null && p.Price > 0)
            {
                order.Price = p.Price.Value;

            }
            if (!string.IsNullOrWhiteSpace(p.sport))
            {
                order.Sport = p.sport;

            }
            if (!string.IsNullOrWhiteSpace(p.Discount))
            {
                order.Discount = p.Discount;

            }
            if (!string.IsNullOrWhiteSpace(p.playerId))
            {
                order.PlayerId = p.playerId;

            }
            if (!string.IsNullOrWhiteSpace(p.ProviderType))
            {
                order.ProviderType = p.ProviderType;
            }

            if (!string.IsNullOrWhiteSpace(p.UserImageUrl))
            {
                order.UserImageUrl = p.UserImageUrl;
            }

            if (!string.IsNullOrWhiteSpace(p.BackUrl))
            {
                order.BackUrl = p.BackUrl;
            }
            if (!string.IsNullOrWhiteSpace(p.UserName))
            {
                order.UserName = p.UserName;
            }

            if (p.isPaid!=null)
            {
                order.IsPaid = p.isPaid;
            }
            if (p.EndOfLife != null&&p.EndOfLife>DateTime.MinValue)
            {
                order.EndOfLife = p.EndOfLife;
            }
            if (p.ExpireAt != null && p.ExpireAt > DateTime.MinValue)
            {
                order.ExpireAt = p.ExpireAt;
            }
            if (!string.IsNullOrWhiteSpace(p.PackageTypeName))
            {
                order.PackageTypeName = p.PackageTypeName;
            }
            if (!string.IsNullOrWhiteSpace(p.ProviderName))
            {
                order.ProviderName = p.ProviderName;
            }
            if (!string.IsNullOrWhiteSpace(p.ProviderObject))
            {
                order.ProviderObject = p.ProviderObject;
            }
            if (p.IsMonthly != null)
            {
                order.IsMonthly = p.IsMonthly;
            }
            if (p.Period != null)
            {
                order.Period = p.Period;
            }
            if (p.ValidityDays != null)
            {
                order.ValidityDays = p.ValidityDays;
            }
            if (!string.IsNullOrWhiteSpace(p.PlayerObject))
            {
                order.PlayerObject = p.PlayerObject;
            }
            if (!string.IsNullOrWhiteSpace(p.ProviderObject))
            {
                order.ProviderObject = p.ProviderObject;
            }
            if (!string.IsNullOrWhiteSpace(p.ProviderUserId))
            {
                order.ProviderUserId = p.ProviderUserId;
            }
            //public string PackageTitle { get; set; }
            if (!string.IsNullOrWhiteSpace(p.PackageObject))
            {
                order.PackageObject = p.PackageObject;
            }
            if (!string.IsNullOrWhiteSpace(p.SportObject))
            {
                order.SportObject = p.SportObject;
            }
            if (!string.IsNullOrWhiteSpace(p.CountryId))
            {
                order.CountryId = p.CountryId;
            }
            if (!string.IsNullOrWhiteSpace(p.OfferId))
            {
                order.OfferId = p.OfferId;
            }
            //public string PackageTitle { get; set; }
            if (!string.IsNullOrWhiteSpace(p.CountryObject))
            {
                order.CountryObject = p.CountryObject;
            }
            if (!string.IsNullOrWhiteSpace(p.OfferObject))
            {
                order.OfferObject = p.OfferObject;
            }

            if (p.TrainingTypeId > 0)
            {
                order.TrainingTypeId = p.TrainingTypeId;
            }
            if (p.PlaceId > 0)
            {
                order.PlaceId = p.PlaceId;
            }
            if (p.ZoneId != null && p.ZoneId > 0)
            {
                order.ZoneId = p.ZoneId;

            }
            if (!string.IsNullOrWhiteSpace(p.TrainingTypeName))
            {
                order.TrainingTypeName = p.TrainingTypeName;

            }
            if (!string.IsNullOrWhiteSpace(p.PlaceAddress))
            {
                order.PlaceAddress = p.PlaceAddress;

            }
            if (!string.IsNullOrWhiteSpace(p.PlaceName))
            {
                order.PlaceName = p.PlaceName;

            }
            if (!string.IsNullOrWhiteSpace(p.PlaceObject))
            {
                order.PlaceObject = p.PlaceObject;
            }

            if (p.SlotArray != null && p.SlotArray.Count > 0)
                order.SlotArray = string.Join(",", p.SlotArray);
            if (!string.IsNullOrWhiteSpace(p.ZoneObject))
            {
                order.ZoneObject = p.ZoneObject;
            }
            if (p.IsVirtual!=null)
            {
                order.IsVirtual = p.IsVirtual;
            }
            if (p.TransportationFees != null)
            {
                order.TransportationFees = p.TransportationFees;
            }
            return order;
        }

public static IQueryable<orderRecord> ApplyFilter(IQueryable<orderRecord> query, orderRecord record)
{
            if (record.id > 0)
            {
                query = query.Where(p => p.id == record.id);
            }
            if (record.SportId > 0)
            {
                query = query.Where(p => p.SportId == record.SportId);
            }
            if (!string.IsNullOrWhiteSpace(record.ProviderType))
            {
                query = query.Where(p => p.ProviderType != null && p.ProviderType.Contains(record.ProviderType));
            }
            if (!string.IsNullOrWhiteSpace(record.playerId))
            {
                query = query.Where(p => p.playerId != null && p.playerId.Contains(record.playerId));
            }
            if (!string.IsNullOrWhiteSpace(record.CountryId))
            {
                query = query.Where(p => p.CountryId != null && p.CountryId.Contains(record.CountryId));
            }
            if (!string.IsNullOrWhiteSpace(record.OfferId))
            {
                query = query.Where(p => p.OfferId != null && p.OfferId.Contains(record.OfferId));
            }
            if (record.isPaid != null)
            {
                query.Where(p => p.isPaid == record.isPaid);
            }
            return query;
}
    }
}