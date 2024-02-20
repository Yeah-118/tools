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
    private Dictionary<CMDType, Action<object>> cmdobj = new Dictionary<CMDType, Action<object>>();
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
        //if (needtween > 0)
        //{
        //    view.tran.localScale = Vector3.one * 0.01f;
        //    view.tran.DOScale(Vector3.one, needtween);
        //}
        Show();
    }
    public virtual void Navigation(params int[] param) { }
    public virtual void Navigation(params string[] strings) { }
    public virtual void Hide() { }
    public virtual void OnHide(float needtween, Action action)
    {
        foreach (var item in cmdobj)
        {
            LogOffCMD(item.Key, item.Value);
        }
        //if (needtween > 0)
        //{
        //    view.tran.localScale = Vector3.one;
        //    view.tran.DOScale(Vector3.one * 0.01f, needtween).OnComplete(
        //        delegate ()
        //        {

        //            view.OnHide();
        //            Hide();
        //            action?.Invoke();
        //        });
        //}
        //else
        //{
        //    view.OnHide();
        //    Hide();
        //}
        view.OnHide();
        Hide();
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
    public void RegisterCMD(CMDType type, Action<object> action)
    {
        if (cmdobj.ContainsKey(type))
        {
            cmdobj[type] = action;
        }
        else
        {
            cmdobj.Add(type, action);
        }
        module.RegisterCMD(type, action);
    }
    public void ExecuteCMD(CMDType type, object par, bool isnotshow = false)
    {
        module.ExecuteCMD(type, par, isnotshow);
    }
    public void LogOffCMD(CMDType type, Action<object> action)
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
    public Sprite GetMultiple(string path, string name)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == name)
            {
                return sprites[i];
            }
        }
        return null;
    }
    public void SetImage(Transform tran, Sprite sprite, bool isother = false, bool set = false)
    {
        Image image = tran.GetComponent<Image>();
        if (!isother)
        {
            SetImage(image, sprite, set);
        }
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
        Debug.Log(path);
        Sprite sprite = Resources.Load<Sprite>(path);
        Debug.Log(sprite);
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
    public void SetParent(GameObject game, Transform tran)
    {
        game.transform.SetParent(tran);
    }
    public void SetParent(string name, Transform tran)
    {
        GetTransform(name).SetParent(tran);
    }
    public void SetParent(Transform child, Transform tran)
    {
        child.SetParent(tran);
    }
    public void SetParent<T>(T component, Transform tran) where T : Component
    {
        component.transform.SetParent(tran);
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
    public void SetSize(Transform transform, float width, float height)
    {
        SetSize(transform.GetComponent<RectTransform>(), width, height);
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