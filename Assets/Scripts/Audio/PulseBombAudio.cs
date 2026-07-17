using UnityEngine;

/// <summary>
/// Handles Pulse Bomb sounds.
/// </summary>
public class PulseBombAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Audio")]
    [SerializeField] private AudioClip throwClip;
    [SerializeField] private AudioClip explosionClip;

    /// <summary>
    /// Plays the throw sound.
    /// </summary>
    public void PlayThrow()
    {
        audioPlayer.Play(throwClip);
    }

    /// <summary>
    /// Plays the explosion sound.
    /// </summary>
    public void PlayExplosion()
    {
        audioPlayer.Play(explosionClip);
    }
}