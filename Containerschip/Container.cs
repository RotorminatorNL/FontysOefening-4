using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class Container
    {
        public ContainerTypes Type { get; private set; }
        public int Weight { get; private set; }

        public Container(ContainerTypes type, int weight)
        {
            Type = type;
            Weight = weight;
        }
    }
}
