using System.Linq;
using AutoMapper;
using DomainModel;
using DomainView;

namespace WorkDataMVC.Util.ModelMapper
{
    public class ModelMapper
    {
        public static void InitializeConfigMapper()
        {
            Mapper.CreateMap<Employee, EmployeeView>();
 
            Mapper.CreateMap<Employee, EmployeeView>()
                .ForMember(n => n.Salary, s => s.MapFrom(n => n.EmpPromotion.Any() ?
                    n.EmpPromotion.OrderBy(date => date.HireDate).Last().Salary : 0));

            Mapper.CreateMap<Employee, EmployeeView>()
                .ForMember(n => n.NameJobTitle, s => s.MapFrom(n => n.EmpPromotion.Any() ?
                    n.EmpPromotion.OrderBy(date => date.HireDate).Last().JobTitle.NameJobTitle : null));
        }
    }
}