/// ERIC MOULEDOUX
using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Coroutine for creating, playing and removing audio clips from the game
    /// </summary>
    /// <param name="a_clip">Clip to play</param>
    /// <returns></returns>
    private IEnumerator CreateAndPlayAudio(AudioClip a_clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();    // Creates an audio component
        source.clip = a_clip;                                           // Adds the clip to it
        source.Play();                                                  // Plays the clip

        while(source.isPlaying) // While the clip is playing
        {                       
            yield return null;  // Kepp playing it
        }// Once the clip is done playing

        Destroy(source); // Destroy it
    }

    /// <summary>
    /// Plays the "laser" audio clip
    /// </summary>
    public void PlayLaserAudio()
    {
        if(Laser)   // Check for a clip
            StartCoroutine(CreateAndPlayAudio(Laser));
    }

    /// <summary>
    /// Plays one of the possible "explosion" sounds
    /// </summary>
    public void PlayExplodeAudio()
    {
        int rand = Random.Range(1, 10); // Random number between 1 and 10
        if (Explode && rand < 7)            
            StartCoroutine(CreateAndPlayAudio(Explode));    // Normal explosion
        else if(EXPLOSIONS && rand > 7)
            StartCoroutine(CreateAndPlayAudio(EXPLOSIONS)); // EXPLOSIONS!!!
    }

    /// <summary>
    /// Laser sound
    /// </summary>
    public AudioClip Laser;
    /// <summary>
    /// Explosion sound
    /// </summary>
    public AudioClip Explode;
    /// <summary>
    /// EXPLOSIONS!!!
    /// </summary>
    public AudioClip EXPLOSIONS;
}
