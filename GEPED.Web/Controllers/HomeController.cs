using GEPED.Web.Models;
using System.Web.Mvc;
using System.Linq;
using GEPED.Utils;
using GEPED.Business;

namespace GEPED.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string OrderId)
        {
            var listapedidos = new System.Collections.Generic.List<MerchantOrderViewModel>();
            var business = GEPED.Utils.Resolver.Get<IOrderBusiness>();
            business.List().ToList().ForEach(i => listapedidos.Add(AutoMapper.Mapper.Map<MerchantOrderViewModel>(i)));

            if (!string.IsNullOrWhiteSpace(OrderId))
                listapedidos = listapedidos.Where(i => i.Id.Contains(OrderId)).ToList();

            return View(listapedidos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MerchantOrderViewModel vm)
        {
            var orderLocal = AutoMapper.Mapper.Map<Model.OrderLocal>(vm);
            var business = GEPED.Utils.Resolver.Get<IOrderBusiness>();
            var response = business.Save(orderLocal);

            TempData["Entity"] = response.Entity;
            TempData["Messages"] = response.Messages;
            TempData["StatusCode"] = response.StatusCode;

            return RedirectToAction("Result");
        }

        public ActionResult Details(string merchantOrderId)
        {
            var business = GEPED.Utils.Resolver.Get<IOrderBusiness>();
            var response = business.Get(merchantOrderId);

            ViewBag.Entity = response.Entity;
            ViewBag.Messages = response.Messages;
            ViewBag.StatusCode = response.StatusCode;

            return View("Result");
        }

        public ActionResult Capturar(string merchantOrderId)
        {
            var business = GEPED.Utils.Resolver.Get<IOrderBusiness>();
            var response = business.Capture(merchantOrderId);

            ViewBag.Entity = response.Entity;
            ViewBag.Messages = response.Messages;
            ViewBag.StatusCode = response.StatusCode;

            return View("Result");
        }

        public ActionResult Cancelar(string merchantOrderId)
        {
            var business = GEPED.Utils.Resolver.Get<IOrderBusiness>();
            var response = business.Cancel(merchantOrderId);

            ViewBag.Entity = response.Entity;
            ViewBag.Messages = response.Messages;
            ViewBag.StatusCode = response.StatusCode;

            return View("Result");
        }

        public ActionResult Result()
        {
            ViewBag.Entity = TempData["Entity"];
            ViewBag.Messages = TempData["Messages"];
            ViewBag.StatusCode = TempData["StatusCode"];

            return View();
        }
    }
}