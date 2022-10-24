using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpService
{
    public interface IScriptService
    {
        bool Compile();

        void Run(int count);

    }
}
