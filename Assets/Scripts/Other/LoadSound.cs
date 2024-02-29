using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class LoadSound : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void StartPlaySound(string path, bool islood = false)
    {
        StartCoroutine(AudioWWW(path, islood));
    }
    private IEnumerator AudioWWW(string _url, bool islood = false)
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(_url, AudioType.UNKNOWN);
        yield return www.SendWebRequest();
        AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
        if (islood)
        {
            SoundManager.GetInstance.PlayLoopSound(clip);
        }
        else
        {
            SoundManager.GetInstance.PlaySound(clip);
        }
    }
}