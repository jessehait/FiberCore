using UnityEngine;

namespace RHGameCore
{
    public class MainThreadObserver : MonoBehaviour, IMainThreadObserver
    {
        public MonoBehaviour Root => this;
    }
}