using System;
public class WindowHelper
{
    private static WindowManager instance = WindowManager.GetInstance;
    public static void ShowWindow(WindowID window, float tween = 0)
    {
        instance.ShowWindow(window, tween);
    }
    public static void ShowWindow(int window, float tween = 0)
    {
        instance.ShowWindow(window, tween);
    }
    public static void HideAllWindow()
    {
        instance.HideAll();
    }
    public static void HideWindow(int window)
    {
        instance.HideWindow(window);
    }
    public static void HideWindow(int window, float tween)
    {
        instance.HideWindow(window, tween);
    }
    public static void HideWindow(int window, float tween, Action action)
    {
        instance.HideWindow(window, tween, action);
    }
    public static void Navigation(WindowID window, params string[] param)
    {
        instance.Navigation(window, param);
    }
    public static void Navigation(WindowID window, params int[] param)
    {
        instance.Navigation(window, param);
    }
    public static void Navigation(int window, params string[] param)
    {
        instance.Navigation(window, param);
    }
    public static void Navigation(int window, params int[] param)
    {
        instance.Navigation(window, param);
    }
    public static void ExecuteCMD(CMDType type, object parm, bool isnotshow = false)
    {
        instance.ExecuteCMD(type, parm, isnotshow);
    }
}