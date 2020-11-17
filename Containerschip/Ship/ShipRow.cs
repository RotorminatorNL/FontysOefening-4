using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class ShipRow
    {
        private List<ContainerStack> _containerStacks = new List<ContainerStack>();
        public int Weight { get; private set; }

        public ShipRow(int amountStacks)
        {
            CreateContainerStacks(amountStacks);
        }

        private void CreateContainerStacks(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _containerStacks.Add(new ContainerStack());
            }
        }

        private bool AddCoolableContainer(IContainer container)
        {
            if (_containerStacks[0].AddContainerToList(container))
            {
                Weight += container.Weight;
                return true;
            }
            return false;
        }

        private bool AddOtherContainer(IContainer container)
        {
            foreach (ContainerStack stack in _containerStacks)
            {
                if (IsStackAvialable(_containerStacks.IndexOf(stack), stack.GetContainers().Count + 1, container) && stack.AddContainerToList(container))
                {
                    Weight += container.Weight;
                    return true;
                }
            }
            return false;
        }

        private bool IsStackAvialable(int stackNumber, int containerAmount, IContainer container)
        {
            if (CheckStackSurroundings(stackNumber, containerAmount, container))
            {
                return true;
            }
            return false;
        }

        private bool CheckStackSurroundings(int stackNumber, int containerAmount, IContainer container)
        {
            ContainerStack currentStack = GetSurroundingStack(stackNumber, 0);

            ContainerStack previousStack = GetSurroundingStack(stackNumber, -1);
            ContainerStack secondPreviousStack = GetSurroundingStack(stackNumber, -2);

            ContainerStack nextStack = GetSurroundingStack(stackNumber, 1);
            ContainerStack secondNextStack = GetSurroundingStack(stackNumber, 2);

            if (previousStack == null || secondPreviousStack == null)
            {
                return true;
            }
            else if (previousStack.GetContainers().Count == 0 || secondPreviousStack.GetContainers().Count == 0)
            {
                return true;
            }
            else if (!container.IsValuable)
            {
                return CanNormalContainerBePlaced(containerAmount, previousStack, currentStack, nextStack, secondNextStack);
            }
            else if (container.IsValuable)
            {
                return CanValuableContainerBePlaced(containerAmount, previousStack, nextStack);
            }
            
            return false;
        }

        private ContainerStack GetSurroundingStack(int stackNumber, int amount)
        {
            try
            {
                return _containerStacks[stackNumber + amount];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool CanNormalContainerBePlaced(int containerAmount, ContainerStack previousStack, ContainerStack currentStack, ContainerStack nextStack, ContainerStack secondNextStack)
        {
            if (containerAmount < previousStack.GetContainers().Count)
            {
                return CheckNextStacks(nextStack, secondNextStack);
            }
            else if (currentStack.IsTopContainerValuable())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckNextStacks(ContainerStack nextStack, ContainerStack secondNextStack)
        {
            if (nextStack == null || secondNextStack == null)
            {
                return true;
            }

            if (nextStack.IsTopContainerValuable() && nextStack.GetContainers().Count > secondNextStack.GetContainers().Count)
            {
                return true;
            }
            return false;
        }

        private bool CanValuableContainerBePlaced(int containerAmount, ContainerStack previousStack, ContainerStack nextStack)
        {
            if (containerAmount < previousStack.GetContainers().Count)
            {
                return CheckNextStack(nextStack, containerAmount);
            }
            else
            {
                return false;
            }
        }

        private bool CheckNextStack(ContainerStack nextStack, int containerAmount)
        {
            if (nextStack == null)
            {
                return true;
            }

            if (containerAmount > nextStack.GetContainers().Count)
            {
                return true;
            }
            return false;
        }

        public bool AddContainerToStacks(IContainer container)
        {
            if (container.IsCoolable)
            {
                return AddCoolableContainer(container);
            }
            else
            {
                return AddOtherContainer(container);
            }
        }

        public IReadOnlyCollection<IContainer> GetStack(int stackNumber)
        {
            return _containerStacks[stackNumber].GetContainers();
        }
    }
}
