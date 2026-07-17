using UnityEngine;

/// <summary>
/// Handles Blink ability sounds.
/// </summary>
public class BlinkAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Audio")]
    [SerializeField] private AudioClip blinkClip;

    /// <summary>
    /// Plays the Blink sound.
    /// </summary>
    public void PlayBlink()
    {
        audioPlayer.Play(blinkClip);
    }
}