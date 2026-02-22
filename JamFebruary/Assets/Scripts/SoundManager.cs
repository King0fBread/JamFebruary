using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _masterMixer;
    private AudioClip _clip;
    private AudioSource _source;
    public AudioClipAndSourcePairedToSound[] audioClipsAndSourcePairedToSounds;
    [System.Serializable]
    public class AudioClipAndSourcePairedToSound
    {
        public Sounds sound;
        public AudioClip clip;
        public AudioSource source;
    }
    private static SoundManager _instance;
    public static SoundManager instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public enum Sounds
    {
        PlayerWalking,
        PlayerPickupItem,
        PlayerPlaceItem,
        PlayerKnocking,

        PlayerFailed,
        PlayerSucceeded,

        PlayerDialogueReply,

        EnvironmentDoorOpening,
        EnvironmentDoorClosing,
        EnvironmentCar,

        BackgroundStreet,
        BackgroundFloor01,
        BackgroundFloorCat,
        BackgroundFloorIce,
        BackgroundFloorVoid,
    }
    public void PlaySound(Sounds sound, bool shouldLoop = false)
    {
        AudioClip _clip = null;
        AudioSource _source = null;

        GetRequestedAudioClipAndAudioSource(sound, out _clip, out _source);

        if (!_source.isPlaying)
        {
            _source.PlayOneShot(_clip);
        }
        else
        {
            if (!shouldLoop)
            {
                _source.Stop();
                _source.PlayOneShot(_clip);
            }
            else
            {
                _source.loop = true;
                _source.Play();
            }
        }
    }
    public IEnumerator PlaySoundAfterDelay(Sounds sound, float delayInSeconds, bool shouldLoop = false)
    {
        yield return new WaitForSeconds(delayInSeconds);

        PlaySound(sound, shouldLoop);
    }
    public void StopSound(Sounds sound)
    {
        GetRequestedAudioClipAndAudioSource(sound, out _clip, out _source);

        if (_clip == null || _source == null)
        {
            print("Clip " + _clip + ", Source " + _source);
            return;
        }

        if (_source.isPlaying)
        {
            _source.Stop();
        }
    }
    private void GetRequestedAudioClipAndAudioSource(Sounds soundToFind, out AudioClip clip, out AudioSource source)
    {
        clip = null;
        source = null;

        foreach (AudioClipAndSourcePairedToSound audioClipAndSourcePairedToSound in audioClipsAndSourcePairedToSounds)
        {
            if (audioClipAndSourcePairedToSound.sound == soundToFind)
            {
                clip = audioClipAndSourcePairedToSound.clip;
                source = audioClipAndSourcePairedToSound.source;
            }
        }
    }
    public void SilenceAllSounds()
    {
        _masterMixer.SetFloat("MasterVolume", -60f);
    }
}