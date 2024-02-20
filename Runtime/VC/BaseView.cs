using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseView
{
    public GameObject game;
    public WindowType windowType;
    public Transform tran;
    public Module module;
    public Dictionary<string, Transform> gameAllTran;
    protected int windowId;
    private string path;
    private string winname;
    public void OnInit(string perfabpath, int WindowId, Module windowmodule, string name)
    {
        windowId = WindowId;
        path = perfabpath;
        module = windowmodule;
        gameAllTran = new Dictionary<string, Transform>();
        winname = name + "Panel";
    }
    protected virtual void InitPanel(WindowType type = WindowType.Main, float offsetX = 0, float offsetY = 0)
    {
        windowType = type;
        GameObject panel = Resources.Load(path) as GameObject;
        game = GameObject.Instantiate(panel);
        Transform parents = WindowManager.GetInstance.GetMainLayout();
        switch (type)
        {
            case WindowType.Main:
                parents = WindowManager.GetInstance.GetMainLayout();
                break;
            case WindowType.PopUp:
                parents = WindowManager.GetInstance.GetPopUpLayout();
                break;
            case WindowType.Tips:
                parents = WindowManager.GetInstance.GetTipsLayout();
                break;
            case WindowType.Guide:
                parents = WindowManager.GetInstance.GetGuide();
                break;
            default:
                break;
        }
        Vector2 offset = new Vector2(offsetX, offsetY);
        game.transform.SetParent(parents);
        tran = game.transform;
        game.transform.localPosition = Vector2.zero + offset;
        GetAllChildTransform(tran, true);
    }
    public Transform GetTransform(string name)
    {
        if (gameAllTran.ContainsKey(name))
        {
            return gameAllTran[name];
        }
        else
        {
            Debug.LogWarning(string.Format("ÕÒ²»µ½{0}Â·¾¶", name));
        }
        return null;
    }
    public virtual void OnCreate()
    {
        InitPanel();
        game.name = winname;
    }
    public virtual void OnShow() { }
    public virtual void OnHide()
    {
        game.SetActive(false);
    }
    public void ExecuteCMD(CMDType type, object par, bool isnotshow = false)
    {
        module.ExecuteCMD(type, par, isnotshow);
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
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(unity);
    }
    public void SetText(string name, params string[] strings)
    {
        Text text = GetTransform(name).GetComponent<Text>();
        SetText(text, strings);
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
    public bool GetSelfActive()
    {
        return game.activeSelf;
    }
    private void GetAllChildTransform(Transform parents, bool isBigParent = false, int parentid = 0)
    {
        for (int i = 0; i < parents.transform.childCount; i++)
        {
            Transform child = parents.transform.GetChild(i);
            string newname = isBigParent ? child.name : string.Format("{0}_{1}_{2}", parentid, parents.name, child.name);
            gameAllTran.Add(newname, child);
            GetAllChildTransform(child, false, i);
        }
    }
}