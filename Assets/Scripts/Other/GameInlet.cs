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
        //RootResourceHelper.PlayVideo("������Ƶ");
        //RootResourceHelper.StartProgram("����");
        //RootResourceHelper.OpenWeb("https://www.xuexi.cn/");
        //RootResourceHelper.PlaySound("������");
        RootResourceHelper.PlaySound("����");
    }
    private void Update()
    {

    }
    private void OnDestroy()
    {

    }
}