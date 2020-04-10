using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // AudioSource object
    public AudioSource gameMusic;
    private bool isFadeOutOld;
    private bool isFadeInNew;

    // Start is called before the first frame update
    void Start()
    {
        isFadeOutOld = false;
        isFadeInNew = false;
    }

    // Update is called once per frame
    void Update()
    {
        // // Fade out old music
        // isFadeOutOld = true;
        //
        // while (isFadeOutOld)
        // {
        //     if (gameMusic.volume <= 0)
        //     {
        //         gameMusic.Stop();
        //         isFadeOutOld = false;
        //         isFadeInNew = true;
        //     }
        //     else
        //     {
        //         gameMusic.volume -= Time.deltaTime;    
        //     }
        // }
        //
        // while (isFadeInNew)
        // {
        //     gameMusic.clip = music;
        //     gameMusic.volume = 0;
        //     gameMusic.Play();
        //
        //     if (gameMusic.volume >= 1)
        //     {
        //         gameMusic.volume = 1;
        //         isFadeInNew = false;
        //     }
        //     else
        //     {
        //         gameMusic.volume += Time.deltaTime;
        //     }


        //}
    }
    
    // Change music with given clip
    public void changeMusic(AudioClip music)
    {
        gameMusic.Stop();
        gameMusic.clip = music;
        gameMusic.Play();
    }
    
    // Stop music
    public void stopMusic()
    {
        gameMusic.Stop();
    }
    
    // Play music
    public void playMusic()
    {
        gameMusic.Play();
    }
}
