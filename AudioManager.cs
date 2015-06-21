using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO(vlad): use dataIo for saving sound states?

/*
 * General class for playing sounds
 * Too many loops for now :(
 */ 
public class AudioManager : MonoBehaviour
{

    #region Public Variables
    [System.Serializable]
    public class SoundItem
    {
        public string clipName;
        public AudioClip clip;
    }

    public int channels = 1;
    public SoundItem[] soundClips;

    #endregion

    #region Private Variables
    private const string SOUND_KEY          = "sound_key";
    private const string MASTER_VOLUME_KEY  = "masterVolumeKey";

    private AudioSource[] sources;

    private static AudioManager instance;
    #endregion

    #region Unity Methods

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            Debug.LogError("Another instance of Audiomanager is destroyed");
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        //=======================================

        initSound();

        initChannels();
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            setSound();
        }
    }

    #endregion

    #region Private methods

    private void initSound()
    {
        if (!PlayerPrefs.HasKey(SOUND_KEY))
        {
            PlayerPrefs.SetInt(SOUND_KEY, 1);
        }

        if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
        {
            setVolume(1.0f);
        }

        setSound();
        setVolume(getVolume());
    }

    private void initChannels()
    {
        sources = new AudioSource[channels];

        for (int i = 0; i < channels; ++i)
        {
            GameObject channelObject = new GameObject("Channel");
            AudioSource channelSource = channelObject.AddComponent<AudioSource>();
            sources[i] = channelSource;
            channelObject.transform.SetParent(transform);
        }

        Debug.Log("Audio manager created (" + channels + " channels)");
    }

    private void setSound()
    {
        if (!isSoundOn())
        {
            muteOff();
        }
    }

    private void playSound(AudioClip clipToPlay, bool isLooped = false)
    {
        //find free channel and play sound
        foreach (AudioSource source in sources)
        {
            if (!source.isPlaying)
            {
                source.clip = clipToPlay;
                source.loop = isLooped;
                source.Play();
                break;
            }
        }
    }

    private void stopSound(AudioClip clipToStop)
    {
        foreach (AudioSource source in sources)
        {
            if (source.clip == clipToStop && source.isPlaying)
            {
                source.Stop();
                break;
            }
        }
    }

    #endregion

    #region Interface

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void playSoundByKey(string key, bool isLooped = false)
    {
        foreach (SoundItem item in soundClips)
        {
            if (item.clipName == key)
            {
                playSound(item.clip, isLooped);
                return;
            }
        }

        Debug.LogWarning("AudioManager play(): Sound " + key + " not found");
    }

    public void stopSoundByKey(string key)
    {
        foreach (SoundItem item in soundClips)
        {
            if (item.clipName == key)
            {
                stopSound(item.clip);
                break;
            }
        }

        Debug.LogError("AudioManager stop(): Sound " + key + " not found");
    }

    public bool isSoundPlaying(string key)
    {
        AudioClip clip = null;

        //find clip by key
        foreach (SoundItem item in soundClips)
        {
            if (item.clipName == key)
            {
                clip = item.clip;
                break;
            }
        }

        if (clip == null)
        {
            Debug.LogError("AudioManager stop(): Sound " + key + " not found");
            return false;
        }

        //check if one of channels is playing the clip
        foreach (AudioSource source in sources)
        {
            if (source.clip == clip && source.isPlaying)
            {
                return true;
            }
        }

        return false;
    }

    //=========== UTILS ====================
    public void muteOff()
    {
        PlayerPrefs.SetInt(SOUND_KEY, 0);
        AudioListener.pause = true;
    }

    public void muteOn()
    {
        PlayerPrefs.SetInt(SOUND_KEY, 1);
        AudioListener.pause = false;
    }

    public bool isSoundOn()
    {
        return PlayerPrefs.GetInt(SOUND_KEY) == 1;
    }

    public void setVolume(float value) 
    {
        if (value > 1.0f)
        {
            value = 1.0f;
        }

        if (value < 0)
        {
            value = 0;
        }

        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, value);
        AudioListener.volume = value;
    }

    public float getVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }


    #endregion
}
