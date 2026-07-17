using UnityEngine;

/// <summary>
/// Handles hit confirmation sounds.
/// </summary>
public class HitAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Audio")]
    [SerializeField] private AudioClip[] hitClips;

    /// <summary>
    /// Plays a random hit sound.
    /// </summary>
    public void PlayHit()
    {
        audioPlayer.PlayRandom(hitClips);
    }
}