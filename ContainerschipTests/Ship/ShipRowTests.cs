using Microsoft.VisualStudio.TestTools.UnitTesting;
using Containerschip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip.Tests
{
    [TestClass()]
    public class ShipRowTests
    {
        [TestMethod()]
        public void AddContainerToStacks_TwoValuableToOneStack_ShouldReturnFalse()
        {
            // Arrange
            bool expected = false;
            ShipRow shipRow = new ShipRow(1);
            shipRow.AddContainerToStacks(new ContainerValuable(4000));

            // Act
            bool actual = shipRow.AddContainerToStacks(new ContainerValuable(4000));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddContainerToStacks_TwoCoolableValuableToTwoStacks_ShouldReturnFalse()
        {
            // Arrange
            bool expected = false;
            ShipRow shipRow = new ShipRow(2);
            shipRow.AddContainerToStacks(new ContainerCoolableValuable(4000));

            // Act
            bool actual = shipRow.AddContainerToStacks(new ContainerCoolableValuable(4000));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddContainerToStacks_OneCoolableValuableOneValuableToTwoStacks_ShouldReturnFalse()
        {
            // Arrange
            bool expected = true;
            ShipRow shipRow = new ShipRow(2);
            shipRow.AddContainerToStacks(new ContainerCoolableValuable(4000));

            // Act
            bool actual = shipRow.AddContainerToStacks(new ContainerValuable(4000));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddContainerToStacks_TwoValuableToTwoStacks_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;
            ShipRow shipRow = new ShipRow(2);
            shipRow.AddContainerToStacks(new ContainerValuable(4000));

            // Act
            bool actual = shipRow.AddContainerToStacks(new ContainerValuable(4000));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddContainerToStacks_OneValuableOneNormalToStack_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;
            ShipRow shipRow = new ShipRow(1);
            shipRow.AddContainerToStacks(new ContainerValuable(4000));

            // Act
            bool actual = shipRow.AddContainerToStacks(new ContainerNormal(4000));

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}