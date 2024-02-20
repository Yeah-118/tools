using UnityEngine;
public class GameInlet : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //Transform.FindObjectOfType<Image>().sprite = RootResourceHelper.SetImage("test");
        //RootResourceHelper.PlayVideo("消防视频");
        //RootResourceHelper.StartProgram("长征");
        //RootResourceHelper.OpenWeb("https://www.xuexi.cn/");
        //RootResourceHelper.PlaySound("真美啊");
        RootResourceHelper.PlaySound("错误");
    }
    private void Update()
    {

    }
    private void OnDestroy()
    {

    }
}