using UnityEngine;
public class SoundManager : BaseManager<SoundManager>
{
    private AudioSource audio;
    private int loodid;
    protected override void OnInit()
    {
        audio = new GameObject("��Ƶ������").AddComponent<AudioSource>();
        GameObject parents = GameObject.Find("��ý��") ?? new GameObject("��ý��");
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
        AudioSource loopaudio = new GameObject("ѭ������" + GetSourceId()).AddComponent<AudioSource>();
        GameObject parents = GameObject.Find("��ý��") ?? new GameObject("��ý��");
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