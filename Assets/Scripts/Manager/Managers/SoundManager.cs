using UnityEngine;
public class SoundManager : BaseManager<SoundManager>
{
    private AudioSource audio;
    private int loodid;
    protected override void OnInit()
    {
        audio = new GameObject("音频播放器").AddComponent<AudioSource>();
        GameObject parents = GameObject.Find("多媒体") ?? new GameObject("多媒体");
        audio.transform.parent = parents.transform;
    }
    public void Stop()
    {
        audio.Stop();
    }
    public void PlayAndPause()
    {
        if (audio.isPlaying)
        {
            audio.Pause();
        }
        else
        {
            audio.Play();
        }
    }
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        audio.clip = clip;
        audio.Play();
    }
    public void PlaySound(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }
    private int GetSourceId()
    {
        return loodid++;
    }
    AudioSource GetLoopSource()
    {
        AudioSource loopaudio = new GameObject("循环音乐" + GetSourceId()).AddComponent<AudioSource>();
        GameObject parents = GameObject.Find("多媒体") ?? new GameObject("多媒体");
        loopaudio.transform.parent = parents.transform;
        loopaudio.loop = true;
        return loopaudio;
    }
    public void PlayLoopSound(AudioClip clip)
    {
        var loopaudio = GetLoopSource();
        loopaudio.clip = clip;
        loopaudio.Play();
    }
    public void PlayLoopSound(string name)
    {
        var loopaudio = GetLoopSource();
        AudioClip clip = Resources.Load<AudioClip>(name);
        loopaudio.clip = clip;
        loopaudio.Play();
    }
}