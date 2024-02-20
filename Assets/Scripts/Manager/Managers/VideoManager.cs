using UnityEngine;
using UnityEngine.Video;
public class VideoManager : BaseManager<VideoManager>
{
    private VideoPlayer video;
    protected override void OnInit()
    {
        GameObject videoobject = new GameObject("视频播放器");
        video = videoobject.AddComponent<VideoPlayer>();
        video.targetTexture = Resources.Load<RenderTexture>(Constant.VIDEORENDERTEXTURE);
        GameObject parents = GameObject.Find("多媒体") ?? new GameObject("多媒体");
        videoobject.transform.parent = parents.transform;
        video.playOnAwake = false;
        video.source = VideoSource.Url;
    }
    public void PlayVideo(string videopath)
    {
        video.url = videopath;
        video.Play();
    }
    public void SetVolume(float volume)
    {
        video.SetDirectAudioVolume(0, volume);
    }
    public float GetVolume()
    {
        return video.GetDirectAudioVolume(0);
    }
    //public bool GetPlaying { get { return video.isPlaying; } }
    public bool SetPlayPause(bool paused = false)
    {
        if (paused)
        {
            video.Stop();
            return video.isPlaying;
        }
        if (video.isPaused)
        {
            video.Play();
        }
        else
        {
            video.Pause();
        }
        return video.isPlaying;
    }
    public bool SetPause(bool isStop = false)
    {
        if (isStop) { video.Stop(); }
        else { video.Pause(); }
        return video.isPlaying;
    }
    public bool SetMute()
    {
        video.SetDirectAudioMute(0, !video.GetDirectAudioMute(0));
        return video.GetDirectAudioMute(0);
    }
    public void ChangeSchedule(float value)
    {
        video.time = video.length * value;
    }
    public float GetSchedule()
    {
        return (float)video.time / (float)video.length;
    }
    public void SetLoop(bool value)
    {
        video.isLooping = value;
    }
    public bool IsLooping
    {
        get { return video.isLooping; }
    }
}