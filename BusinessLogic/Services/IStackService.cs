using BusinessLogic.Enums;

namespace BusinessLogic.Services;

public interface IStackService
{
    StackSettingsOptions DisplayStackSettingsMenu();
    Task ManageStacksSettings();
    Task GetStackToAdd();
    void DisplayUpdateStackMenu();
    Task UpdateStackName();
    void DisplayDeleteMenu();
    Task DeleteStack();
    Task ViewStackForFlashCardOperations();
    void SelectStackToOperateOn();
    Task CreateStackTable();
}
