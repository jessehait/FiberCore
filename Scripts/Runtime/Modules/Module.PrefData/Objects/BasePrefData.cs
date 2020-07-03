using System;
using UnityEngine;

namespace Fiber.PrefData
{
    [Serializable]
    public class BasePrefData : IPrefData
    {
        [SerializeField]
        private string _prefName;
        [SerializeField]
        private string _creationDate;
        [SerializeField]
        private string _lastModifyDate;

        public string   PrefName       => _prefName;
        public DateTime CreationDate   => DateTime.Parse(_creationDate);
        public DateTime LastModifyDate => DateTime.Parse(_lastModifyDate);

        internal void Create(string prefName, DateTime date)
        {
            _prefName       = prefName;
            _creationDate   = date.ToString();
            _lastModifyDate = date.ToString();
        }

        internal void Modify(DateTime date)
        {
            _lastModifyDate = date.ToString();
        }
    }
}