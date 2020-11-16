using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class ContainerStack
    {
        private List<IContainer> _containers = new List<IContainer>();
        private int _weight;

        public bool AddContainerToList(IContainer container)
        {
            if (!IsAble(container))
            {
                return false;
            }

            _weight += container.Weight;

            if (container.IsValuable)
            {
                _containers.Insert(0, container);
                return true;
            }
            else
            {
                _containers.Add(container);
                return true;
            }
        }

        private bool IsAble(IContainer container)
        {
            if (container.IsValuable && ContainsValuableContainer())
            {
                return false;
            }
            else if (IsTooHeavy(container))
            {
                return false;
            }
            return true;
        }

        private bool ContainsValuableContainer()
        {
            foreach (IContainer container in _containers)
            {
                if (container.IsValuable)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsTooHeavy(IContainer container)
        {
            if (_weight <= container.MaxWeightOnTop)
            {
                return false;
            }
            return true;
        }

        public bool IsTopContainerValuable()
        {
            try
            {
                return _containers[0].IsValuable;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IReadOnlyCollection<IContainer> GetContainers()
        {
            return _containers.AsReadOnly();
        }

        public override string ToString()
        {
            string output = "";
            foreach (IContainer container in _containers)
            {
                output += $"{container} \n";
            }
            return output;
        }
    }
}
