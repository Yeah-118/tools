using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
public class ProgramHelper
{
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    public static void WinMinimize()
    {
        ShowWindow(GetForegroundWindow(), Constant.SW_SHOWMINIMIZED);
    }
    public static void StartProgram(string path)
    {
        try
        {
            Process.Start(path);
        }
        catch
        {
        }
    }
    public void OpenFile()
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.structSize = Marshal.SizeOf(ofd);
        ofd.filter = "图片文件(*.jpg*.png)\0*.jpg;*.png";
        ofd.file = new string(new char[256]);
        ofd.maxFile = ofd.file.Length;
        ofd.fileTitle = new string(new char[64]);
        ofd.maxFileTitle = ofd.fileTitle.Length;
        ofd.initialDir = @"C:";//默认路径
        ofd.title = "Open File Dialog";
        //ofd.defExt = "JPG";
        ofd.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (DialogShow.GetOpenFileName(ofd))
        {
            UnityEngine.Debug.Log(ofd.file);
        }
    }
}