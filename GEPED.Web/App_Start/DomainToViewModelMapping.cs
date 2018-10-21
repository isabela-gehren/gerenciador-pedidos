using AutoMapper;
using GEPED.Model;
using GEPED.Web.Models;

namespace GEPED.Web
{
    public class DomainToViewModelMapping : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMapping"; }
        }

        protected override void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<Creditcard, CreditcardViewModel>();
                cfg.CreateMap<Payment, PaymentViewModel>();
                cfg.CreateMap<MerchantOrder, MerchantOrderViewModel>();
            });

            IMapper iMapper = config.CreateMapper();
        }

    }
}