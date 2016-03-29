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

    public void PlayRocketAudio()
    {
        if (Rocket)
            StartCoroutine(CreateAndPlayAudio(Rocket));
    }

    public void PlayExplodeAudio()
    {
        int rand = Random.Range(1, 10);
        if (Explode && rand < 7)
            StartCoroutine(CreateAndPlayAudio(Explode));
        else if(EXPLOSIONS && rand > 7)
            StartCoroutine(CreateAndPlayAudio(EXPLOSIONS));
    }

    public AudioClip Laser;
    public AudioClip Rocket;
    public AudioClip Explode;
    public AudioClip EXPLOSIONS;
}
