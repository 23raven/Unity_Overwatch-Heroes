using UnityEngine;

/// <summary>
/// Handles background music playback.
/// </summary>
[RequireComponent(typeof(AudioPlayer))]
public class BackgroundMusic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Audio")]
    [SerializeField] private AudioClip musicClip;

    private void Reset()
    {
        audioPlayer = GetComponent<AudioPlayer>();
    }

    private void Awake()
    {
        if (audioPlayer == null)
            audioPlayer = GetComponent<AudioPlayer>();
    }

    private void Start()
    {
        audioPlayer.PlayLoop(musicClip);
    }
}