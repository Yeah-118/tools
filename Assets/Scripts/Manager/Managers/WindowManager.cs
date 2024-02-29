using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
public class WindowManager : BaseManager<WindowManager>
{
    private Dictionary<int, List<string>> windowDefault;
    private Module module = new Module();
    private Dictionary<int, BaseControl> haveshowWindow;
    private List<BaseControl> controls;
    protected override void OnInit()
    {
        module.OnInit(this);
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

    private void AddCanvas(GameObject window, int order)
    {
        Canvas canvas = window.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = window.AddComponent<Canvas>();
            window.AddComponent<GraphicRaycaster>();
            canvas.sortingOrder = order;
        }
        window.transform.SetParent(GetCanvas().transform);
        window.transform.localPosition = Vector3.zero;
        window.transform.localScale = Vector3.one;
        canvas.overrideSorting = true;
    }
    public Transform GetMainLayout()
    {
        GameObject MainWindow = GameObject.Find("MainWindow") ?? new GameObject("MainWindow");
        AddCanvas(MainWindow, 1);
        return MainWindow.transform;
    }
    public Transform GetPopUpLayout()
    {
        GameObject PopUpWindow = GameObject.Find("PopUpWindow") ?? new GameObject("PopUpWindow");
        AddCanvas(PopUpWindow, 2);
        return PopUpWindow.transform;
    }
    public Transform GetTipsLayout()
    {
        GameObject TipsWindow = GameObject.Find("TipsWindow") ?? new GameObject("TipsWindow");
        AddCanvas(TipsWindow, 3);
        return TipsWindow.transform;
    }
    public Transform GetGuide()
    {
        GameObject GetGuide = GameObject.Find("GuideWindow") ?? new GameObject("GuideWindow");
        AddCanvas(GetGuide, 4);
        return GetGuide.transform;
    }
    public bool IsWindowShowIng(int windowId)
    {
        BaseControl control;
        return IsWindowShow(windowId, out control);
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
    public void HideAll(int window = 0)
    {
        for (int i = 0; i < controls.Count; i++)
        {
            if (controls[i].windowId != window)
            {
                HideWindow(controls[i].windowId);
            }
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
    public void ExecuteCMD(CMDType type, object parm, bool isnotshow = false)
    {
        module.ExecuteCMD(type, parm, isnotshow);
    }
}
public enum WindowType
{
    Main = 100,
    PopUp = 200,
    Tips = 300,
    Guide = 400,
}
public enum WindowID
{
#if false
ÆúÓÃ
#endif
}