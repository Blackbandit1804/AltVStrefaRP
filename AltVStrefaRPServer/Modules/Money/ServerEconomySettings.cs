﻿using AltVStrefaRPServer.Models;

namespace AltVStrefaRPServer.Modules.Money
{
    public static class ServerEconomySettings
    {
        public static float VehicleTax { get; private set; } = 0.15f;
        public static float PropertyTax { get; private set; } = 0.22f;
        public static float GunTax { get; private set; } = 0.18f;
        public static float GlobalTax { get; private set; } = 0.09f;

        public static bool SetVehicleTax(float newTax)
        {
            if (newTax > AppSettings.Current.ServerConfig.EconomySettings.VehicleTaxSettings.Max ||
                newTax < AppSettings.Current.ServerConfig.EconomySettings.VehicleTaxSettings.Min)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool SetPropertyTax(float newTax)
        {
            if (newTax > AppSettings.Current.ServerConfig.EconomySettings.PropertyTax.Max ||
                newTax < AppSettings.Current.ServerConfig.EconomySettings.PropertyTax.Min)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool SetGunTax(float newTax)
        {
            if (newTax > AppSettings.Current.ServerConfig.EconomySettings.GunTax.Max ||
                newTax < AppSettings.Current.ServerConfig.EconomySettings.GunTax.Min)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool SetGlobalTax(float newTax)
        {
            if (newTax > AppSettings.Current.ServerConfig.EconomySettings.GlobalTax.Max ||
                newTax < AppSettings.Current.ServerConfig.EconomySettings.GlobalTax.Min)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}