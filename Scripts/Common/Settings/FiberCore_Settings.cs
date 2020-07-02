using System;
using UnityEngine;

namespace Fiber.Common
{
    public class FiberCore_Settings : ScriptableObject
    {
        [FieldData("Allow Core logs", "Allow FiberCore to show logs in console")]
        [SerializeField] private bool _allowLogs;
        public bool AllowLogs => _allowLogs;

        [FieldData("Allow Core warnings", "Allow FiberCore to show warning logs in console")]
        [SerializeField] private bool _allowWarnings;
        public bool AllowWarnings => _allowWarnings;

        [FieldData("Allow Core errors","Allow FiberCore to show error logs in console")]
        [SerializeField] private bool _allowErrors;
        public bool AllowErrors => _allowErrors;

        [FieldData("Runtime calculation", "If enabled, you can get current FPS at runtime")]
        [SerializeField] private bool _calculateFPS;
        public bool CalculateFPS => _calculateFPS;

        [FieldData("Limit", "Limits FPS\n\n" +
        "0 = unlimited")]
        [SerializeField] private uint _limitFPS = uint.MaxValue;
        public uint LimitFPS => _limitFPS;

        [FieldData("VSync", "Turn VSync state")]
        [SerializeField] private bool _enableVSync;
        public bool EnableVSync => _enableVSync;

        [FieldData("Expanding method", "Pools expanding behaviour on GetElement\n\n" +
        "None = don't expand\n" +
        "Expand = create new items if pool is empty\n" +
        "Replace = if pool is empty - reuse items that currently in use")]
        [SerializeField] private PoolExpandMethod _poolExpandMethod;
        public PoolExpandMethod PoolExpandMethod => _poolExpandMethod;

        [FieldData("Unused clean rate", "If you don't use some pool, objects in this pool will be removed one by one with this time rate (seconds)")]
        [SerializeField] private float _poolCleanUpRate;
        public float PoolCleanUpRate => _poolCleanUpRate;

    }
}
