using System;

namespace Fiber.Api
{
    public interface ICoreConditions
    {
        /// <summary>
        /// Core initialization flag
        /// </summary>
        bool IsInitialized { get; }
    }
}