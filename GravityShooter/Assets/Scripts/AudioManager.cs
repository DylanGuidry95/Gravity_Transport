using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private IEnumerator CreateAndPlayAudio(AudioClip a_clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = a_clip;
        source.Play();

        while(source.isPlaying)
        {
            yield return null;
        }

        Destroy(source);
    }

    public void PlayLaserAudio()
    {
        if(Laser)
            StartCoroutine(CreateAndPlayAudio(Laser));
    }

    public void PlayExplodeAudio()
    {
        if (Explode)
            StartCoroutine(CreateAndPlayAudio(Explode));
    }

    public AudioClip Laser;
    public AudioClip Explode;
}
