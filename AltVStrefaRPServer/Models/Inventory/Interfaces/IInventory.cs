﻿using System.Collections.Generic;
using AltVStrefaRPServer.Models.Inventory.Items;

namespace AltVStrefaRPServer.Models.Inventory
{
    public interface IInventory
    {
        int Id { get; set; }
        int MaxSlots { get; set; }
        IReadOnlyCollection<InventoryItem> Items { get; }
        bool HasEmptySlots();
        bool HasItem(int id, out InventoryItem item);
        bool HasItem<TItem>() where TItem : BaseItem;
        bool TryToGetInventoryItemWithoutFullStack(BaseItem item, out InventoryItem inventoryItem);
    }
}