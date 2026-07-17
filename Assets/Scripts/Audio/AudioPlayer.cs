using UnityEngine;

/// <summary>
/// Universal audio player used by gameplay systems.
/// Wraps AudioSource and provides simple methods
/// for playing single clips, random clips and loops.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource audioSource;

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays a clip once.
    /// </summary>
    public void Play(AudioClip clip)
    {
        if (clip == null)
            return;

        audioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays a random clip from the array.
    /// </summary>
    public void PlayRandom(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
            return;

        int index = Random.Range(0, clips.Length);

        Play(clips[index]);
    }

    /// <summary>
    /// Starts looping a clip.
    /// </summary>
    public void PlayLoop(AudioClip clip)
    {
        if (clip == null)
            return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// Stops current playback.
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Changes playback volume.
    /// </summary>
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// Returns true if AudioSource is currently playing.
    /// </summary>
    public bool IsPlaying => audioSource.isPlaying;
}