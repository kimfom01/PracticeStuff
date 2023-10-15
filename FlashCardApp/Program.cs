using FlashCardApp.Config;
using FlashCardApp.Data;
using FlashCardApp.Data.Implementation;
using FlashCardApp.Input;
using FlashCardApp.Services;
using FlashCardApp.Services.Implementation;
using FlashCardApp.UI;


Configuration config = new();

IStackDataManager stackDataManager = new StackDataManager(config);
IFlashCardDataManager flashCardDataManager = new FlashCardDataManager(config, stackDataManager);
IStudyAreaDataManager studyAreaDataManager = new StudyAreaDataManager(config, stackDataManager);
UserInput input = new();
TableVisualizationEngine tableVisualizationEngine = new(flashCardDataManager, stackDataManager, studyAreaDataManager);

IFlashCardService flashCardService = new FlashCardService(input, tableVisualizationEngine, flashCardDataManager);
IStackService stackService = new StackService(input, stackDataManager, tableVisualizationEngine, flashCardService);
IStudyAreaService studyAreaService = new StudyAreaService(input, tableVisualizationEngine, flashCardDataManager, studyAreaDataManager);

ProgramController programController = new(input, flashCardService, studyAreaService, stackService);

programController.StartProgram();