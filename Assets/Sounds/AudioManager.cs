using UnityEngine.Audio;
using System;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioMixer masterAudioMixer;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;

    public Sound[] sounds;

    //[SerializeField] private bool isMusicMuted = false;
    //private float unmutedMusicVolume;
    private const float mutedVolume = -80f;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        //masterAudioMixer.GetFloat("musicVolume", out float musicVolume);
        //Debug.Log("MUSIC VOLUME IS " + musicVolume);
    }

    public void PlayMusic(string sound) {
        Sound msc = Array.Find(sounds, item => item.soundType == Sound.SOUND_TYPE.MUSIC && item.name == sound);

        if (msc == null) {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        Sound otherPlaying = Array.Find(sounds, item => item.soundType == Sound.SOUND_TYPE.MUSIC && item.source.isPlaying);

        Sequence seq = DOTween.Sequence();
        if (otherPlaying != null) {
            seq.Append(otherPlaying.source.DOFade(0f, 2f).OnComplete(() => otherPlaying.source.Stop()));
        }
        seq.Join(msc.source.DOFade(1f, 2f).From(0).OnStart(() => msc.source.Play()));
    }

    public void PlaySFX(string sound) {
        Sound s = Array.Find(sounds, item => item.name == sound && item.soundType == Sound.SOUND_TYPE.SFX);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public bool IsMusicMuted() {
        masterAudioMixer.GetFloat("musicVolume", out float musicVolume);
        return musicVolume == mutedVolume;
    }

    public bool IsSFXMuted() {
        masterAudioMixer.GetFloat("sfxVolume", out float sfxVolume);
        return sfxVolume == mutedVolume;
    }

    public void ToggleMusicMute() {
        if (IsMusicMuted()) masterAudioMixer.ClearFloat("musicVolume");
        else masterAudioMixer.SetFloat("musicVolume", mutedVolume);
    }

    public void ToggleSFXMute() {
        if (IsSFXMuted()) masterAudioMixer.ClearFloat("sfxVolume");
        else masterAudioMixer.SetFloat("sfxVolume", mutedVolume);
    }

    private void Update() {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B)) {
            PlayMusic("BG_1");
        } else if (Input.GetKeyDown(KeyCode.N)) {
            PlayMusic("BG_2");
        }
#endif
    }
}
