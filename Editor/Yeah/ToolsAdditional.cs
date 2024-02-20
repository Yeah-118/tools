using OfficeOpenXml;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public class ToolsAdditional
{
    public static void GetMaxRowsCols(ExcelWorksheet worksheet, out int rows, out int cols)
    {
        rows = 0;
        cols = 0;
        for (int i = worksheet.Cells.Start.Column; i <= worksheet.Cells.End.Column; i++)
        {
            if (worksheet.Cells[1, i].Value == null)
            {
                cols = --i;
                break;
            }
        }
        for (int i = worksheet.Cells.Start.Row; i <= worksheet.Cells.End.Row; i++)
        {
            if (worksheet.Cells[i, 1].Value == null)
            {
                rows = --i;
                break;
            }
        }
    }
    public static void WriteConfig(string filenema, string msg)
    {
        Excel2Txt_C txt_C = Resources.Load<Excel2Txt_C>("Config/ScriptableObject/" + filenema);
        //Debug.Log(txt_C.export + "name" + filenema);
        for (int i = 0; i < txt_C.isExport.Count; i++)
        {
            if (txt_C.isExport[i])
            {
                string path = txt_C.exportPath[i];
                StreamWriter writer = new StreamWriter(path);
                writer.Write(msg);
                writer.Close();
            }
        }
        //string path = Application.dataPath + "/Resources/Config/" + filenema + ".txt";
        //StreamWriter writer = new StreamWriter(path);
        //writer.Write(msg);
        //writer.Close();
    }
    public static string DealWithPath(string dataPath, string folderPath)
    {
        string newstring = folderPath.Substring(6, folderPath.Length - 6);
        return dataPath + newstring;
    }
    public static string GetViewText(string filename)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("public class ");
        sb.Append(filename + "View : BaseView\n{\n}");
        return sb.ToString();
    }
    public static string GetControlText(string filename)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("public class ");
        sb.Append(filename + "Control : BaseControl\n{\n}");
        return sb.ToString();
    }
    public static string GetMethodString(string sprites, string name)
    {
        StringBuilder sb = new StringBuilder();
        int start = sprites.IndexOf(name);
        for (int i = start; i < sprites.Length; i++)
        {
            sb.Append(sprites[i]);
            if (sprites[i] == '}')
            {
                break;
            }
        }
        return sb.ToString();
    }

    public static string AddStringToMethod(string method, string add)
    {
        int start = method.IndexOf("{");
        method = method.Replace(method.Substring(start + 1), "\r\n" + add + "\n    }");
        return method;
    }
    public static string GetReplaceString(string methodname)
    {
        string path = Application.dataPath + "/Scripts/Manager/Managers";
        string[] files = Directory.GetFiles(path, "*.cs", SearchOption.TopDirectoryOnly);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < files.Length; i++)
        {
            sb.Append(string.Format("        {0}.GetInstance.{1}();{2}", Path.GetFileNameWithoutExtension(files[i]), methodname, "\r"));
        }
        return sb.ToString();
    }
    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log(string.Format("创建了|   {0}   |文件夹！", path));
        }
    }
    public static void ProjectSetting()
    {
        PlayerSettings.defaultIsNativeResolution = false;
        PlayerSettings.defaultScreenHeight = 1080;
        PlayerSettings.defaultScreenWidth = 1920;
        PlayerSettings.companyName = "WanxiangThcn";
    }
}