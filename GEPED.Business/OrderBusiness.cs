using GEPED.Model;
using GEPED.Repository;
using GEPED.Repository.Interfaces;
using GEPED.ServicePayment;
using GEPED.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace GEPED.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        public IList<OrderLocal> List()
        {
            return Resolver.Get<IOrderRepository>().List();
        }

        public Response<OrderLocal> Get(string id)
        {
            var orderLocal = Resolver.Get<IOrderRepository>().Get(id);

            return GetOrder(orderLocal);
        }

        public Response<OrderLocal> Save(OrderLocal local)
        {
            Response<OrderLocal> response = new Response<OrderLocal>();

            try
            {
                string id = DateTime.Now.ToString("yyyyMMddHHhhmm");
                local.Id = id;
                local.DtOrder = DateTime.Now;

                var order = new Order()
                {
                    MerchantOrderId = id,
                    Customer = local.Customer,
                    Payment = local.Payment
                };

                var apiGateway = new ApiGateway();
                var result = apiGateway.InvokeApi<Order, Order>(ConfigurationManager.AppSettings["BraspagUrlAuthor"], order, "POST");
                order = result.Entity;
                if (result.StatusCode == StatusCode.OK && result.Entity.Payment.ReasonCode == 0)
                {
                    local.Status = OrderStatus.Criado;
                    local.Payment.PaymentId = result.Entity.Payment.PaymentId;
                    local.Payment.CreditCard = null;
                    var rep = Resolver.Get<IOrderRepository>();
                    local = rep.Add(local);
                    response.Entity = local;
                }
                else
                {
                    response.Messages.Add(result.Entity.Payment.ReasonMessage);
                    response.StatusCode = StatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.ReponseException(ex);
                //cancelar Transacao
                if (!string.IsNullOrEmpty(local.Payment.PaymentId))
                {
                    string paymentId = local.Payment.PaymentId;
                    int amount = local.Payment.Amount;
                    var resultCancel = new ApiGateway().InvokeApi<Order, CancelOrder>(string.Format(ConfigurationManager.AppSettings["BraspagUrlCancel"], paymentId, amount), null, "PUT");
                }
            }
            return response;
        }

        public Response<OrderLocal> Cancel(string id)
        {
            var local = Resolver.Get<IOrderRepository>().Get(id);
            Response<OrderLocal> response = new Response<OrderLocal>();
            try
            {
                var result = new ApiGateway().InvokeApi<Order, CancelOrder>(string.Format(ConfigurationManager.AppSettings["BraspagUrlCancel"], local.Payment.PaymentId, local.Payment.Amount), null, "PUT");

                if (result.StatusCode == StatusCode.OK && result.Entity.ReasonCode == 0)
                {
                    local.Status = OrderStatus.Cancelado;
                    var rep = Resolver.Get<IOrderRepository>();
                    rep.Edit(local);
                }
                response.Entity = local;
                response.Messages = result.Messages;
                response.StatusCode = result.StatusCode;
            }
            catch (Exception ex)
            {
                response.ReponseException(ex);
            }

            return response;
        }

        public Response<OrderLocal> Capture(string id)
        {
            var local = Resolver.Get<IOrderRepository>().Get(id);
            Response<OrderLocal> response = new Response<OrderLocal>();
            try
            {
                var result = new ApiGateway().InvokeApi<Order, CaptureOrder>(string.Format(ConfigurationManager.AppSettings["BraspagUrlCapture"], local.Payment.PaymentId, local.Payment.Amount), null, "PUT");

                if (result.StatusCode == StatusCode.OK && result.Entity.Status == 2)
                {
                    local.Status = OrderStatus.Autorizado;
                    var rep = Resolver.Get<IOrderRepository>();
                    rep.Edit(local);
                }
                response.Entity = local;
                response.Messages = result.Messages;
                response.StatusCode = result.StatusCode;
            }
            catch (Exception ex)
            {
                response.ReponseException(ex);
            }

            return response;
        }

        private Response<OrderLocal> GetOrder(OrderLocal local)
        {
            Response<OrderLocal> response = new Response<OrderLocal>();

            try
            {
                var apiGateway = new ApiGateway();
                var result = apiGateway.InvokeApi<Order, Order>(string.Format(ConfigurationManager.AppSettings["BraspagUrlConsult"], local.Payment.PaymentId), null, "GET");
                var order = result.Entity;
                if (result.StatusCode == StatusCode.OK)
                {
                    response.Entity = local;
                    response.Entity.Customer = order.Customer;
                    response.Entity.Payment = order.Payment;
                }
                else
                {
                    response.Entity = null;
                    response.StatusCode = result.StatusCode;
                    response.Messages = result.Messages;
                }
            }
            catch (Exception ex)
            {
                response.ReponseException(ex);
            }

            return response;
        }
    }
}
