using UnityEngine;

/*
 * Author:      Zachary Schmalz
 * Version:     1.0.0
 * Date:        September 19, 2018
 */

/// <summary>
/// This class extends functionality of the default Debug.Log to categorize debugging statements
/// </summary>
public class Debug : MonoBehaviour
{
    public enum LogType
    {
        Normal,
        Warning,
        Error
    };

    [SerializeField] private bool generalLog;
    [SerializeField] private bool audioLog;
    [SerializeField] private bool inputLog;
    [SerializeField] private Color generalColor;
    [SerializeField] private Color audioColor;
    [SerializeField] private Color inputColor;

    private static bool staticGeneralLog;
    private static bool staticAudioLog;
    private static bool staticInputLog;
    private static Color staticGeneralColor;
    private static Color staticAudioColor;
    private static Color staticInputColor;

    private void Awake()
    {
        OnValidate();
    }

    public void OnValidate()
    {
        staticGeneralLog = generalLog;
        staticAudioLog = audioLog;
        staticInputLog = inputLog;
        staticGeneralColor = generalColor;
        staticAudioColor = audioColor;
        staticInputColor = inputColor;
    }

    public static void Log(object o, LogType type = LogType.Normal)
    {
        switch(type)
        {
            case LogType.Normal:
                UnityEngine.Debug.Log(o);
                break;

            case LogType.Warning:
                UnityEngine.Debug.LogWarning(o);
                break;

            case LogType.Error:
                UnityEngine.Debug.LogError(o);
                break;
        }
    }

    public static void GeneralLog(object o, LogType type = LogType.Normal)
    {
        if (staticGeneralLog)
            Log(FormatString("(General Log)\n", staticGeneralColor) + o, type);
    }

    public static void AudioLog(object o, LogType type = LogType.Normal)
    {
        if (staticAudioLog)
            Log(FormatString("(Audio Log)\n", staticAudioColor) + o, type);
    }

    public static void InputLog(object o, LogType type = LogType.Normal)
    {
        if (staticInputLog)
            Log(FormatString("(Input Log)\n", staticInputColor) + o, type);
    }

    public static void LogWarning(object o)
    {
        Log(o, LogType.Warning);
    }

    public static void LogError(object o)
    {
        Log(o, LogType.Error);
    }
    
    private static string FormatString(object o, Color color)
    {
        return "<b><color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + o + "</color></b>";
    }
}