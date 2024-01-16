using AutoMapper;
using PracticeStuff.Application.Dtos.FlashCard;
using PracticeStuff.Application.Dtos.Stack;
using PracticeStuff.Application.Dtos.StudyArea;
using PracticeStuff.Core;

namespace PracticeStuff.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FlashCard, CreateFlashCardDto>().ReverseMap();
        CreateMap<FlashCard, GetFlashCardListDto>().ReverseMap();
        CreateMap<FlashCard, GetFlashCardDetailDto>().ReverseMap();
        CreateMap<FlashCard, UpdateFlashCardDto>().ReverseMap();
        CreateMap<FlashCard, UpdateFlashCardFrontDto>().ReverseMap();
        CreateMap<FlashCard, UpdateFlashCardBackDto>().ReverseMap();
        CreateMap<Stack, CreateStackDto>().ReverseMap();
        CreateMap<Stack, GetStackListDto>().ReverseMap();
        CreateMap<Stack, GetStackDetailDto>().ReverseMap();
        CreateMap<Stack, UpdateStackDto>().ReverseMap();
        CreateMap<StudyArea, CreateStudyAreaDto>().ReverseMap();
        CreateMap<StudyArea, GetStudyAreaListDto>().ReverseMap();
        CreateMap<StudyArea, GetStudyAreaDetailDto>().ReverseMap();
    }
}