﻿using AltVStrefaRPServer.Models.Enums;
using AltVStrefaRPServer.Models.Fractions;
using AltVStrefaRPServer.Modules.Fractions;

namespace AltVStrefaRPServer.Services.Money
{
    public class TaxService : ITaxService
    {
        private readonly FractionManager _fractionManager;

        public TaxService(FractionManager fractionManager)
        {
            _fractionManager = fractionManager;
        }

        public float CalculatePriceAfterTax(float price, TransactionType transactionType)
        {
            if (!_fractionManager.TryToGetTownHallFraction(out TownHallFraction townHall)) return price;

            switch (transactionType)
            {
                case TransactionType.VehicleSell: case TransactionType.VehicleBuy:
                    return townHall.PriceAfterTax(price, townHall.VehicleTax);
                case TransactionType.BankDeposit: case TransactionType.BankWithdraw: case TransactionType.BankTransfer:
                    return price;
                case TransactionType.BuyingFurnitures: case TransactionType.BuyingProperties:
                    return townHall.PriceAfterTax(price, townHall.PropertyTax);
                case TransactionType.BuyingGuns:
                    return townHall.PriceAfterTax(price, townHall.GunTax);
                default:
                    return townHall.PriceAfterTax(price, townHall.GlobalTax);
            }
        }
    }
}