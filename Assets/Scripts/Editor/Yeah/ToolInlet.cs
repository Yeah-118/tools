using UnityEditor;
public class ToolInlet
{
    [MenuItem("Yeah/Test", false, 1)]
    private static void SSTest()
    {
        YeahTools.TestForTools();
    }
    [MenuItem("Yeah/Config/Excel����Txt����", false, 11)]
    private static void ToConfig()
    {
        YeahTools.ToConfig();
    }
    [MenuItem("Yeah/Config/Excel��������", false, 12)]
    private static void ToExcel_C()
    {
        YeahTools.CreatExcel_C();
    }
    [MenuItem("Yeah/Scripts/Managerע���GameIniet", false, 21)]
    private static void InGameIniet()
    {
        YeahTools.InGameIniet();
    }
    [MenuItem("Yeah/Scripts/����C#", false, 22)]
    private static void CreatCshpe()
    {
        YeahTools.CreatCshpe();
    }
    [MenuItem("Yeah/��Ŀ/��ʼ��",false ,31)]
    private static void ProjectInit()
    {
        YeahTools.ProjectInit();
    }
}
