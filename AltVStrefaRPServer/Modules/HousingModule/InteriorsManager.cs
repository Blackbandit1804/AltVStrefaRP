﻿using System;
using System.Collections.Generic;
using System.Linq;
using AltVStrefaRPServer.Helpers;
using AltVStrefaRPServer.Models.Houses;
using AltVStrefaRPServer.Models.Interfaces.Managers;
using AltVStrefaRPServer.Services.Housing;
using AltVStrefaRPServer.Services.Housing.Factories;
using Microsoft.Extensions.Logging;

namespace AltVStrefaRPServer.Modules.HousingModule
{
    public class InteriorsManager : IInteriorsManager
    {
        private Dictionary<int, Interior> _interiors;
        private readonly ILogger<InteriorsManager> _logger;
        private readonly IInteriorDatabaseService _interiorDatabaseService;
        private readonly IInteriorsFactoryService _interiorsFactoryService;

        public InteriorsManager(IInteriorDatabaseService interiorDatabaseService, IInteriorsFactoryService interiorsFactoryService, ILogger<InteriorsManager> logger)
        {
            _interiors = new Dictionary<int, Interior>();
            _interiorDatabaseService = interiorDatabaseService;
            _interiorsFactoryService = interiorsFactoryService;
            _logger = logger;

            InitializeInteriors();
            CreateDefaultInteriors();
        }

        private void CreateDefaultInteriors()
        {
            if (_interiors.Count > 0) return;
            
            try
            {
                _logger.LogInformation("Not found any interiors. Creating new default interiors");
                var newInteriors = _interiorsFactoryService.CreateDefaultInteriors().ToList();
                _interiorDatabaseService.AddNewInteriors(newInteriors);
                foreach (var interior in newInteriors)
                {
                    _interiors.TryAdd(interior.Id, interior);
                }
                _logger.LogInformation("Created {interiorsCount} default interiorsgit");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error in loading new default interiors");
                throw;
            }
        }

        public bool TryGetInterior(int interiorId, out Interior interior) => _interiors.TryGetValue(interiorId, out interior);

        public Interior GetInterior(int interiorId) =>
            _interiors.ContainsKey(interiorId) ? _interiors[interiorId] : null;
        
        private void InitializeInteriors()
        {
            var startTime = Time.GetTimestampMs();
            foreach (var interior in _interiorDatabaseService.GetAllInteriors().ToList())
            {
                _interiors.TryAdd(interior.Id, interior);
            }
            _logger.LogInformation("Loaded {interiorsCount} interiors from database in {elapsedTime} ms", _interiors.Count, Time.GetElapsedTime(startTime));
        }
    }
}