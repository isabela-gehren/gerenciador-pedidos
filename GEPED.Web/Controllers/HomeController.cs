using GEPED.Web.Models;
using System.Web.Mvc;

namespace GEPED.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(MerchantOrderViewModel vm)
        {
            var tset = AutoMapper.Mapper.Map<Model.MerchantOrder>(vm);
        }
    }
}