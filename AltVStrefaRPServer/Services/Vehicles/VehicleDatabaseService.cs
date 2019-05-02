﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltVStrefaRPServer.Database;
using AltVStrefaRPServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AltVStrefaRPServer.Services.Vehicles
{
    public class VehicleDatabaseService : IVehicleDatabaseService
    {
        private ServerContext _serverContext;

        public VehicleDatabaseService(ServerContext serverContext)
        {
            _serverContext = serverContext;
        }

        /// <summary>
        /// Gets all vehicles from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<VehicleModel>> LoadVehiclesFromDatabaseAsync()
            => await _serverContext.Vehicles.ToListAsync().ConfigureAwait(false);

        public List<VehicleModel> LoadVehiclesFromDatabase()
            => _serverContext.Vehicles.ToList();

        public async Task RemoveVehicleAsync(VehicleModel vehicle)
        {
            _serverContext.Vehicles.Remove(vehicle);
            await _serverContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public void SaveVehicle(VehicleModel vehicle)
        {
            _serverContext.Vehicles.Update(vehicle);
            _serverContext.SaveChanges();
        }

        public async Task SaveVehicleAsync(VehicleModel vehicle)
        {
            _serverContext.Vehicles.Update(vehicle);
            await _serverContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}