using UnityEditor;
public class ToolInlet
{
    [MenuItem("Yeah/Test", false, 1)]
    private static void SSTest()
    {
        YeahTools.TestForTools();
    }
    [MenuItem("Yeah/Config/Excel生成Txt配置", false, 11)]
    private static void ToConfig()
    {
        YeahTools.ToConfig();
    }
    [MenuItem("Yeah/Config/Excel设置配置", false, 12)]
    private static void ToExcel_C()
    {
        YeahTools.CreatExcel_C();
    }
    [MenuItem("Yeah/Scripts/Manager注册进GameIniet", false, 21)]
    private static void InGameIniet()
    {
        YeahTools.InGameIniet();
    }
    [MenuItem("Yeah/Scripts/创建C#", false, 22)]
    private static void CreatCshpe()
    {
        YeahTools.CreatCshpe();
    }
    [MenuItem("Yeah/项目/初始化",false ,31)]
    private static void ProjectInit()
    {
        YeahTools.ProjectInit();
    }
}
