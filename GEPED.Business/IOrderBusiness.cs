using GEPED.Model;
using GEPED.Utils;
using System.Collections.Generic;

namespace GEPED.Business
{
    public interface IOrderBusiness
    {
        Response<OrderLocal> Save(OrderLocal api);
        Response<OrderLocal> Get(string id);
        Response<OrderLocal> Cancel(string id);
        Response<OrderLocal> Capture(string id);
        IList<OrderLocal> List();
    }
}
