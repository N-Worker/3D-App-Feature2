using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("รายการเพลงทั้งหมด")]
    public AudioClip[] bgmClips;

    [Header("ตั้งค่าเริ่มต้น")]
    public int bgmIndexToPlayOnStart = 0;

    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;

    public float fadeDuration = 1f;

    [Header("AudioSource")]
    public AudioSource bgmSource;

    private int currentBGMIndex = 0;

    private void Start()
    {
        if (bgmClips.Length > 0 && bgmIndexToPlayOnStart < bgmClips.Length)
        {
            PlayBGM(bgmClips[bgmIndexToPlayOnStart]);
        }
    }

    public void PlayBGM(AudioClip newClip)
    {
        if (newClip == null || bgmSource == null) return;
        if (bgmSource.clip == newClip) return;

        StopAllCoroutines();
        StartCoroutine(FadeBGM(newClip));
    }

    public void PlayBGMByIndex(int index)
    {
        if (bgmClips == null || bgmClips.Length == 0) return;

        index = Mathf.Clamp(index, 0, bgmClips.Length - 1);
        currentBGMIndex = index;
        PlayBGM(bgmClips[index]);
    }

    public void PlayNextBGM()
    {
        if (bgmClips == null || bgmClips.Length == 0) return;

        currentBGMIndex = (currentBGMIndex + 1) % bgmClips.Length;
        PlayBGMByIndex(currentBGMIndex);
    }


    public void PlayPreviousBGM()
    {
        if (bgmClips == null || bgmClips.Length == 0) return;

        currentBGMIndex = (currentBGMIndex - 1 + bgmClips.Length) % bgmClips.Length;
        PlayBGMByIndex(currentBGMIndex);
    }
    IEnumerator FadeBGM(AudioClip newClip)
    {
        float startVolume = bgmSource.volume;

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        bgmSource.clip = newClip;
        bgmSource.Play();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0, bgmVolume, t / fadeDuration);
            yield return null;
        }

        bgmSource.volume = bgmVolume;
    }

    public void SetVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = volume;
    }

    public void Mute(bool isMute)
    {
        if (bgmSource != null)
            bgmSource.mute = isMute;
    }

    public bool IsMuted()
    {
        return bgmSource != null && bgmSource.mute;
    }
}
