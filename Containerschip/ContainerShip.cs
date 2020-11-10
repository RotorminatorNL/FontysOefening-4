using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class ContainerShip
    {
        /* For example:
         * - Calculate maxWeight (900): length (3) x width (2) x maxStackWeight (150)
         * - requiredWeight      (450): maxWeight (900) / 2 
         */
        private int _maxWeight;
        private int _requiredWeight;
        private List<IContainer> _containers;

        public ContainerShip(List<IContainer> containers)
        {
            _containers = containers;
        }
    }
}
