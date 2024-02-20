using System.IO;
using UnityEngine;
public class RootResourceHelper
{
    private static string rootpath;
    public static string root
    {
        get
        {
            if (rootpath == null)
            {
                string path = string.Format("{0}/outpath.txt", Application.streamingAssetsPath);
                using (StreamReader sr = new StreamReader(path))
                {
                    string str = sr.ReadToEnd();
                    rootpath = str.Trim();
                    sr.Close();
                }
            }
            return rootpath;
        }
    }
    private static string GetFileAllName(string forder, string name)
    {
        string[] files = Directory.GetFiles(string.Format("{0}/{1}", root, forder), "*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (Path.GetFileNameWithoutExtension(files[i]) == name)
            {
                return files[i];
            }
        }
        return null;
    }
    public static void PlayVideo(string video)
    {
        string path = GetFileAllName("Video", video);
        VideoManager.GetInstance.PlayVideo("file://" + path);
    }
    public static Sprite SetImage(string picture, int width = 350, int high = 250)
    {
        string path = GetFileAllName("Picture", picture);// imagepath;
        Debug.Log(path);
        Sprite sprite;
        if (File.Exists(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;
            Texture2D texture = new Texture2D(width, high);
            texture.LoadImage(bytes);
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            sprite = Resources.Load<Sprite>("Image/Mising");
        }
        return sprite;
    }
    public static void StartProgram(string program)
    {
        string path = GetFileAllName("Pragram", program);
        ProgramHelper.StartProgram(path);
    }
    public static void OpenWeb(string url)
    {
        ProgramHelper.StartProgram(url);
    }
    public static string[] ReadData(string name, char separator = '\t')
    {
        string path = GetFileAllName("Config", name);
        using (StreamReader sr = new StreamReader(path))
        {
            if (sr == null) return null;
            string str = sr.ReadToEnd();
            string[] str1 = str.Split(separator);
            sr.Close();
            return str1;
        }
    }
    public static void PlaySound(string sound,bool islood = false)
    {
        string path = GetFileAllName("Sound", sound);
        Transform.FindObjectOfType<LoadSound>().StartPlaySound(path, islood);
    }
}