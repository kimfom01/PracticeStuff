using BusinessLogic.Input;
using BusinessLogic.Services;
using BusinessLogic.Services.Implementation;
using BusinessLogic.TableVisualizer;
using ConsoleUI.UI;
using DataAccess.Config;
using DataAccess.Data;
using DataAccess.Data.Implementation;
using DataAccess.DTO;

namespace ConsoleUI;

public static class Factory
{
    public static Configuration CreateConfiguration()
    {
        return new Configuration();
    }

    public static IStackDataManager CreateStackDataManager()
    {
        return new StackDataManager(CreateConfiguration());
    }

    public static IFlashCardDataManager CreateFlashCardDataManager()
    {
        return new FlashCardDataManager(
            CreateConfiguration(),
        CreateStackDataManager());
    }

    public static IStudyAreaDataManager CreateStudyAreaDataManager()
    {
        return new StudyAreaDataManager(
            CreateConfiguration(),
            CreateStackDataManager());
    }

    public static VisualizationService<TModel>
    CreateVisualizationService<TModel>() where TModel : class
    {
        return new VisualizationService<TModel>();
    }

    public static UserInput CreateUserInput()
    {
        return new UserInput();
    }

    public static IFlashCardService CreateFlashCardService()
    {
        return new FlashCardService(
            CreateUserInput(),
            CreateVisualizationService<FlashCardDTO>(),
            CreateFlashCardDataManager());
    }

    public static IStackService CreateStackService()
    {
        return new StackService(
            CreateUserInput(),
            CreateStackDataManager(),
            CreateVisualizationService<StackDTO>(),
            CreateFlashCardService());
    }

    public static IStudyAreaService CreateStudyAreaService()
    {
        return new StudyAreaService(
            CreateUserInput(),
            CreateVisualizationService<StudyAreaDto>(),
            CreateFlashCardDataManager(),
            CreateStudyAreaDataManager(),
            CreateVisualizationService<StackDTO>(),
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
