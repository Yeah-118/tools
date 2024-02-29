using System.Text.RegularExpressions;
using UnityEngine;
public class StringHelper
{
    public static void MontageMsg(string msg, params object[] parm)
    {
        for (int i = 0; i < parm.Length; i++)
        {
            parm[i] = string.Format("<size=24><b><color=#{1}>{0}</color></b></size>", parm[i] != null ? parm[i] : "NULL", ColorUtility.ToHtmlStringRGBA(new Color(Random.Range(0f, 0.6f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1)));
        }
        Debug(string.Format(msg, parm));
    }
    public static void Debug(object msg)
    {
#if  UNITY_EDITOR
        UnityEngine.Debug.Log(msg);
#endif
    }
    public static string Wrap(string value)
    {
        return value.Replace("|", "\n");
    }
    public static bool IsNumerice(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
    }
    public static bool IsInt(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*$");
    }
    public bool IsUnsign(string value)
    {
        return Regex.IsMatch(value, @"^[.]?\d*$");
    }
}
