﻿using FiberCore.UI;
using System.Collections.Generic;

namespace FiberCore.Api
{
    public sealed class FiberCore_UIManager : Manager, IUIManager
    {
        private Dictionary<string, UIScreen> _allScreens = new Dictionary<string, UIScreen>();

        public bool ContainsScreen(string key) => _allScreens.ContainsKey(key.ToUpper());

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
                global::FiberCore.Tools.Logger.LogWarning("CORE.UIManager", "Screen with key \"" + key + "\" not found. Use <b>AddScreen</b> instead.");
            }
            else
            {
                _allScreens.Remove(key);
                AddScreen(key, screen);
            }
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
                global::FiberCore.Tools.Logger.LogError("CORE.UIManager", "Screen with key \"" + key + "\" not found");
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
                    global::FiberCore.Tools.Logger.LogError("CORE.UIManager", "UIScreen with key \"" + key + "\" is not type of: \"" + typeof(T).ToString() + "\"");
            }
            return null;
        }
    }
}