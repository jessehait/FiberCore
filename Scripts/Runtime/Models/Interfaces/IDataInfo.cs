using System;

namespace FiberCore.Data
{
    public interface IDataInfo
    {
        string   FileName       { get; }
        DateTime CreationDate   { get; }
        DateTime LastModifyDate { get; }
    }
}