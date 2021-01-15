using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Com.Technitaur.GreenBean.Inventory;
using Com.Technitaur.GreenBean.Core;

namespace Tests
{
    public class InventoryTests
    {
        public GameObject player = null;
        public PlayerInventory inventory = null;
        public Dictionary<ItemType, IInventoryItem> items = new Dictionary<ItemType, IInventoryItem>();

        [OneTimeSetUp]
        public void SetUp()
        {
            player = CreateTestPlayer("Test Player");
            inventory = CreateTestInventory(player);
            items = CreateItemDictionary();
        }
        
        private GameObject CreateTestPlayer(string name)
        {
            var newPlayer = new GameObject();
            newPlayer.name = name;
            newPlayer.transform.position = Vector3.zero;
            return newPlayer;
        }
        
        private PlayerInventory CreateTestInventory(GameObject player)
        {
            return player.AddComponent<PlayerInventory>();
        }
        
        private Dictionary<ItemType, IInventoryItem> CreateItemDictionary()
        {
            IInventoryItem wand = new FakeInventoryItem("wand", ItemType.Wand, 100, 0);
            IInventoryItem sword = new FakeInventoryItem("sword", ItemType.Sword, 50, 2000);
            IInventoryItem torch = new FakeInventoryItem("torch", ItemType.Torch, 500, 0);
            IInventoryItem redKey = new FakeInventoryItem("red key", ItemType.KeyRed, 50, 300);
            IInventoryItem cyanKey = new FakeInventoryItem("cyan key", ItemType.KeyCyan, 50, 300);
            IInventoryItem violetKey = new FakeInventoryItem("violet key", ItemType.KeyViolet, 50, 300);
            Dictionary<ItemType, IInventoryItem> items = new Dictionary<ItemType, IInventoryItem>()
            {
                { ItemType.Wand, wand },
                { ItemType.Sword, sword },
                { ItemType.Torch, torch },
                { ItemType.KeyCyan, cyanKey },
                { ItemType.KeyRed, redKey },
                { ItemType.KeyViolet, violetKey }
            };
            return items;
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            player = null;
            inventory = null;
            items = null;
        }

        [Test]
        [Order(1)]
        public void InventoryCanReceiveNewItems()
        {
            inventory.Add(items[ItemType.Sword]);
            Assert.AreNotEqual(0, inventory.Count);
        }

        [Test]
        [Order(2)]
        public void InventoryCountIsAccurateTo1()
        {
            Assert.AreEqual(1, inventory.Count);
        }

        [Test]
        [Order(3)]
        public void InventoryCountIsAccurateTo2()
        {
            inventory.Add(items[ItemType.KeyCyan]);
            Assert.AreEqual(2, inventory.Count);
        }

        [Test]
        [Order(4)]
        public void InventoryCountIsAccurateTo3()
        {
            inventory.Add(items[ItemType.KeyViolet]);
            Assert.AreEqual(3, inventory.Count);
        }

        [Test]
        [Order(5)]
        public void InventoryCountIsAccurateTo4()
        {
            inventory.Add(items[ItemType.KeyRed]);
            Assert.AreEqual(4, inventory.Count);
        }

        [Test]
        [Order(6)]
        public void InventoryCountIsAccurateTo5()
        {
            inventory.Add(items[ItemType.Torch]);
            Assert.AreEqual(5, inventory.Count);
        }
        
        [Test]
        [Order(7)]
        public void ItemsAreInOrderEntered()
        {
            Assert.AreEqual("sword", inventory.items[0].Name);
            Assert.AreEqual("cyan key", inventory.items[1].Name);
            Assert.AreEqual("violet key", inventory.items[2].Name);
            Assert.AreEqual("red key", inventory.items[3].Name);
            Assert.AreEqual("torch", inventory.items[4].Name);
        }
    }
}