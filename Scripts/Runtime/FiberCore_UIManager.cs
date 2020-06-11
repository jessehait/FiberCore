using Fiber.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fiber.Core
{
    public sealed class FiberCore_UIManager : Manager, IUIManager
    {
        private Dictionary<string, UIScreen> _allScreens = new Dictionary<string, UIScreen>();

        public bool ContainsScreen(string key) => _allScreens.ContainsKey(key.ToUpper());

        public FiberCore_UIManager()
        {
            FindScreens();
        }

        private void FindScreens()
        {
            var screens = Object.FindObjectsOfType<UIScreen>();

            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];
                screen.Enabled = screen.ShownByDefault;
                AddScreen(screen.Key, screen);
            }
        }

        public void AddScreen(string key, UIScreen screen)
        {
            key = key.ToUpper();

            if (_allScreens.ContainsKey(key))
            {
                _allScreens.Remove(key);
            }
            _allScreens.Add(key, screen);
        }

        public void ReplaceScreen(string key, UIScreen screen)
        {
            key = key.ToUpper();

            if (!_allScreens.ContainsKey(key))
            {
                Tools.Logger.LogWarning("CORE.UIManager", "Screen with key \"" + key + "\" not found. Use <b>AddScreen</b> instead.");
            }
            else
            {
                _allScreens.Remove(key);
                AddScreen(key, screen);
            }
        }

        public T GetScreen<T>()
        {
            var screen = _allScreens.Where(x => x.Value.GetType() == typeof(T)).FirstOrDefault().Value;

            if (screen && screen is T scr)
                return scr;


            Tools.Logger.LogError("CORE.UIManager", "Screen of type" + typeof(T).ToString() + "\" not found");
            return default;

        }

        public UIScreen GetScreen(string key)
        {
            key = key.ToUpper();

            if (_allScreens.ContainsKey(key))
            {
                return _allScreens[key];
            }
            else
            {
                Tools.Logger.LogError("CORE.UIManager", "Screen with key \"" + key + "\" not found");
                return null;
            }
        }

        public T GetScreen<T>(string key) where T: UIScreen
        {

            var screen = GetScreen(key);

            if (screen != null)
            {
                if(screen is T tmp)
                    return tmp;
                else
                    Tools.Logger.LogError("CORE.UIManager", "UIScreen with key \"" + key + "\" is not type of: \"" + typeof(T).ToString() + "\"");
            }
            return null;
        }
    }
}