using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

	public enum SOUND_TYPE { MUSIC, SFX }

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	public SOUND_TYPE soundType;

	[HideInInspector]
	public AudioSource source;

}
