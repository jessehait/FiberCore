using System;

namespace Fiber.FileData
{
    public interface IFileData
    {
        string   FileName       { get; }
        DateTime CreationDate   { get; }
        DateTime LastModifyDate { get; }
    }
}