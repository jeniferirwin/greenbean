using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Com.Technitaur.GreenBean.Player;

namespace Tests
{
    public class InventoryTests
    {
        Inventory testInventory;

        [OneTimeSetUp]
        
        public void OneTimeSetUp()
        {
            GameObject invTester = new GameObject();
            testInventory = invTester.AddComponent<Inventory>();
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void InventoryTestsSimplePasses()
        {
            // Use the Assert class to test conditions
            var test = testInventory.TestMethod(5);
            Assert.IsTrue(test == 7);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator InventoryTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
