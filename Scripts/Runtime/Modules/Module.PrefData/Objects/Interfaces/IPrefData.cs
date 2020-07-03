using System;

namespace Fiber.PrefData
{
    public interface IPrefData
    {
        string   PrefName       { get; }
        DateTime CreationDate   { get; }
        DateTime LastModifyDate { get; }
    }
}