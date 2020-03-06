using System;

namespace RHGameCore.DataManagement
{
    public interface IDataInfo
    {
        string   FileName       { get; }
        DateTime CreationDate   { get; }
        DateTime LastModifyDate { get; }
    }
}