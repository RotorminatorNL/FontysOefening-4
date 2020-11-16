using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class ContainerCoolableValuable : IContainer
    {
        public int MinWeight { get; } = 4000;
        public int MaxWeight { get; } = 30000;
        public int Weight { get; }
        public int MaxWeightOnTop { get; } = 120000;
        public int Priority { get; } = 1;
        public bool IsCoolable { get; } = true;
        public bool IsValuable { get; } = true;

        public ContainerCoolableValuable(int weight)
        {
            Weight = weight;
        }

        public override string ToString()
        {
            return $"Koel- en kostbaar [gewicht: {Weight}]";
        }
    }
}
