using UnityEngine;

/*
 * Author:      Zachary Schmalz
 * Version:     1.0.0
 * Date:        September 13, 2018
 */

/// <summary>
/// This class extends functionality of the default Debug.Log to categorize debugging statements
/// </summary>
public class Debug : MonoBehaviour
{
    [SerializeField] private bool generalLog;
    [SerializeField] private bool audioLog;
    [SerializeField] private Color generalColor;
    [SerializeField] private Color audioColor;

    private static bool staticGeneralLog;
    private static bool staticAudioLog;
    private static Color staticGeneralColor;
    private static Color staticAudioColor;

    private void Awake()
    {
        OnValidate();
    }

    public void OnValidate()
    {
        staticGeneralLog = generalLog;
        staticAudioLog = audioLog;
        staticGeneralColor = generalColor;
        staticAudioColor = audioColor;
    }

    public static void Log(string s)
    {
        UnityEngine.Debug.Log(s);
    }

    public static void GeneralLog(object s)
    {
        if (staticGeneralLog)
            UnityEngine.Debug.Log(FormatString("(General Log)\n", staticGeneralColor) + s);
    }

    public static void AudioLog(object s)
    {
        if (staticAudioLog)
            UnityEngine.Debug.Log(FormatString("(Audio Log)\n", staticAudioColor) + s);
    }

    public static void LogWarning(object s)
    {
        UnityEngine.Debug.LogWarning(s);
    }

    public static void LogError(object s)
    {
        UnityEngine.Debug.LogError(s);
    }

    private static string FormatString(object s, Color color)
    {
        return "<b><color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + s + "</color></b>";
    }
}