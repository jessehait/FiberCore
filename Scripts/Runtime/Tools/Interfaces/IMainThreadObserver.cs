using UnityEngine;

namespace RHGameCore
{
    public interface IMainThreadObserver
    {
        MonoBehaviour Root { get; }
    }
}