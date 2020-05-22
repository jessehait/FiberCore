using System;

namespace Fiber.Core
{
    public interface ICoreConditions
    {
        /// <summary>
        /// Core initialization flag
        /// </summary>
        bool IsInitialized { get; }
    }
}