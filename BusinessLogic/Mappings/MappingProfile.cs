using AutoMapper;
using DataAccess.Dtos;
using DataAccess.Dtos.FlashCard;
using DataAccess.Models;

namespace BusinessLogic.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FlashCard, CreateFlashCardDto>().ReverseMap();
        CreateMap<FlashCard, GetFlashCardDto>().ReverseMap();
        CreateMap<FlashCard, UpdateFlashCardBackDto>().ReverseMap();
        CreateMap<FlashCard, UpdateFlashCardDto>().ReverseMap();
        CreateMap<FlashCard, UpdateFlashCardFrontDto>().ReverseMap();
        CreateMap<StudyArea, StudyAreaDto>().ReverseMap();
        CreateMap<Stack, StackDto>().ReverseMap();
    }
}