using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RHGameCore.ResourceManagement
{
    public class Resource
    {
        internal List<string>   _path;
        internal List<string> _directory;
        internal Object      _object;

        internal bool IsLoaded()
        {
            return _object != null;
        }

        public Resource(string path)
        {
            path       = path.Trim();

            _path      = path.Split('/').ToList();
            _directory = path.Split('/').ToList();
            _directory.Remove(_directory.LastOrDefault());
            _object    = null;
        }

        internal bool Load<T>() where T : Object
        {
            _object = Resources.Load<T>(GetPath());

            return IsLoaded();
        }

        internal void UnLoad()
        {
            if (_object)
                Resources.UnloadAsset(_object);
            _object = null;
        }

        internal string GetPath()
        {
            string path = "";

            for (int i = 0; i < _path.Count; i++)
            {
                if (i > 0)
                    path += "/";

                path += _path[i];
            }
            return path;
        }
    }
}
