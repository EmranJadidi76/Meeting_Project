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
                 .ForMember(a => a.UserName, option => { option.MapFrom(b => b.NationalCode); })
                 .ForMember(a=>a.IsActive , option => option.UseValue(true))
                 .ForMember(a=>a.IsModerator , option => option.UseValue(true));
            CreateMap<Users, CreateUserViewModel>().ReverseMap()
                 .ForMember(a => a.UserName, option => { option.MapFrom(b => b.NationalCode); })
                 .ForMember(a => a.IsActive, option => option.UseValue(true))
                 .ForMember(a => a.IsModerator, option => option.UseValue(true))
                 .ForMember(a => a.IsModerator, option => option.UseValue(false));
        }
    }
}
