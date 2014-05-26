using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ATS.Service
{
    [ServiceContract]
    interface IPersonService
    {
        [OperationContract]
        string GetPerson(int personId);
    }
}
