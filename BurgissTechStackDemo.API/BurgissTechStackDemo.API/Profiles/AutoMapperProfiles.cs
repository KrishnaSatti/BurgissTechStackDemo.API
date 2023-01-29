using AutoMapper;
using BurgissTechStackDemo.API.DomainModels;
using BurgissTechStackDemo.API.Profiles.AfterMaps;
using DataModels = BurgissTechStackDemo.API.DataModels;

namespace BurgissTechStackDemo.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Employee, Employee>()
                .ReverseMap();

            CreateMap<DataModels.Gender, Gender>()
                .ReverseMap();

            CreateMap<DataModels.Address, Address>()
                .ReverseMap();

            CreateMap<UpdateEmployeeRequest, DataModels.Employee>().AfterMap<UpdateEmployeeRequestAfterMap>();

            CreateMap<AddEmployeeRequest, DataModels.Employee>().AfterMap<AddEmployeeRequestAfterMap>();
        }
    }
}
