using UnityEngine;

namespace Fiber.Core
{
    [CreateAssetMenu(fileName ="RHCore Config",menuName ="RHCore/Config")]
    public class FiberCoreConfig : ScriptableObject
    {
        [Header("Debugging")]
        [SerializeField] private bool _allowLogs;
        public bool AllowLogs => _allowLogs;

        [SerializeField] private bool _allowWarnings;
        public bool AllowWarnings => _allowWarnings;

        [SerializeField] private bool _allowErrors;
        public bool AllowErrors => _allowErrors;

        [SerializeField] private bool _autoUpdateResourceList;
        public bool AutoUpdateResourceList => _autoUpdateResourceList;
    }
}