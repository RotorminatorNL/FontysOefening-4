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
         * - Calculate maxWeight (900): length (3) x width (2) x maxContainerSpaceWeight (150)
         * - requiredWeight      (450): maxWeight (900) / 2 
         */
        private readonly int maxContainerSpaceWeight = 150;
        private int maxWeight;
        private int requiredWeight;
    }
}
