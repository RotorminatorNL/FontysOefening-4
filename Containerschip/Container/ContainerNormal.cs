using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class ContainerNormal : IContainer
    {
        public int MinWeight { get; } = 4000;
        public int MaxWeight { get; } = 30000;
        public int Weight { get; }
        public int MaxWeightOnTop { get; } = 120000;
        public int Priority { get; } = 4;

        public ContainerNormal(int weight)
        {
            if (weight < 4000)
            {
                Weight = 4000;
            }
            else if (weight > 30000)
            {
                Weight = 30000;
            }
            else
            {
                Weight = weight;
            }
        }

        public override string ToString()
        {
            return $"Normaal [gewicht: {Weight}]";
        }
    }
}
