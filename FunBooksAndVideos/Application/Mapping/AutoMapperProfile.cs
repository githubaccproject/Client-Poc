using Application.DTOs.CustomerDto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();
            // Add other mappings as needed
        }
    }

}
