using UnityEngine;

/// <summary>
/// Handles weapon firing sounds.
/// </summary>
public class WeaponAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Audio")]
    [SerializeField] private AudioClip[] fireClips;

    /// <summary>
    /// Plays a random weapon fire sound.
    /// </summary>
    public void PlayFire()
    {
        audioPlayer.PlayRandom(fireClips);
    }
}