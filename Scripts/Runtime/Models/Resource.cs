using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RHGameCore.ResourceManagement
{
    public class Resource
    {
        internal List<string>   _path;
        internal List<string> _directory;
        internal UnityEngine.Object _object;

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

        internal bool Load<T>() where T : UnityEngine.Object
        {
            _object = Resources.Load<T>(GetPath());

            return IsLoaded();
        }

        internal void UnLoad()
        {
            if (IsLoaded())
            {
                _object = null;
                GC.Collect();
            }
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
