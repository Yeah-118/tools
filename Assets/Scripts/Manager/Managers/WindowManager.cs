using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class WindowManager : BaseManager<WindowManager>
{
    private Dictionary<int, List<string>> windowDefault;
    private Module module = new Module();
    private Dictionary<int, BaseControl> haveshowWindow;
    private List<BaseControl> controls;
    protected override void OnInit()
    {
        module.OnInit();
        haveshowWindow = new Dictionary<int, BaseControl>();
        windowDefault = new Dictionary<int, List<string>>();
        controls = new List<BaseControl>();
        string alltext = Resources.Load<TextAsset>("Config/WindowDefault").text;
        string[] nums = alltext.Split('\n');
        for (int i = 1; i < nums.Length; i++)
        {
            string[] data = nums[i].Split('\t');
            List<string> list = new List<string>();
            for (int j = 1; j < data.Length; j++)
            {
                list.Add(data[j].Trim());
            }
            windowDefault.Add(Convert.ToInt32(data[0]), list);
        }
    }
    public override void Update()
    {
        for (int i = 0; i < controls.Count; i++)
        {
            controls[i].Update();
        }
    }
    public Transform GetCanvas()
    {
        return GameObject.Find("Canvas").transform;
    }
    public Transform GetMainLayout()
    {
        GameObject MainWindow = GameObject.Find("MainWindow") ?? new GameObject("MainWindow");
        MainWindow.transform.parent = GetCanvas().transform;
        MainWindow.transform.localPosition = Vector3.zero;
        MainWindow.transform.localScale = Vector3.one;
        return MainWindow.transform;
    }
    public Transform GetPopUpLayout()
    {
        GameObject PopUpWindow = GameObject.Find("PopUpWindow") ?? new GameObject("PopUpWindow");
        PopUpWindow.transform.parent = GetCanvas().transform;
        PopUpWindow.transform.localPosition = Vector3.zero;
        PopUpWindow.transform.localScale = Vector3.one;
        return PopUpWindow.transform;
    }
    public Transform GetTipsLayout()
    {
        GameObject TipsWindow = GameObject.Find("TipsWindow") ?? new GameObject("TipsWindow");
        TipsWindow.transform.parent = GetCanvas().transform;
        TipsWindow.transform.localPosition = Vector3.zero;
        TipsWindow.transform.localScale = Vector3.one;
        return TipsWindow.transform;
    }
    public Transform GetGuide()
    {
        GameObject GetGuide = GameObject.Find("GuideWindow") ?? new GameObject("GuideWindow");
        GetGuide.transform.parent = GetCanvas().transform;
        GetGuide.transform.localPosition = Vector3.zero;
        GetGuide.transform.localScale = Vector3.one;
        return GetGuide.transform;
    }
    public bool IsWindowShow(int windowId, out BaseControl control)
    {
        for (int i = 0; i < controls.Count; i++)
        {
            if (controls[i].windowId.Equals(windowId))
            {
                control = controls[i];
                return true;
            }
        }
        control = null;
        return false;
    }
    public void HideAll()
    {
        for (int i = 0; i < controls.Count; i++)
        {
            HideWindow(controls[i].windowId);
        }
    }
    public void HideWindow(WindowID window, float tewentime = 0, Action action = null)
    {
        int windowid = (int)window;
        HideWindow(windowid, tewentime, action);
    }
    public void HideWindow(int windowId, float tewentime = 0, Action action = null)
    {
        BaseControl control = null;
        if (IsWindowShow(windowId, out control))
        {
            controls.Remove(control);
            control.OnHide(tewentime, action);
        }
    }
    public BaseControl ShowWindow(WindowID window, float needtween = 0)
    {
        int windowid = (int)window;
        return ShowWindow(windowid, needtween);
    }
    public BaseControl ShowWindow(int windowId, float needtween = 0)
    {
        if (windowId == 0) { return null; }

        if (haveshowWindow.ContainsKey(windowId))
        {
            BaseControl control = haveshowWindow[windowId];
            control.OnShow(needtween);
            if (!controls.Contains(control)) { controls.Add(control); }

            return control;
        }
        List<string> windowdata = windowDefault[windowId];
        Type View = Type.GetType(windowdata[0] + "View");
        Type Control = Type.GetType(windowdata[0] + "Control");
        object Viewinstance = Activator.CreateInstance(View);
        object Controlinstance = Activator.CreateInstance(Control);
        MethodInfo ViewOnInit = View.GetMethod("OnInit");
        MethodInfo ViewOnCreate = View.GetMethod("OnCreate");
        MethodInfo ControlOnInit = Control.GetMethod("OnInit");
        MethodInfo ControlOnCreate = Control.GetMethod("OnCreate");
        MethodInfo ControlOnShow = Control.GetMethod("OnShow");
        ViewOnInit.Invoke(Viewinstance, new object[] { windowdata[1], windowId, module, windowdata[0] });
        ViewOnCreate.Invoke(Viewinstance, new object[] { });
        ControlOnInit.Invoke(Controlinstance, new object[] { Viewinstance, windowId, module });
        ControlOnCreate.Invoke(Controlinstance, new object[] { });
        ControlOnShow.Invoke(Controlinstance, new object[] { needtween });
        haveshowWindow.Add(windowId, Controlinstance as BaseControl);
        controls.Add(Controlinstance as BaseControl);
        return Controlinstance as BaseControl;
    }
    public void Navigation(WindowID window, params string[] strings)
    {
        Navigation((int)window, strings);
    }
    public void Navigation(int windowId, params string[] strings)
    {
        ShowWindow(windowId);
        BaseControl control = haveshowWindow[windowId];
        if (control != null)
        {
            control.Navigation(strings);
        }
    }
    public void Navigation(WindowID window, params int[] param)
    {
        Navigation((int)window, param);
    }
    public void Navigation(int windowId, params int[] param)
    {
        ShowWindow(windowId);
        BaseControl control = haveshowWindow[windowId];
        if (control != null)
        {
            control.Navigation(param);
        }
    }
    public void ExecuteCMD(CMDType type, int parm, bool isnotshow = false)
    {
        module.ExecuteCMD(type, parm, isnotshow);
    }
    public void ExecuteCMD(CMDType type, string parm, bool isnotshow = false)
    {
        module.ExecuteCMD(type, parm, isnotshow);
    }
    public void ExecuteCMD(CMDType type, bool parm, bool isnotshow = false)
    {
        module.ExecuteCMD(type, parm, isnotshow);
    }
}
public enum WindowType
{
    Main = 1,
    PopUp = 2,
    Tips = 3,
    Guide,
}
public enum WindowID
{
#if false
ÆúÓÃ
#endif
}