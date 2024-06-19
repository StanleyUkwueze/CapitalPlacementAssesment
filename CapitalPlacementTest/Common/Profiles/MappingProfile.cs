using AutoMapper;
using CapitalPlacementTest.Models;
using CapitalPlacementTest.Requests;
using CapitalPlacementTest.Responses;

namespace CapitalPlacementTest.Common.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Application, ApplicationDto>().ReverseMap();
            CreateMap<ApplicationQuestion, CreateQuestionDto>().ReverseMap();
            CreateMap<ApplicationQuestion, EditQuestionDto>().ReverseMap();
            CreateMap<ApplicationQuestion, QuestionResponse>().ReverseMap();
            CreateMap<CreateQuestionDto, QuestionResponse>().ReverseMap();
        }
    }
}
