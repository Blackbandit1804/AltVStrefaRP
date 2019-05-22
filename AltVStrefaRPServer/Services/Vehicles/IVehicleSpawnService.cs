﻿using System.Threading.Tasks;
using AltVStrefaRPServer.Models;

namespace AltVStrefaRPServer.Services.Vehicles
{
    public interface IVehicleSpawnService
    {
        Task SpawnVehicleAsync(VehicleModel vehicleModel);
        void SpawnVehicle(VehicleModel vehicleModel);
        Task DespawnVehicleAsync(VehicleModel vehicleModel);
        void DespawnVehicle(VehicleModel vehicleModel);
    }
}
