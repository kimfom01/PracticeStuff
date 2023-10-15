namespace FlashCardApp.Services;

public interface IStackService
{
    void DisplayStackSettingsMenu();
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
