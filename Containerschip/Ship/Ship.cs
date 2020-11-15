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
        public readonly int WeightLeftWing;
        public readonly int WeightRightWing;
        public readonly double WeightDifferenceOfWings;
        public readonly int TotalWeight;
        public readonly int MaxWeight;
        public readonly int RequiredWeight;
        public readonly bool IsAbleToGo;

        private List<IContainer> _containers;
        private List<IContainer> _unplacableContainers = new List<IContainer>();
        private List<ShipRow> _shipRows = new List<ShipRow>();

        public Ship(List<IContainer> containers, int length, int width)
        {
            Length = length;
            Width = width;
            MaxWeight = Length * Width * 150000;
            RequiredWeight = MaxWeight / 2;

            _containers = containers.ToList();

            CreateShipRows();
            SortContainerList();
            DistributeContainers();
            TryUnplacableContainers();


            WeightLeftWing = GetWeightOfWing(0, _shipRows.Count / 2);
            WeightRightWing = GetWeightOfWing(_shipRows.Count / 2, _shipRows.Count);

            if (IsAmountShipRowsOdd())
            {
                WeightLeftWing = GetWeightOfWing(0, _shipRows.Count / 2);
                WeightRightWing = GetWeightOfWing((_shipRows.Count / 2) + 1, _shipRows.Count);
            }

            TotalWeight = WeightLeftWing + WeightRightWing;
            WeightDifferenceOfWings = GetWeightDifferenceOfWings();
            IsAbleToGo = CheckIfAbleToGo();
        }

        private bool IsAmountShipRowsOdd()
        {
            return _shipRows.Count % 2 != 0;
        }

        private void CreateShipRows()
        {
            for (int i = 0; i < Width; i++)
            {
                _shipRows.Add(new ShipRow(Length));
            }
        }

        private void SortContainerList()
        {
            List<IContainer> output = new List<IContainer>();
            while (_containers.Count != 0)
            {
                IContainer container = GetHeaviestContainer(GetHeighestPriorityContainerType());
                output.Add(container);
                _containers.Remove(container);
            }
            _containers = output;
        }

        private string GetHeighestPriorityContainerType()
        {
            IContainer output = null;
            foreach (IContainer container in _containers)
            {
                if (output == null)
                {
                    output = container;
                }
                else if (output.Priority > container.Priority)
                {
                    output = container;
                }
            }
            return output.GetType().Name;
        }

        private IContainer GetHeaviestContainer(string containerType)
        {
            IContainer output = null;
            foreach (IContainer container in _containers)
            {
                if(container.GetType().Name == containerType)
                if (output == null)
                {
                    output = container;
                }
                else if (output.Weight < container.Weight)
                {
                    output = container;
                }
            }
            return output;
        }

        private void DistributeContainers()
        {
            foreach (IContainer container in _containers)
            {
                if (!GetRowWithLeastWeight().AddContainerToStack(container))
                {
                    _unplacableContainers.Add(container);
                }
            }
        }

        private void TryUnplacableContainers()
        {
            List<IContainer> storedUnstorables = new List<IContainer>();
            foreach (IContainer container in _unplacableContainers)
            {
                if (GetRowWithLeastWeight().AddContainerToStack(container))
                {
                    storedUnstorables.Add(container);
                }
            }

            RemoveStoredUnstorables(storedUnstorables);
        }

        private void RemoveStoredUnstorables(List<IContainer> storedUnstorables)
        {
            foreach (IContainer container in storedUnstorables)
            {
                _unplacableContainers.Remove(container);
            }
        }

        private ShipRow GetRowWithLeastWeight()
        {
            ShipRow output = null;
            foreach (ShipRow shipRow in _shipRows)
            {
                if (output == null)
                {
                    output = shipRow;
                }
                else if (output.Weight > shipRow.Weight)
                {
                    output = shipRow;
                }
            }
            return output;
        }

        private int GetWeightOfWing(int beginWing, int endWing)
        {
            int output = 0;
            for (int i = beginWing; i < endWing; i++)
            {
                output += _shipRows[i].Weight;
            }
            return output;
        }

        private double GetWeightDifferenceOfWings()
        {
            if (WeightLeftWing > WeightRightWing)
            {
                return Math.Round(100 / (double)TotalWeight * (WeightLeftWing - WeightRightWing), 1);
            }
            else if (Width == 1)
            {
                return 0;
            }
            else
            {
                return Math.Round(100 / (double)TotalWeight * (WeightRightWing - WeightLeftWing), 1);
            }
        }

        private bool CheckIfAbleToGo()
        {
            if (WeightDifferenceOfWings < 20 && TotalWeight >= RequiredWeight || Width == 1 && TotalWeight >= RequiredWeight)
            {
                return true;
            }
            return false;
        }

        public IReadOnlyCollection<IContainer> GetStack(string stackPos)
        {
            string[] splitStackPos = stackPos.Split('_');
            int stackRow = Convert.ToInt32(splitStackPos[0]);
            int stackNumber = Convert.ToInt32(splitStackPos[1]);

            return _shipRows[stackRow].GetStack(stackNumber);
        }

        public IReadOnlyCollection<IContainer> GetUnstorableContainers()
        {
            return _unplacableContainers.AsReadOnly();
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
