using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // AudioSource object
    private AudioSource[] ambientNoises;
    
    // Timer for playing bubble noise in swimming minigame and time to wait
    private float timerBubble;
    private float randTimeWaitBubble;
    
    // Start is called before the first frame update
    void Start()
    {
        ambientNoises = GetComponents<AudioSource>();
        timerBubble = 0.0f;

        for (int i = 2; i < 6; i++)
        {
            ambientNoises[i].Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {

        // If current minigame is Swimming, stop playing ambience and wind noises
        if (CameraMove.Instance.GetCurrentSceneName() == "Swimming")
        {
            
            // Stop all other noises
            for(int i = 0; i < 2; i++)
            {
                ambientNoises[i].Stop();
            }
            
            // Check timer, and if amount of time passes, play bubble noise
            if (timerBubble == 0.0f)
            {
                randTimeWaitBubble = Random.Range(5.0f, 10.0f);
            }

            timerBubble += Time.deltaTime;

            if (timerBubble >= randTimeWaitBubble)
            {
                ambientNoises[2].Play();
                timerBubble = 0.0f;
            }
        }
        else if (CameraMove.Instance.GetCurrentSceneName() == "Birdwatching")
        {
            if (GlobalValueTracker.Instance.goldfinchCount == 1)
            {
                ambientNoises[3].Play();
            } else if (GlobalValueTracker.Instance.goldfinchCount == 0)
            {
                ambientNoises[3].Stop();
            }
            
            if (GlobalValueTracker.Instance.sparrowCount == 1)
            {
                ambientNoises[4].Play();
            } else if (GlobalValueTracker.Instance.sparrowCount == 0)
            {
                ambientNoises[4].Stop();
            }
        }
        
        else
        {
            for(int i = 0; i < 2; i++)
            {
                if (!ambientNoises[i].isPlaying)
                {
                    ambientNoises[i].Play();    
                }
            }

            for (int i = 2; i < 5; i++)
            {
                ambientNoises[i].Stop();   
            }
        }
        
        
    }
    
    // Play sound effect
    public void playSoundEffect(AudioClip sfx)
    {
        ambientNoises[5].clip = sfx;
        ambientNoises[5].Play();
    }
    
    // Stop sound effect
    public void stopSoundEffect(AudioClip sfx)
    {
        ambientNoises[5].clip = sfx;
        ambientNoises[5].Play();
    }
}
