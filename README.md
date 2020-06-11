<p align="center">
<img align="center" src="http://i.piccy.info/i9/eac687c16e079fc2e290ec9add953d83/1591537021/34362/1382405/Fiber.png"/>
  <br/><br/>
    <a href="https://www.npmjs.com/package/com.fiber.fibercore" alt="NPM">
        <img src="https://img.shields.io/npm/v/com.fiber.fibercore?style=for-the-badge" /></a>
    <a href="https://github.com/jessehait/FiberCore/commits/master" alt="Commit Activity">
        <img src="https://img.shields.io/github/commit-activity/m/jessehait/FiberCore?style=for-the-badge" /></a>
    <a href="https://github.com/jessehait/FiberCore/commits/master" alt="Last Commit">
        <img src="https://img.shields.io/github/last-commit/jessehait/FiberCore?style=for-the-badge" /></a>
        <img src="https://img.shields.io/github/languages/top/jessehait/FiberCore?style=for-the-badge" />
        <img src="https://img.shields.io/github/repo-size/jessehait/FiberCore?style=for-the-badge" />
</p>

FiberCore it's a lightweight game core that allows you to use **Data Management, UI Management, Scene Management** etc.
Here is small example of game (without gameplay logic) developed using FiberCore.

***

### Game Loader Example

```cs
public class ExampleGameLoader : MonoBehaviour
{
    private ExampleData gameData;

    private void Awake()
    {
        FiberCore.Instances.OnInstanceChanged += OnInstanceChanged;
        
        FiberCore.PrefData.RegisterType<ExampleData>();
        FiberCore.PrefData.Load();
        FiberCore.PrefData.GetData(out gameData);
        
        FiberCore.Instances.LoadInstance(gameData.NextLevelID);
    }

    private void OnInstanceChanged(Instance obj)
    {
        if (obj)
        {
            FiberCore.UI.GetScreen<GameUI>().Show();
            
            obj.As<ExampleLevel>().Example_Start();
        }
        else
        {
            FiberCore.UI.GetScreen<GameUI>().Hide();
        }
    }
}
```
***

### Game Data Example

```cs
public class ExampleData: BasicData
{
    public int NextLevelID = 1;
}
```
***

### Game UI Example

```cs
public class GameUI : UIScreen
{
    protected override void OnReady()
    {
    }
}
```
***

### Game Instance Example

```cs
public class ExampleLevel : Instance
{
    private const string res_playerPath = "prefabs/playerPrefab";

    [SerializeField]
    private AudioClip     levelStartSound;
    [SerializeField]
    private AudioListener audioListener;
   
    protected override void OnReady()
    {
        FiberCore.Audio.SetListener(audioListener);
        
        FiberCore.Resources.Load<GameObject>(res_playerPath);
    }

    protected override void OnUnload()
    {
        FiberCore.Resources.Unload(res_playerPath);
    }

    public void Example_Start()
    {
        FiberCore.Audio.Play(levelStartSound);

        var player = FiberCore.Resources.Get<GameObject>(res_playerPath);

        Instantiate(player);
    }

    public void Emample_Complete()
    {
        if(FiberCore.PrefData.GetData(out ExampleData data))
        {
            data.NextLevelID = ID + 1;

            FiberCore.PrefData.Save();

            FiberCore.Instances.LoadInstance(data.NextLevelID);
        }
    }
}
```

