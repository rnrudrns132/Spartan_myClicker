using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void Initiailzer()
    {
        BGMdicts = new Dictionary<BGMEnum, AudioClip>();
        foreach (var t in BGMs)
        {
            BGMdicts.Add(t.myEnum, t.myClip);
        }

        SFXdicts = new Dictionary<SFXEnum, AudioClip>();
        foreach (var t in SFXs)
        {
            SFXdicts.Add(t.myEnum, t.myClip);
        }
    }

    [SerializeField] private AudioSource BGMsource;
    private Dictionary<BGMEnum, AudioClip> BGMdicts;
    [SerializeField] private AudioClipClass<BGMEnum>[] BGMs;
    public void PlayBGM(BGMEnum target)
    {
        BGMsource.clip = BGMdicts[target];
        BGMsource.Play();
    }

    [SerializeField] private AudioSource SFXsource;
    private Dictionary<SFXEnum, AudioClip> SFXdicts;
    [SerializeField] private AudioClipClass<SFXEnum>[] SFXs;
    public void PlaySFX(SFXEnum target)
    {
        SFXsource.PlayOneShot(SFXdicts[target]);
    }
}
[System.Serializable]
public class AudioClipClass<T>
{
    public T myEnum;
    public AudioClip myClip;
}

public enum BGMEnum
{
    TITLE,
    MAIN
}
public enum SFXEnum
{
    UI_CLICK,
    UI_CLOSE,
    ALERT,
    ATTACK,
    UPGRADE,
    DEAD,
    CRITICAL
}
