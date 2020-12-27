using AutoMapper;
using DataLayer.Entities.User;
using DataLayer.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Users, RegisterViewModel>().ReverseMap()
                 .ForMember(a => a.UserName, option => { option.MapFrom(b => b.NationalCode); });
            CreateMap<Users, CreateUserViewModel>().ReverseMap()
                 .ForMember(a => a.UserName, option => { option.MapFrom(b => b.NationalCode); });
        }
    }
}
