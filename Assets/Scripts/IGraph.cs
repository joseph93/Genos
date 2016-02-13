using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    interface IGraph<T>
    {
        bool Contains(T vertex);
        IEnumerable<T> GetAdjacent(T vertex);
    }
}
