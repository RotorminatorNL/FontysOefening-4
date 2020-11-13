using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public interface IContainer
    {
        int MinWeight { get; }
        int MaxWeight { get; }
        int Weight { get; }
        int MaxWeightOnTop { get; }
        int Priority { get; }
        bool IsValuable { get; }
        bool IsCoolable { get; }
    }
}
