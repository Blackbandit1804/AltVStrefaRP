﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AltV.Net.Mock;
using AltVStrefaRPServer.Models;
using AltVStrefaRPServer.Models.Inventory;
using AltVStrefaRPServer.Models.Inventory.Items;
using AltVStrefaRPServer.Models.Inventory.Responses;
using AltVStrefaRPServer.Services.Inventories;
using NUnit.Framework;
using StrefaRPServer.UnitTests.Core;

namespace StrefaRPServer.UnitTests.Modules.Inventory
{
    public class InventoryEquipServiceTest : ServerContextTestBase
    {
        private Character _character;
        private ItemFactory _itemFactory;
        private PlayerEquipment _playerEquipment;
        private InventoryContainer _inventoryContainer;
        private Equipmentable _equipmentableItem;
        private InventoryItem _itemToEquip;
        private InventoryDatabaseService _inventoryDatabaseService;
        private InventoryEquipService _inventoryEquipService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _itemFactory = new ItemFactory();
        }

        [SetUp]
        public void Setups()
        {
            _inventoryDatabaseService = new InventoryDatabaseService(_mockFactory.Object);
            _inventoryEquipService = new InventoryEquipService(_mockFactory.Object);

            var mockPlayer = new MockPlayer(IntPtr.Zero, 0);
            _character = new Character
            {
                Player = mockPlayer
            };

            _inventoryContainer = new PlayerInventoryContainer(20)
            {
                Owner = _character
            };

            _playerEquipment = new PlayerEquipment
            {
                Owner = _character
            };

            _character.Equipment = _playerEquipment;
            _character.Inventory = _inventoryContainer as PlayerInventoryContainer;

            _equipmentableItem = _itemFactory.CreateCombatPistol();
            _itemToEquip = new InventoryItem(_equipmentableItem, 1, 1);
        }

        [Test]
        public async Task EquipItemRemovesItemFromInventory()
        {
            await _inventoryContainer.AddNewInventoryItemAsync(_itemToEquip, _inventoryDatabaseService);

            var response = await _inventoryEquipService.EquipItemAsync(_inventoryContainer, _playerEquipment, _itemToEquip.Id);
            
            Assert.That(response, Is.EqualTo(InventoryEquipItemResponse.ItemEquipped));
            Assert.That(_inventoryContainer.Items.Contains(_itemToEquip), Is.False);
        }

        [Test]
        public async Task EquipItemRemovesItemFromInventoryInDatabase()
        {
            await _inventoryContainer.AddNewInventoryItemAsync(_itemToEquip, _inventoryDatabaseService);

            await _inventoryEquipService.EquipItemAsync(_inventoryContainer, _playerEquipment, _itemToEquip.Id);

            bool actual = (await _context.Inventories.FindAsync(_inventoryContainer.Id)).Items.Contains(_itemToEquip);
            Assert.That(actual, Is.False);
        }

        [Test]
        public async Task EquipItemChangesItemInvetoryId()
        {
            await _inventoryContainer.AddNewInventoryItemAsync(_itemToEquip, _inventoryDatabaseService);

            await _inventoryEquipService.EquipItemAsync(_inventoryContainer, _playerEquipment, _itemToEquip.Id);

            Assert.That(_itemToEquip.InventoryId, Is.EqualTo(_playerEquipment.Id));
        }

        [Test]
        public async Task CantEquipItemInNotCharacterEquipment()
        {
            var wrongCharacterEquipmentId = _character.Equipment.Id + 1;

            var response = await _inventoryEquipService.EquipItemAsync(_character, _inventoryContainer, wrongCharacterEquipmentId, _itemToEquip.Id);

            Assert.That(response, Is.EqualTo(InventoryEquipItemResponse.EquipmentInventoryNotFound));
        }
    }
}
