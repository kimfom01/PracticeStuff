using BusinessLogic.Input;
using BusinessLogic.Services;
using BusinessLogic.Services.Implementation;
using BusinessLogic.TableVisualizer;
using ConsoleUI.UI;
using DataAccess.Config;
using DataAccess.Data;
using DataAccess.Data.Implementation;
using DataAccess.DTO;


Configuration config = new();

IStackDataManager stackDataManager = new StackDataManager(config);
IFlashCardDataManager flashCardDataManager = new FlashCardDataManager(config, stackDataManager);
IStudyAreaDataManager studyAreaDataManager = new StudyAreaDataManager(config, stackDataManager);
UserInput input = new();

VisualizationService<FlashCardDTO> flashCardVisualizer = new();
VisualizationService<StackDTO> stackVisualizer = new();
VisualizationService<StudyAreaDto> studyAreaVisualizer = new();
IFlashCardService flashCardService = new FlashCardService(input, flashCardVisualizer, flashCardDataManager);
IStackService stackService = new StackService(input, stackDataManager, stackVisualizer, flashCardService);
IStudyAreaService studyAreaService = new StudyAreaService(input, studyAreaVisualizer, flashCardDataManager, studyAreaDataManager, stackVisualizer, stackDataManager);

ProgramController programController = new(input, flashCardService, studyAreaService, stackService);

programController.StartProgram();