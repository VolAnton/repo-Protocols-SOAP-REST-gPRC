using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PumpService
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples", SessionMode = SessionMode.Required, CallbackContract = typeof(IPumpServiceCallback))]
    public interface IPumpService
    {
        [OperationContract]
        void RunScript();


        [OperationContract]
        void UpdateAndCompileScript(string fileName);

    }
}
