using UnityEngine;

namespace Fiber.Audio
{
    public sealed class AudioClipConfig 
    {
        public bool          loop          = false;
        public float         volume        = 1;
        public ulong         delay         = 0;
        public bool          destroySource = false;
        public Transform     attachTarget  = null;
    }
}