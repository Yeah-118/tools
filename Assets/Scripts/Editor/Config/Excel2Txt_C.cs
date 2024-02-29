using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Excel2Txt_C", menuName = "Config/Excle", order = 0)]
public class Excel2Txt_C : ScriptableObject
{
    public string worksheetName;
    public List<string> exportPath;
    public List<bool> isExport;
    public void AddPath(string path,bool isexport = true)
    {
        exportPath = exportPath ?? new List<string>();
        isExport = isExport ?? new List<bool>();
        exportPath.Add(path);
        isExport.Add(isexport);
    }
}
