using BusinessLogic.Input;
using BusinessLogic.Services;
using BusinessLogic.Services.Implementation;
using BusinessLogic.TableVisualizer;
using ConsoleUI.UI;
using DataAccess.Config;
using DataAccess.Dtos;
using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;

namespace ConsoleUI;

public static class Factory
{
    private static Configuration CreateConfiguration()
    {
        return new Configuration();
    }

    private static IStackRepository CreateStackDataManager()
    {
        return new StackRepository(CreateConfiguration());
    }

    private static IFlashCardRepository CreateFlashCardDataManager()
    {
        return new FlashCardRepository(
            CreateConfiguration(),
        CreateStackDataManager());
    }

    private static IStudyAreaRepository CreateStudyAreaDataManager()
    {
        return new StudyAreaRepository(
            CreateConfiguration(),
            CreateStackDataManager());
    }

    private static VisualizationService<TModel>
    CreateVisualizationService<TModel>() where TModel : class
    {
        return new VisualizationService<TModel>();
    }

    private static UserInput CreateUserInput()
    {
        return new UserInput();
    }

    private static IFlashCardService CreateFlashCardService()
    {
        return new FlashCardService(
            CreateUserInput(),
            CreateVisualizationService<FlashCardDto>(),
            CreateFlashCardDataManager());
    }

    private static IStackService CreateStackService()
    {
        return new StackService(
            CreateUserInput(),
            CreateStackDataManager(),
            CreateVisualizationService<StackDto>(),
            CreateFlashCardService());
    }

    private static IStudyAreaService CreateStudyAreaService()
    {
        return new StudyAreaService(
            CreateUserInput(),
            CreateVisualizationService<StudyAreaDto>(),
            CreateFlashCardDataManager(),
            CreateStudyAreaDataManager(),
            CreateVisualizationService<StackDto>(),
            CreateStackDataManager());
    }

    public static ProgramController CreateProgramController()
    {
        return new ProgramController(
            CreateFlashCardService(),
            CreateStudyAreaService(),
            CreateStackService());
    }
}
