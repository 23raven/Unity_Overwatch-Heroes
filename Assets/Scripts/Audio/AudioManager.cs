using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource blink;
    [SerializeField] public AudioSource shoot;
    [SerializeField] private AudioSource recall;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource voice;
    [SerializeField] private AudioSource music;

    public void PlayBlink() => blink.Play();

    public void StopBlink() => blink.Stop();

    public void PlayRecall() => recall.Play();

    public void StopRecall() => recall.Stop();

    private Coroutine shootFadeCoroutine;


    public void PlayShoot()
    {
        if (shootFadeCoroutine != null)
        {
            StopCoroutine(shootFadeCoroutine);
            shootFadeCoroutine = null;
        }

        shoot.volume = 0.2f;

        if (!shoot.isPlaying)
            shoot.Play();
    }

    public void StopShoot()
    {
        if (!shoot.isPlaying || shootFadeCoroutine != null)
            return;

        shootFadeCoroutine = StartCoroutine(FadeOutShoot());
    }

    private IEnumerator FadeOutShoot()
    {
        float duration = 0.2f;
        float startVolume = shoot.volume;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            shoot.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        shoot.Stop();
        shoot.volume = 0.2f;
        shootFadeCoroutine = null;
    }
}
