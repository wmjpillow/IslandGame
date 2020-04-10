using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun_controller : MonoBehaviour
{
    public float DayLength;
    private float _rotationSpeed;
    private float cumulativeTime = 0;
    private TextDisplay textDisplay;
    private float tod;
    
    // Audio for each day
    private MusicManager musicMan;
	public AudioClip mainHubMusic2;
	public AudioClip mainHubMusic3;
	public AudioClip mainHubMusic4;

    void Start()
    {
        tod = 50;
        musicMan = FindObjectOfType<MusicManager>();
    }

    void Update()
    {
        _rotationSpeed = Time.deltaTime / DayLength;
        transform.Rotate(_rotationSpeed, 0, 0);
        cumulativeTime += _rotationSpeed;

        tod = cumulativeTime;
        GlobalValueTracker.Instance.tod = tod;

        if (cumulativeTime > 180)
        {
            nextDay();
        }
    }

    public void nextDay(){
                    musicMan.stopMusic();
            Debug.Log("new day");
            //Increment the day by 1.
            IngameDate.GetInstance().incrementDay();
            cumulativeTime = 0;
            //Should also enable those things
            UIManager.Instance.FadeIn("BlackScreenOfDeath");
            Vector3 elg = transform.eulerAngles;
            elg.x = 0;
            elg.y = -30;
            elg.z = 0;
            transform.rotation = Quaternion.Euler(elg);

            textDisplay = UIManager.Instance.gameObject.GetComponent<TextDisplay>();
            
			// Change music depending on day
			if(IngameDate.GetInstance().getCurrentDay() == 1){
				StartCoroutine(playMusicAfterBlack(mainHubMusic2));
			} else if(IngameDate.GetInstance().getCurrentDay() == 2){
				StartCoroutine(playMusicAfterBlack(mainHubMusic3));
			} else if(IngameDate.GetInstance().getCurrentDay() == 3) {
				StartCoroutine(playMusicAfterBlack(mainHubMusic4));
			}

            //Kick the player back to main screen
            if(GlobalValueTracker.Instance.day == 4){
                textDisplay.DisplayText("Congratulations! There's nothing left to do! You win automatically!");
            }

            Invoke ("RemoveBlackScreen", 3);
    }
    

    void RemoveBlackScreen(){
        CameraMove.Instance.TransformToOriginal();
        UIManager.Instance.FadeOut("BlackScreenOfDeath");
        textDisplay.HideText();
    }
    
    // Separate function for playing audio after black screen
    IEnumerator playMusicAfterBlack(AudioClip newMusic)
    {
        yield return new WaitForSeconds(3);
        musicMan.changeMusic(newMusic);
    }
}
