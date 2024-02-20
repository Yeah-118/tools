using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BaseControl
{
    public int windowId;
    public WindowType windowType;
    private Dictionary<CMDType, Action<int>> cmdint = new Dictionary<CMDType, Action<int>>();
    private Dictionary<CMDType, Action<string>> cmdstr = new Dictionary<CMDType, Action<string>>();
    private Dictionary<CMDType, Action<bool>> cmdbool = new Dictionary<CMDType, Action<bool>>();
    private List<int> timerlist;
    protected BaseView view;
    protected Module module;

    public virtual void OnInit(BaseView baseview, int Id, Module windowmodule)
    {
        view = baseview;
        windowType = view.windowType;
        windowId = Id;
        module = windowmodule;
        timerlist = new List<int>();
    }
    public virtual void OnCreate() { }
    public virtual void OnDestroy() { }
    public virtual void Update() { }
    public virtual void Show() { }
    public virtual void OnShow(float needtween)
    {
        view.OnShow();
        view.game.SetActive(true);
        if (needtween > 0)
        {
            view.tran.localScale = Vector3.one * 0.01f;
            view.tran.DOScale(Vector3.one, needtween);
        }
        Show();
    }
    public virtual void Navigation(params int[] param) { }
    public virtual void Navigation(params string[] strings) { }
    public virtual void Hide() { }
    public virtual void OnHide(float needtween, Action action)
    {
        foreach (var item in cmdint)
        {
            LogOffCMD(item.Key, item.Value);
        }
        foreach (var item in cmdstr)
        {
            LogOffCMD(item.Key, item.Value);
        }
        foreach (var item in cmdbool)
        {
            LogOffCMD(item.Key, item.Value);
        }
        if (needtween > 0)
        {
            view.tran.localScale = Vector3.one;
            view.tran.DOScale(Vector3.one * 0.01f, needtween).OnComplete(
                delegate ()
                {

                    view.OnHide();
                    Hide();
                    action?.Invoke();
                });
        }
        else
        {
            view.OnHide();
            Hide();
        }

    }
    public void HideSelf(float tweentime = 0, Action callback = null)
    {
        WindowHelper.HideWindow(windowId, tweentime, callback);
    }
    public void ShowWindow(WindowID window, float needtween = 0)
    {
        WindowHelper.ShowWindow(window, needtween);
    }
    public void ShowWindow(int window, float needtween = 0)
    {
        WindowHelper.ShowWindow(window, needtween);
    }
    public void Navigation(WindowID windowId, params string[] strings)
    {
        WindowHelper.Navigation(windowId, strings);
    }
    public void Navigation(WindowID windowId, params int[] strings)
    {
        WindowHelper.Navigation(windowId, strings);
    }
    public void Navigation(int windowId, params string[] strings)
    {
        WindowHelper.Navigation(windowId, strings);
    }
    public void Navigation(int windowId, params int[] strings)
    {
        WindowHelper.Navigation(windowId, strings);
    }
    public void RegisterCMD(CMDType type, Action<string> action)
    {
        if (cmdstr.ContainsKey(type))
        {
            cmdstr[type] = action;
        }
        else
        {
            cmdstr.Add(type, action);
        }
        module.RegisterCMD(type, action);
    }
    public void RegisterCMD(CMDType type, Action<int> action)
    {
        if (cmdint.ContainsKey(type))
        {
            cmdint[type] = action;
        }
        else
        {
            cmdint.Add(type, action);
        }
        module.RegisterCMD(type, action);
    }
    public void RegisterCMD(CMDType type, Action<bool> action)
    {
        if (cmdbool.ContainsKey(type))
        {
            cmdbool[type] = action;
        }
        else
        {
            cmdbool.Add(type, action);
        }
        module.RegisterCMD(type, action);
    }
    public void ExecuteCMD(CMDType type, string par, bool isnotshow = false)
    {
        module.ExecuteCMD(type, par, isnotshow);
    }
    public void ExecuteCMD(CMDType type, int par, bool isnotshow = false)
    {
        module.ExecuteCMD(type, par, isnotshow);
    }
    public void ExecuteCMD(CMDType type, bool par, bool isnotshow = false)
    {
        module.ExecuteCMD(type, par, isnotshow);
    }
    public void LogOffCMD(CMDType type, Action<string> action)
    {
        module.LogOffCMD(type, action);
    }
    public void LogOffCMD(CMDType type, Action<int> action)
    {
        module.LogOffCMD(type, action);
    }
    public void LogOffCMD(CMDType type, Action<bool> action)
    {
        module.LogOffCMD(type, action);
    }
    public void SetImage(Image image)
    {
        image.sprite = null;
    }
    public void SetImage(string imagename, string path, bool set = false)
    {
        Image image = GetComment<Image>(imagename);
        SetImage(image, path, set);
    }

    public void SetImage(Transform tran, string path, bool isother = false, bool set = false)
    {
        Image image = tran.GetComponent<Image>();
        if (!isother)
        {
            SetImage(image, path, set);
        }
    }
    public void SetImage(Image image, string path, bool set = false)
    {
        Sprite sprite = Resources.Load<Sprite>(path);
        SetImage(image, sprite, set);
    }
    public void SetImage(Image image, Sprite sprite, bool set = false)
    {
        image.sprite = sprite;
        if (set)
        {
            image.SetNativeSize();
        }
    }
    public void AddBtnListener(Button button, UnityAction unity)
    {
        unity += delegate () { Debug.Log(string.Format("执行的是| {0} |的界面的| {1} |按钮方法", view.ToString(), button.name)); };

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(unity);
    }
    public void AddBtnListener(string name, UnityAction unity)
    {
        Button bt = GetTransform(name).GetComponent<Button>();
        AddBtnListener(bt, unity);
    }
    public void AddBtnListener(Transform tran, UnityAction unity)
    {
        Button button = tran.GetComponent<Button>();
        AddBtnListener(button, unity);
    }
    public void SetText(Text text, params string[] strings)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < strings.Length; i++)
        {
            builder.Append(strings[i]);
        }
        text.text = builder.ToString();
    }
    public void SetText(string name, params string[] strings)
    {
        Text text = GetTransform(name).GetComponent<Text>();
        SetText(text, strings);
    }
    public void SetText(Transform tran, params string[] strings)
    {
        Text text = tran.GetComponent<Text>();
        SetText(text, strings);
    }
    public void SetPosition(Transform transform, float x, float y)
    {
        transform.localPosition = new Vector3(x, y);
    }
    public void SetPosition(string name, float x, float y)
    {
        SetPosition(GetTransform(name), x, y);
    }
    public void SetPosition(Transform transform, Vector3 vector)
    {
        transform.localPosition = vector;
    }
    public void SetPosition(string name, Vector3 vector)
    {
        SetPosition(GetTransform(name), vector);
    }
    public Vector2 GetSize(string name)
    {
        return GetTransform(name).GetComponent<RectTransform>().sizeDelta;
    }
    public Vector2 GetSize(Transform tran)
    {
        return tran.GetComponent<RectTransform>().sizeDelta;
    }
    public Vector2 GetSize(GameObject game)
    {
        return game.transform.GetComponent<RectTransform>().sizeDelta;
    }
    public void SetSize(string name, float width, float height)
    {
        GetTransform(name).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
    public void SetSize(GameObject obj, float width, float height)
    {
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
    public void SetSize(RectTransform transform, float width, float height)
    {
        transform.sizeDelta = new Vector2(width, height);
    }
    public void SetActive(GameObject game, bool active)
    {
        game.SetActive(active);
    }
    public void SetActive(Transform tran, bool active)
    {
        tran.gameObject.SetActive(active);
    }
    public void SetActive(string name, bool active)
    {
        Transform transform = GetTransform(name);
        SetActive(transform, active);
    }
    public void SetActive<T>(T com, bool active) where T : Component
    {
        com.gameObject.SetActive(active);
    }
    public Transform GetTransform(string path)
    {
        return view.GetTransform(path);
    }
    public T GetComment<T>(string path) where T : Component
    {
        return GetTransform(path).GetComponent<T>();
    }
    public T GetComment<T>(GameObject game) where T : Component
    {
        return game.GetComponent<T>();
    }
    public T GetComment<T>(Transform tran) where T : Component
    {
        return tran.GetComponent<T>();
    }
}