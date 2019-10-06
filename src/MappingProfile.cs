using AutoMapper;
using Northwind.Entities.Models;
using Northwind.ViewModels;


namespace CSharp_Net_Core_MVC_Northwind
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //https://www.cjavapy.com/article/100/
            //Install-Package AutoMapper -Version 9.0.0
            //Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 7.0.0

            //加入需要映射的Model
            CreateMap<Customers, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customers>();

            CreateMap<Products, ProductViewModel>();
            CreateMap<ProductViewModel, Products>();


        }
    }
}
