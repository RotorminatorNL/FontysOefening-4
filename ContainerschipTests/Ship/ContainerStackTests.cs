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
    public class ContainerStackTests
    {
        [TestMethod()]
        public void AddContainerToList_TwoValuable_ShouldReturnFalse()
        {
            // Arrange
            bool expected = false;
            ContainerStack containerStack = new ContainerStack();
            containerStack.AddContainerToList(new ContainerCoolableValuable(4000));

            // Act
            bool actual = containerStack.AddContainerToList(new ContainerCoolableValuable(4000));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddContainerToList_TooHeavy_ShouldReturnFalse()
        {
            // Arrange
            bool expected = false;
            ContainerStack containerStack = new ContainerStack();
            containerStack.AddContainerToList(new ContainerCoolableValuable(120001));

            // Act
            bool actual = containerStack.AddContainerToList(new ContainerCoolable(4000));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddContainerToList_OneValuableOneNormal_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;
            ContainerStack containerStack = new ContainerStack();
            containerStack.AddContainerToList(new ContainerCoolableValuable(4000));

            // Act
            bool actual = containerStack.AddContainerToList(new ContainerCoolable(4000));

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}