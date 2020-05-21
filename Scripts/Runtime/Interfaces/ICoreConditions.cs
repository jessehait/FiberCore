using System;

namespace RHGameCore.Api
{
    public interface ICoreConditions
    {
        /// <summary>
        /// Core initialization flag
        /// </summary>
        bool IsInitialized { get; }
    }
}