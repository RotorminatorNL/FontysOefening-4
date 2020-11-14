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

        public bool AddContainerToStack(IContainer container)
        {
            if (container.IsCoolable)
            {
                if (_containerStacks[0].AddContainerToList(container))
                {
                    Weight += container.Weight;
                    return true;
                }
            }
            else
            {
                foreach (ContainerStack stack in _containerStacks)
                {
                    if (IsStackAvialable(_containerStacks.IndexOf(stack), stack.GetContainers().Count + 1) && stack.AddContainerToList(container))
                    {
                        Weight += container.Weight;
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsStackAvialable(int stackNumber, int containerAmount)
        {
            if (CheckStacksBehind(stackNumber, containerAmount) && CheckStacksInFront(stackNumber, containerAmount))
            {
                return true;
            }
            return false;
        }

        private bool CheckStacksBehind(int stackNumber, int containerAmount)
        {
            try
            {
                int amountContainersPreviousStack = _containerStacks[stackNumber - 1].GetContainers().Count;
                int amountContainersBeforeLastStack = _containerStacks[stackNumber - 2].GetContainers().Count;

                if (amountContainersPreviousStack == 0 || amountContainersPreviousStack > containerAmount)
                {
                    return true;
                }
                else if (amountContainersBeforeLastStack == 0 || amountContainersBeforeLastStack < amountContainersPreviousStack)
                {
                    return true;
                }
                else if (!_containerStacks[stackNumber - 1].IsTopContainerValuable())
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        private bool CheckStacksInFront(int stackNumber, int containerAmount)
        {
            try
            {
                int amountContainersNextStack = _containerStacks[stackNumber + 1].GetContainers().Count;
                int amountContainersSecondNextStack = _containerStacks[stackNumber + 2].GetContainers().Count;

                if (amountContainersNextStack == 0 || amountContainersNextStack > containerAmount)
                {
                    return true;
                }
                else if (amountContainersSecondNextStack == 0 || amountContainersSecondNextStack < amountContainersNextStack)
                {
                    return true;
                }
                else if (!_containerStacks[stackNumber + 1].IsTopContainerValuable())
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public IReadOnlyCollection<IContainer> GetStack(int stackNumber)
        {
            return _containerStacks[stackNumber].GetContainers();
        }
    }
}
