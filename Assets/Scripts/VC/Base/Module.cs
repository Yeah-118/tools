using System;
using System.Collections.Generic;
public class Module
{
    private Dictionary<CMDType, Action<string>> CmdStrDir = new Dictionary<CMDType, Action<string>>();
    private Dictionary<CMDType, Action<int>> CmdIntDir = new Dictionary<CMDType, Action<int>>();
    private Dictionary<CMDType, string> NotCmdStr = new Dictionary<CMDType, string>();
    private Dictionary<CMDType, int> NotCmdInt = new Dictionary<CMDType, int>();
    private Dictionary<CMDType, Action<bool>> CmdBoolDir = new Dictionary<CMDType, Action<bool>>();
    private Dictionary<CMDType, bool> NotCmdBool = new Dictionary<CMDType, bool>();
    public void OnInit() { }
    public void ExecuteCMD(CMDType type, int parm, bool isnotshow = false)
    {
        if (!CmdIntDir.ContainsKey(type))
        {
            if (isnotshow)
            {
                if (NotCmdInt.ContainsKey(type))
                {
                    NotCmdInt[type] = parm;
                }
                else
                {
                    NotCmdInt.Add(type, parm);
                }
            }
            return;
        }
        CmdIntDir[type].Invoke(parm);
    }
    public void ExecuteCMD(CMDType type, string parm, bool isnotshow = false)
    {
        if (!CmdStrDir.ContainsKey(type))
        {
            if (isnotshow)
            {
                if (NotCmdStr.ContainsKey(type))
                {
                    NotCmdStr[type] = parm;
                }
                else
                {
                    NotCmdStr.Add(type, parm);
                }
            }
            return;
        }
        CmdStrDir[type].Invoke(parm);
    }
    public void ExecuteCMD(CMDType type, bool parm, bool isnotshow = false)
    {
        if (!CmdBoolDir.ContainsKey(type))
        {
            if (isnotshow)
            {
                if (NotCmdBool.ContainsKey(type))
                {
                    NotCmdBool[type] = parm;
                }
                else
                {
                    NotCmdBool.Add(type, parm);
                }
            }
            return;
        }
        CmdBoolDir[type].Invoke(parm);
    }
    public void RegisterCMD(CMDType type, Action<int> action)
    {
        if (CmdIntDir.ContainsKey(type))
        {
            CmdIntDir[type] += action;
        }
        else
        {
            CmdIntDir.Add(type, action);
        }
        if (NotCmdInt.ContainsKey(type))
        {
            CmdIntDir[type].Invoke(NotCmdInt[type]);
            NotCmdInt.Remove(type);
        }
    }
    public void RegisterCMD(CMDType type, Action<string> action)
    {
        if (CmdStrDir.ContainsKey(type))
        {
            CmdStrDir[type] += action;
        }
        else
        {
            CmdStrDir.Add(type, action);
        }
        if (NotCmdStr.ContainsKey(type))
        {
            CmdStrDir[type].Invoke(NotCmdStr[type]);
            NotCmdStr.Remove(type);
        }
    }
    public void RegisterCMD(CMDType type, Action<bool> action)
    {
        if (CmdBoolDir.ContainsKey(type))
        {
            CmdBoolDir[type] += action;
        }
        else
        {
            CmdBoolDir.Add(type, action);
        }
        if (NotCmdBool.ContainsKey(type))
        {
            CmdBoolDir[type].Invoke(NotCmdBool[type]);
            NotCmdBool.Remove(type);
        }
    }
    public void LogOffCMD(CMDType type, Action<string> action)
    {
        if (CmdStrDir.ContainsKey(type))
        {
            CmdStrDir.Remove(type);
        }
    }
    public void LogOffCMD(CMDType type, Action<int> action)
    {
        if (CmdIntDir.ContainsKey(type))
        {
            CmdIntDir.Remove(type);
        }
    }
    public void LogOffCMD(CMDType type, Action<bool> action)
    {
        if (CmdBoolDir.ContainsKey(type))
        {
            CmdBoolDir.Remove(type);
        }
    }
}
public enum CMDType
{
#if false
    Int = 0,   
    String = 100, 
    Bool = 200,
#endif
}