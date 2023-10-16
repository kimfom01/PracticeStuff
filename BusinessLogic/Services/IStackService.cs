using BusinessLogic.Enums;

namespace BusinessLogic.Services;

public interface IStackService
{
    StackSettingsOptions DisplayStackSettingsMenu();
    void ManageStacksSettings();
    void GetStackToAdd();
    void DisplayUpdateStackMenu();
    void UpdateStackName();
    void DisplayDeleteMenu();
    void DeleteStack();
    void ViewStackForFlashCardOperations();
    void SelectStackToOperateOn();
    void CreateStackTable();
}
