﻿using System.Threading.Tasks;
using AltVStrefaRPServer.Models.Inventory;
using AltVStrefaRPServer.Models.Inventory.Interfaces;
using AltVStrefaRPServer.Models.Inventory.Responses;

namespace AltVStrefaRPServer.Services.Inventories
{
    public class InventoryTransferService : IInventoryTransferService
    {
        private readonly IInventoryDatabaseService _inventoryDatabaseService;

        public InventoryTransferService(IInventoryDatabaseService inventoryDatabaseService)
        {
            _inventoryDatabaseService = inventoryDatabaseService;
        }

        public async Task<InventoryStackResponse> StackItemBetweenInventoriesAsync(IInventoryContainer source, IInventoryContainer receiver, 
            int itemToStackFromId, int itemToStackId)
        {
            var response = new InventoryStackResponse(type: InventoryStackResponseType.ItemsNotFound);
            if (!source.HasItem(itemToStackFromId, out var itemToStackFrom) || !receiver.HasItem(itemToStackId, out var itemToStack))
                return response;

            return response;
            //return await source.StackItemAsync(itemToStackFrom, itemToStack, _inventoryDatabaseService);

            //if (!InventoriesHelper.AreItemsStackable(itemToStackFrom, itemToStack)) return InventoryStackResponse.ItemsNotStackable;

            //var toAdd = source.CalculateNumberOfItemsToAdd(itemToStack.Item, itemToStackFrom.Quantity, itemToStack);
            //if (toAdd <= 0) return InventoryStackResponse.ItemsNotStackable;

            //if (await source.RemoveItemAsync(itemToStackFrom, toAdd, _inventoryDatabaseService, true) == InventoryRemoveResponse.NotEnoughItems)
            //    return InventoryStackResponse.ItemsNotFound;

            //itemToStack.AddToQuantity(toAdd);

            //return InventoryStackResponse.ItemsStacked;
        }

        public async Task TransferItemAsync(PlayerInventoryContainer source, PlayerInventoryContainer receiver, InventoryItem itemToTransfer, int quantity)
        {
            if (!source.HasItem(itemToTransfer) || itemToTransfer.Quantity < quantity) return;

            var addItemResponse = await receiver.AddItemAsync(itemToTransfer.Item, quantity, _inventoryDatabaseService).ConfigureAwait(false);
            if (addItemResponse.AnyChangesMade) return;

            var removeItemResponse = await source.RemoveItemAsync(itemToTransfer, addItemResponse.ItemsAddedCount, _inventoryDatabaseService).ConfigureAwait(false);
        }
    }
}
