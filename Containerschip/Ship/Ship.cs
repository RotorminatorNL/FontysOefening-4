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
        public readonly int Length;
        public readonly int Width;

        private List<IContainer> _containers;
        private readonly int _maxWeight;
        private readonly int _requiredWeight;

        public Ship(List<IContainer> containers, int length, int width)
        {
            Length = length;
            Width = width;

            _containers = containers;
            _maxWeight = Length * Width * 150;
            _requiredWeight = _maxWeight / 2;

            SortContainerList();
            DistributeContainers();
        }

        private void SortContainerList()
        {

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
