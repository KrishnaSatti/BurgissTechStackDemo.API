using AutoMapper;
using BurgissTechStackDemo.API.DataModels;
using BurgissTechStackDemo.API.DomainModels;
using System.Net.NetworkInformation;
using DataModels = BurgissTechStackDemo.API.DataModels;

namespace BurgissTechStackDemo.API.Profiles.AfterMaps
{
    public class UpdateEmployeeRequestAfterMap : IMappingAction<UpdateEmployeeRequest, DataModels.Employee>
    {
        public void Process(UpdateEmployeeRequest source, DataModels.Employee destination, ResolutionContext context)
        {
            destination.Address = new DataModels.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
