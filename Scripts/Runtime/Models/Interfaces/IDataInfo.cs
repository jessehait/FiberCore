using System;

namespace Fiber.DataManagement
{
    public interface IDataInfo
    {
        string   FileName       { get; }
        DateTime CreationDate   { get; }
        DateTime LastModifyDate { get; }
    }
}