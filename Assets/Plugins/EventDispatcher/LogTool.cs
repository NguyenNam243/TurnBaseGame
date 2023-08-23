using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTool
{
    public static void LogErrorEditorOnly(object messenger)
    {
#if UNITY_EDITOR
        Debug.LogError(messenger);
#endif
    }

    public static void LogWarningEditorOnly(object messenger)
    {
#if UNITY_EDITOR
        Debug.LogError(messenger);
#endif
    }

    public static void LogEditorOnly(object messenger)
    {
#if UNITY_EDITOR
        Debug.LogError(messenger);
#endif
    }
}
