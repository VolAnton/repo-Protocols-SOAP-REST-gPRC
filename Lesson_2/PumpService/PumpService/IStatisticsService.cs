using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpService
{
    public interface IStatisticsService
    {
        int SuccessTacts { get; set; }

        int ErrorTacts { get; set; }

        int AllTacts { get; set; }

    }
}
