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
    CMDType1,
    CMDType2,
    CMDType3,
    CMDType4,
    CMDType5,
    CMDType6,
    CMDType7,
    CMDType8,
    CMDType9,
    CMDType10,
    CMDType11,
    CMDType12,
    CMDType13,
    CMDType14,
    CMDType15,
    CMDType16,
    CMDType17,
    CMDType18,
    CMDType19,
    CMDType20,
    CMDType21,
    CMDType22,
    CMDType23,
    CMDType24,
    CMDType25,
    CMDType26,
    CMDType27,
    CMDType28,
    CMDType29,
    CMDType30,
    CMDType31,
    CMDType32,
    CMDType33,
    CMDType34,
    CMDType35,
    CMDType36,
    CMDType37,
    CMDType38,
    CMDType39,
    CMDType40,
    CMDType41,
    CMDType42,
    CMDType43,
    CMDType44,
    CMDType45,
    CMDType46,
    CMDType47,
    CMDType48,
    CMDType49,
    CMDType50,
}