using System.Configuration;
using FlashCardApp.Data;
using FlashCardApp.Data.Implementation;
using FlashCardApp.Input;
using FlashCardApp.UI;

var connectionString = ConfigurationManager.AppSettings.Get("connectionString")
    ?? throw new NullReferenceException("connection string not provided");

IStackDataManager stackDataManager = new StackDataManager(connectionString);
IFlashCardDataManager flashCardDataManager = new FlashCardDataManager(connectionString, stackDataManager);
IStudyAreaDataManager studyAreaDataManager = new StudyAreaDataManager(connectionString, stackDataManager);
UserInput input = new();
TableVisualizationEngine tableVisualizationEngine = new(flashCardDataManager, stackDataManager, studyAreaDataManager);

ProgramController programController = new(stackDataManager, flashCardDataManager, studyAreaDataManager, input, tableVisualizationEngine);

programController.StartProgram();