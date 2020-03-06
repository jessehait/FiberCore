using RHGameCore.UI;

public interface IUIManager
{
    void AddScreen(string key, UIScreen screen);
    bool ContainsScreen(string key);
    UIScreen GetScreen(string key);
    T GetScreen<T>(string key) where T : UIScreen;
    void ReplaceScreen(string key, UIScreen screen);
}