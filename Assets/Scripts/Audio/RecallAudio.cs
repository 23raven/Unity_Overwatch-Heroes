using UnityEngine;

/// <summary>
/// Handles Recall ability sounds.
/// </summary>
public class RecallAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Audio")]
    [SerializeField] private AudioClip recallStartClip;
    [SerializeField] private AudioClip recallEndClip;

    /// <summary>
    /// Plays the Recall start sound.
    /// </summary>
    public void PlayStart()
    {
        audioPlayer.Play(recallStartClip);
    }

    /// <summary>
    /// Plays the Recall end sound.
    /// </summary>
    public void PlayEnd()
    {
        audioPlayer.Play(recallEndClip);
    }
}