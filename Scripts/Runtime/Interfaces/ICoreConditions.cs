using System;

namespace FiberCore.Api
{
    public interface ICoreConditions
    {
        /// <summary>
        /// Core initialization flag
        /// </summary>
        bool IsInitialized { get; }
    }
}