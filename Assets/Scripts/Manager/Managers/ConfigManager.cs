using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class ConfigManager : BaseManager<ConfigManager>
{
    private Dictionary<string, List<ConfigData>> configall = new Dictionary<string, List<ConfigData>>();

    protected override void OnInit()
    {
        string path = Application.dataPath + "/Resources/Config";
        string[] files = Directory.GetFiles(path, "*txt");
        for (int i = 0; i < files.Length; i++)
        {
            string filename = Path.GetFileNameWithoutExtension(files[i]);
            List<ConfigData> templist = new List<ConfigData>();
            if (filename != "WindowDefault")
                if (!configall.ContainsKey(filename))
                {
                    string alltext = Resources.Load<TextAsset>("Config/" + filename).text;
                    string[] nums = alltext.Split('\n');
                    for (int num = 1; num < nums.Length; num++)
                    {

                        string[] data = nums[num].Split('\t');
                        List<string> list = new List<string>();
                        for (int j = 1; j < data.Length; j++)
                        {
                            list.Add(data[j].Trim());
                        }
                        ConfigData config = new ConfigData(int.Parse(data[0]), list.ToArray());
                        templist.Add(config);
                    }
                }
            configall.Add(filename, templist);
        }
    }
    public ConfigData GetConfigData(string file, int id)
    {
        if (configall.ContainsKey(file))
        {
            List<ConfigData> temp = configall[file];
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Id == id)
                {
                    return temp[i];
                }
            }
        }
        return null;
    }
    public int GetConfigCount(string file)
    {
        if (configall.ContainsKey(file))
        {
            return configall[file].Count;
        }
        return 0;
    }
    public List<ConfigData> GetConfig(string file)
    {
        if (configall.ContainsKey(file))
        {
            return configall[file];
        }
        return null;
    }
}
public class ConfigData
{
    public int Id;
    public string[] Parameter;
    public ConfigData(int id)
    {
        Id = id;
    }
    public ConfigData(int id, params string[] parameter)
    {
        Id = id;
        Parameter = parameter;
    }
}