using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHelper
{
    public static void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public static void LoadScene(int buildIndex)
    {
        WindowHelper.HideAllWindow();
        SceneManager.LoadScene(buildIndex);
    }
    public static void LoadScene(string scenename)
    {
        WindowHelper.HideAllWindow();
        SceneManager.LoadScene(scenename);
    }
    public static string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void Again()
    {  
        LoadScene(GetSceneName());
    }
    public static int GetSceneId()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}