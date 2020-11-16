using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class ContainerCoolable : IContainer
    {
        public int MinWeight { get; } = 4000;
        public int MaxWeight { get; } = 30000;
        public int Weight { get; }
        public int MaxWeightOnTop { get; } = 120000;
        public int Priority { get; } = 3;
        public bool IsCoolable { get; } = true;
        public bool IsValuable { get; } = false;

        public ContainerCoolable(int weight)
        {
            Weight = weight;
        }

        public override string ToString()
        {
            return $"Koelbaar [gewicht: {Weight}]";
        }
    }
}
