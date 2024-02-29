using System;
using System.Collections.Generic;
using UnityEngine;
public class Module
{
    private Dictionary<CMDType, Action<object>> CmdObjDir = new Dictionary<CMDType, Action<object>>();
    private Dictionary<CMDType, object> NotCmdObj = new Dictionary<CMDType, object>();
    private WindowManager manager;
    public void OnInit(WindowManager window)
    {
        manager = window;
    }
    public void ExecuteCMD(CMDType type, object parm, bool isnotshow = false)
    {
        if (!CmdObjDir.ContainsKey(type))
        {
            StringHelper.MontageMsg("类型{0}不存在", type);
            CMDData data = parm as CMDData;
            Debug.Log(data.Name + data.Value);
            if (isnotshow)
            {
                if (NotCmdObj.ContainsKey(type))
                {
                    NotCmdObj[type] = parm;
                }
                else
                {
                    NotCmdObj.Add(type, parm);
                }
            }
            return;
        }
        CmdObjDir[type].Invoke(parm);
    }
    public void RegisterCMD(CMDType type, Action<object> action)
    {
        if (CmdObjDir.ContainsKey(type))
        {
            CmdObjDir[type] += action;
        }
        else
        {
            CmdObjDir.Add(type, action);
        }
        if (NotCmdObj.ContainsKey(type))
        {
            CmdObjDir[type].Invoke(NotCmdObj[type]);
            NotCmdObj.Remove(type);
        }
    }
    public void LogOffCMD(CMDType type, Action<object> action)
    {
        if (CmdObjDir.ContainsKey(type))
        {
            CmdObjDir.Remove(type);
        }
    }
    public void ShowWindow(WindowID window, float tween = 0)
    {
        manager.ShowWindow(window, tween);
    }
    public void ShowWindow(int window, float tween = 0)
    {
        manager.ShowWindow(window, tween);
    }
    public void Navigation(int windowId, params string[] strings)
    {
        manager.Navigation(windowId, strings);
    }
    public void Navigation(WindowID windowId, params string[] strings)
    {
        manager.Navigation(windowId, strings);
    }
    public void Navigation(int windowId, params int[] param)
    {
        manager.Navigation(windowId, param);
    }
    public void Navigation(WindowID windowId, params int[] param)
    {
        manager.Navigation(windowId, param);
    }
}
public enum CMDType
{
#if false
    Int = 0,   
    String = 100, 
    Bool = 200,
#endif
    Window1,
    Window2,
    Window3,
    Window4,
    Window5,
    Window6,
}