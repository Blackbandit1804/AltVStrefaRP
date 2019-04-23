﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltVStrefaRPServer.Database;
using AltVStrefaRPServer.Helpers;
using AltVStrefaRPServer.Models;
using AltVStrefaRPServer.Models.Businesses;
using AltVStrefaRPServer.Models.Enums;
using AltVStrefaRPServer.Services.Businesses;
using Microsoft.EntityFrameworkCore;

namespace AltVStrefaRPServer.Modules.Businesses
{
    public class BusinessManager
    {
        private IBusinessService _businessService;
        private Dictionary<int, Business> Businesses = new Dictionary<int, Business>();
        private ServerContext _serverContext;
        private BusinessFactory _businessFactory;

        public BusinessManager(IBusinessService businessService, ServerContext serverContext)
        {
            _businessService = businessService;
            _serverContext = serverContext;
            _businessFactory = new BusinessFactory();

            LoadBusinesses();
        }

        private void LoadBusinesses()
        {
            var startTime = Time.GetTimestampMs();
            foreach (var business in _serverContext.Businesses.AsNoTracking().ToList())
            {
                //Businesses.TryAdd(business.Id, _businessFactory.CreateNewBusiness(business));
                Businesses.TryAdd(business.Id, business);
                //_businessFactory.CreateNewBusiness(business);
            }
            Alt.Log($"Loaded {Businesses.Count} businesses from database in {Time.GetTimestampMs() - startTime}ms.");
        }

        /// <summary>
        /// Get business by it's id
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public Business GetBusiness(int businessId) => Businesses.ContainsKey(businessId) ? Businesses[businessId] : null;

        /// <summary>
        /// Gets all owned businesses for given character id
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public List<Business> GetCharacterBusinesses(int ownerId) => Businesses.Values.Where(b => b.OwnerId == ownerId).ToList();

        /// <summary>
        /// Get nearest business to player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Business GetNearestBusiness(IPlayer player)
        {
            Business nearestBusiness = null;
            foreach (var business in Businesses.Values)
            {
                if (nearestBusiness == null)
                {
                    nearestBusiness = business;
                    break;
                }
                else
                {
                    if (business.GetPosition().Distance(player.Position) < nearestBusiness.GetPosition().Distance(player.Position))
                    {
                        nearestBusiness = business;
                    }
                }
            }
            return nearestBusiness;
        }

        /// <summary>
        /// Check if business with given name is already registered
        /// </summary>
        /// <param name="businessName"></param>
        /// <returns></returns>
        public bool CheckIfBusinessExists(string businessName)
        {
            return Businesses.Values.Any(b => b.BusinessName == businessName);
        }

        public BusinessRank GetBusinessRankForPlayer(Character character)
        {
            return character.Business.BusinessRanks.FirstOrDefault(q => q.Id == character.BusinessRank);
        }

        /// <summary>
        /// Create new business and save it to database
        /// </summary>
        /// <param name="businessType">Type of the business <see cref="BusinessType"/></param>
        /// <param name="position">Position where the business will be located</param>
        /// <param name="name">Name of the business</param>
        /// <returns></returns>
        public async Task<bool> CreateNewBusinessAsync(int ownerId, BusinessType businessType, Position position, string name)
        {
            var startTime = Time.GetTimestampMs();
            if(businessType == BusinessType.None || string.IsNullOrEmpty(name)) return false;
            if (CheckIfBusinessExists(name)) return false;

            var business = _businessFactory.CreateNewBusiness(ownerId, businessType, position, name);
            await _businessService.AddNewBusinessAsync(business).ConfigureAwait(false);
            Businesses.Add(business.Id, business);
            Alt.Log($"Created new business ID({business.Id}) Name({business.BusinessName}) in {Time.GetTimestampMs() - startTime}ms.");
            return true;
        }

        public async Task<bool> UpdateBusinessOwner(Business business, Character newOwner)
        {
            if (newOwner.Business != business)
            {
                business.AddNewMember(newOwner);
            }
            await _businessService.UpdateOwnerAsync(business, newOwner).ConfigureAwait(false);
            return true;
        }
    }
}
