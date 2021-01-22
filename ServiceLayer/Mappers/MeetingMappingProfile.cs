using AutoMapper;
using DataLayer.Entities.Meeting;
using DataLayer.ViewModels.Meeting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Mappers
{
    public class MeetingMappingProfile : Profile
    {
        public MeetingMappingProfile()
        {
            CreateMap<Meetings, NewMeetingViewModel>().ReverseMap();

            CreateMap<Meetings, UpdateMeetingViewModel>().ReverseMap();

            CreateMap<Meetings, MeetingEditViewModel>().ReverseMap();

            CreateMap<Meetings, MeetingManagerSelectedViewModel>().ReverseMap();

            CreateMap<MeetingTimes, MeetingTimesViewModel>().ReverseMap();

            CreateMap<MeetingUsers, MeetingUserSelectedViewModel>().ReverseMap();
        }
    }
}
