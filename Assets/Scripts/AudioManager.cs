using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private const string VolumeKey = "Volume";

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }

        // Load the volume from PlayerPrefs and set it
        float volume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        ChangeVolume(volume);

        PlaySound("MainTheme");
    }

    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
                Debug.Log("Playing sound: " + name);
            }
        }
    }

    public void ChangeVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }

        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}
