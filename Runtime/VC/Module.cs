using System;
using System.Collections.Generic;
public class Module
{
    private Dictionary<CMDType, Action<object>> CmdObjDir = new Dictionary<CMDType, Action<object>>();
    private Dictionary<CMDType, object> NotCmdObj = new Dictionary<CMDType, object>();
    public void OnInit() { }
    public void ExecuteCMD(CMDType type, object parm, bool isnotshow = false)
    {
        if (!CmdObjDir.ContainsKey(type))
        {
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
}
public enum CMDType
{
#if false
    Int = 0,   
    String = 100, 
    Bool = 200,
#endif
}