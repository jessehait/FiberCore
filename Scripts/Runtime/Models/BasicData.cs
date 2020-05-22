using System;
using UnityEngine;

namespace Fiber.DataManagement
{
    [Serializable]
    public class BasicData : IDataInfo
    {
        [SerializeField]
        private string _fileName;
        [SerializeField]
        private string _creationDate;
        [SerializeField]
        private string _lastModifyDate;

        public string   FileName       => _fileName;
        public DateTime CreationDate   => DateTime.Parse(_creationDate);
        public DateTime LastModifyDate => DateTime.Parse(_lastModifyDate);

        internal void Create(string filename, DateTime date)
        {
            _fileName       = filename;
            _creationDate   = date.ToString();
            _lastModifyDate = date.ToString();
        }

        internal void Modify(DateTime date)
        {
            _lastModifyDate = date.ToString();
        }
    }
}