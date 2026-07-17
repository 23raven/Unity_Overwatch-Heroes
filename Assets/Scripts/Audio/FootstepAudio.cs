using UnityEngine;

/// <summary>
/// Plays footstep sounds while the player is moving.
/// </summary>
public class FootstepAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Audio")]
    [SerializeField] private AudioClip[] footstepClips;

    [Header("Settings")]
    [SerializeField] private float stepInterval = 0.45f;

    private float timer;

    private void Update()
    {
        if (!ShouldPlayFootsteps())
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= stepInterval)
        {
            timer = 0f;
            audioPlayer.PlayRandom(footstepClips);
        }
    }

    /// <summary>
    /// Determines whether footsteps should currently be played.
    /// </summary>
    private bool ShouldPlayFootsteps()
    {
        return IsMoving() && IsGrounded();
    }

    /// <summary>
    /// Replace this with your own movement logic.
    /// </summary>
    private bool IsMoving()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        return input.sqrMagnitude > 0.01f;
    }

    /// <summary>
    /// Replace this with your CharacterController if needed.
    /// </summary>
    private bool IsGrounded()
    {
        return true;
    }
}