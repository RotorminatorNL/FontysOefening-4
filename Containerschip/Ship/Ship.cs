using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class Ship
    {
        /* For example:
         * - Calculate maxWeight (900): length (3) x width (2) x maxStackWeight (150)
         * - requiredWeight      (450): maxWeight (900) / 2 
         */
        private readonly int _maxWeight;
        private readonly int _requiredWeight;
        private List<IContainer> _containers;

        public Ship(List<IContainer> containers, int length, int width)
        {
            _containers = containers;
            _maxWeight = length * width * 150;
            _requiredWeight = _maxWeight / 2;

            DistributeContainers();
        }

        private void DistributeContainers()
        {

        }

        public override string ToString()
        {
            return $"Schip: max-gewicht = {_maxWeight} & verplicht-gewicht = {_requiredWeight}";
        }
    }
}
