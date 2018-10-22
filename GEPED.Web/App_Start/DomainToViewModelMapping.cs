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
            Mapper.CreateMap<Customer, CustomerViewModel>();
            Mapper.CreateMap<Creditcard, CreditcardViewModel>();
            Mapper.CreateMap<Payment, PaymentViewModel>();
            Mapper.CreateMap<OrderLocal, MerchantOrderViewModel>()
                .ForMember(i => i.Status, o => o.MapFrom(op => op.Status.ToString()));
        }
    }
}