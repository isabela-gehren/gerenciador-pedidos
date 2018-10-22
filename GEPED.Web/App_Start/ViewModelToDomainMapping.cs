using AutoMapper;
using GEPED.Model;
using GEPED.Web.Models;

namespace GEPED.Web
{
    public class ViewModelToDomainMapping : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMapping"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<CustomerViewModel, Customer>();
            Mapper.CreateMap<CreditcardViewModel, Creditcard>();
            Mapper.CreateMap<PaymentViewModel, Payment>();
            Mapper.CreateMap<MerchantOrderViewModel, OrderLocal>();
        }
    }
}