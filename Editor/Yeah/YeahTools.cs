using OfficeOpenXml;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public class YeahTools
{
    public static void TestForTools()
    {
        Debug.Log("测试专用！！！！");
        Debug.Log(Application.dataPath);
        Debug.Log(Path.GetDirectoryName(Application.dataPath));
        string path = Path.GetDirectoryName(Application.dataPath);
        //EditorUtility.DisplayProgressBar("标题", "进度条内容", 12 / 50);
        //EditorUtility.DisplayDialog("标题", "询问？", "确定", "取消");

        int hour = System.DateTime.Now.Hour;
        int minute = System.DateTime.Now.Minute;
        int second = System.DateTime.Now.Second;
        int year = System.DateTime.Now.Year;
        int month = System.DateTime.Now.Month;
        int day = System.DateTime.Now.Day;
        Debug.Log(System.DateTime.Now);
        Debug.Log(string.Format("{0}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", year, month, day, hour, minute, second));

        path = Path.GetDirectoryName(Application.dataPath).Substring(Path.GetDirectoryName(Application.dataPath).LastIndexOf("\\") + 1);
        Debug.Log(path);
    }
    public static void ToConfig()
    {
        string path1 = Path.GetDirectoryName(Application.dataPath);
        path1 = path1.Substring(path1.LastIndexOf("\\") + 1);
        string path = Application.dataPath.Replace("Assets", string.Format("{0}.xlsx", path1));
        FileInfo fileInfo = new FileInfo(path);
        using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        {
            ExcelWorksheets worksheets = excelPackage.Workbook.Worksheets;
            Debug.Log(worksheets.Count);
            for (int i = 1; i <= worksheets.Count; i++)
            {
                int rows, cols;
                ExcelWorksheet worksheet = worksheets[i];
                ToolsAdditional.GetMaxRowsCols(worksheet, out rows, out cols);
                StringBuilder builder = new StringBuilder();
                for (int r = 1; r <= rows; r++)
                {
                    for (int c = 1; c <= cols; c++)
                    {
                        builder.Append(worksheet.Cells[r, c].Value);
                        if (c != cols)
                        {
                            builder.Append('\t');
                        }
                    }
                    if (r != rows)
                    {
                        builder.Append('\n');
                    }
                }
                ToolsAdditional.WriteConfig(worksheet.Name, builder.ToString());
                Debug.Log(string.Format("表：{0}已经写完了！！", worksheet.Name));
            }
            AssetDatabase.Refresh();
        }
    }
    public static void CreatExcel_C()
    {
        string path1 = Path.GetDirectoryName(Application.dataPath);
        path1 = path1.Substring(path1.LastIndexOf("\\") + 1);
        string path = Application.dataPath.Replace("Assets", string.Format("{0}.xlsx", path1));
        FileInfo fileInfo = new FileInfo(path);
        using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        {
            ExcelWorksheets worksheets = excelPackage.Workbook.Worksheets;
            for (int i = 1; i <= worksheets.Count; i++)
            {
                ExcelWorksheet worksheet = worksheets[i];
                Excel2Txt_C Loadtxt = Resources.Load<Excel2Txt_C>("Config/ScriptableObject/" + worksheet.Name);
                if (Loadtxt == null)
                {
                    Excel2Txt_C txt_C = ScriptableObject.CreateInstance<Excel2Txt_C>();
                    txt_C.worksheetName = worksheet.Name;
                    txt_C.AddPath(Application.dataPath + "/Resources/Config/" + worksheet.Name + ".txt", true);
                    AssetDatabase.CreateAsset(txt_C, string.Format("Assets/Resources/Config/ScriptableObject/{0}.asset", worksheet.Name));
                    AssetDatabase.SaveAssets();
                    Debug.Log(string.Format("Excel_C：{0}已经写完了！！", worksheet.Name));
                }
                else
                {
                    Debug.Log(string.Format("Excel_C：{0}已经存在，不用生成！！", worksheet.Name));
                }
            }
            AssetDatabase.Refresh();
        }
    }
    public static void CreatCshpe()
    {
        string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string targetpath = ToolsAdditional.DealWithPath(Application.dataPath, folderPath);
        string[] name = targetpath.Split('/');
        string filemame = name[name.Length - 1];
        string viewpath = targetpath + "/" + filemame + "View.cs";
        string controlpath = targetpath + "/" + filemame + "Control.cs";
        using (StreamWriter viewsw = new StreamWriter(viewpath))
        {
            viewsw.WriteLine(ToolsAdditional.GetViewText(filemame));
            viewsw.Close();
            Debug.Log(string.Format("写完了{0}View", filemame));
        }
        using (StreamWriter controlsw = new StreamWriter(controlpath))
        {
            controlsw.WriteLine(ToolsAdditional.GetControlText(filemame));
            controlsw.Close();
            Debug.Log(string.Format("写完了{0}Control", filemame));
        }
        AssetDatabase.Refresh();
    }
    public static void InGameIniet()
    {
        string targetpath = Application.dataPath + "/Scripts/GameInlet.cs";
        string jiaoben;
        using (StreamReader sr = new StreamReader(targetpath))
        {
            jiaoben = sr.ReadToEnd();
        }
        string[] replacename = { "Update", "OnDestroy" };
        for (int i = 0; i < replacename.Length; i++)
        {
            string originalmethod = ToolsAdditional.GetMethodString(jiaoben, string.Format("private void {0}()", replacename[i]));
            string latermethod = ToolsAdditional.AddStringToMethod(originalmethod, ToolsAdditional.GetReplaceString(replacename[i]));
            jiaoben = jiaoben.Replace(originalmethod, latermethod);
            Debug.Log(string.Format("{0}已经写完了！", replacename[i]));
        }
        using (StreamWriter sw = new StreamWriter(targetpath))
        {
            sw.Write(jiaoben);
            sw.Flush();
            sw.Close();
        }
        AssetDatabase.Refresh();
        Debug.Log("刷新完成，管理器已经写入GameIniet中");
    }
    public static void ProjectInit()
    {
        string[] strings = {
            Application.streamingAssetsPath,
            Application.dataPath + "/Resources",
            Application.dataPath + "/Resources/Config",
            Application.dataPath + "/Resources/Perfabs",
            Application.dataPath + "/Resources/Texture",
            Application.dataPath + "/Resources/Config/ScriptableObject",
        };
        for (int i = 0; i < strings.Length; i++)
        {
            ToolsAdditional.CreateDirectory(strings[i]);
        }
        ToolsAdditional.ProjectSetting();
        AssetDatabase.Refresh();
        Debug.Log("项目初始化");
    }
}