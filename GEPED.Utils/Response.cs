using System;
using System.Collections.Generic;

namespace GEPED.Utils
{
    public class Response<T>
    {
        public Response(T entity)
        {
            Entity = entity;
            StatusCode = StatusCode.OK;
            Messages = new List<string>();
        }

        public Response()
        {
            StatusCode = StatusCode.OK;
            Messages = new List<string>();
        }

        public StatusCode StatusCode { get; set; }
        public List<string> Messages { get; set; }
        public T Entity { get; set; }

        public void ReponseException(Exception ex)
        {
            if (Messages == null)
                Messages = new List<string>();

            Messages.Add(ex.ToString());
            StatusCode = StatusCode.InternalServerError;
        }
    }
}
