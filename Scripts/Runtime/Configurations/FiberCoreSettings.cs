using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiberCoreSettings : ScriptableObject
{
    [SerializeField] private bool _allowLogs;
    public bool AllowLogs => _allowLogs;

    [SerializeField] private bool _allowWarnings;
    public bool AllowWarnings => _allowWarnings;

    [SerializeField] private bool _allowErrors;
    public bool AllowErrors => _allowErrors;

    [SerializeField] private bool _autoUpdateResourceList;
    public bool AutoUpdateResourceList => _autoUpdateResourceList;
}
