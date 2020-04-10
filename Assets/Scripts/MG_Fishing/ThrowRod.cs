using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowRod : MonoBehaviour
{
    public GameObject cube;
    public GameObject ball;
    public GameObject fish_caught_panel;
    public GameObject fish;
    public bool isThrown = false;
    private bool isFishCome = false;
    private bool isStart = false;
    private float time;
    private bool isCaught = false;
    public static int fishScore = 0;
    public static int fishTotal = 0;

    private float min_time;
    private float max_time;
    
    // Audio clips for fishing
    private SFXManager sfxMan;
    public AudioClip rodCast;
    public AudioClip fishWaterOut;
    public AudioClip noFishWaterOut;
    public AudioClip plop;
    public AudioClip waterShake;

    private static float waitForPlop = 0.5f;
    private float timer;
    private bool startTimer = false;

    public GameObject notebook;

    float timeLeft;
    
    // Reference to Text UI
    private ObjectiveTextUI objectiveTextUI;
    private static int lastDayUpdated;

    void Start()
    {
        //set the min & max time of the random range
        min_time = 3.0f;
        max_time = 7.0f;
        sfxMan = FindObjectOfType<SFXManager>();
        timer = 0.0f;
        timeLeft = 15;
        
        // Initialize text
        objectiveTextUI = FindObjectOfType<ObjectiveTextUI>();
        
        // Set total to global tracker value goal
        fishTotal = GlobalValueTracker.Instance.fishing_goal;
    }

    void Update()
    {
        if (GlobalValueTracker.Instance.day == 4)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GlobalValueTracker.Instance.g2 = true;
            }
        }

        min_time = GlobalValueTracker.Instance.min_time;
        max_time = GlobalValueTracker.Instance.max_time;
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool tem = screenPoint.x < 1f && screenPoint.x > 0.5f && screenPoint.y < 0f && screenPoint.y > -1.5f && screenPoint.z < 2.5f && screenPoint.z > 2f;

        //if get into fishing scene
        if (tem)
        {
            isStart = true;
        }

        //if leave the fishing scene
        bool onScreen = screenPoint.z < 5f;
        if (!onScreen&&isStart)
        {
            //get the rod back if it is thrown
            if (isThrown)
            {
                isThrown = !isThrown;
                cube.GetComponent<ThrowCube>().ChangeCube(isThrown);
                ball.GetComponent<ThrowBall>().ChangeBall(isThrown);
            }
            Debug.Log("disappear");

            //hide the fishing things
            transform.gameObject.SetActive(false);
            fish_caught_panel.SetActive(false);
            isStart = false;
            fish.SetActive(false);
        }

        //if not hold a fish and click
        if (Input.GetMouseButtonDown(0)&&!isCaught)
        {
            
            //catch a fish if fish come
            if (isFishCome)
            {
                sfxMan.playSoundEffect(fishWaterOut);
                isFishCome = false;
                fish_caught_panel.SetActive(true);
                isCaught = true;

                //show the fish
                fish.SetActive(true);

                //add one fish
                fishScore++;
                GlobalValueTracker.Instance.fishCaught = fishScore;

                //notebook.GetComponent<NotebookScript>().SetFishingNum(fishScore, -1);

                //ball stop shaking
                ball.GetComponent<ThrowBall>().StopShakingBall();
            }
            else
            {
                sfxMan.playSoundEffect(noFishWaterOut);
            }
            isThrown = !isThrown;

            //sinker and stick get back
            cube.GetComponent<ThrowCube>().ChangeCube(isThrown);
            ball.GetComponent<ThrowBall>().ChangeBall(isThrown);

            //if is throwing the rod
            if (isThrown)
            {
                sfxMan.playSoundEffect(rodCast);
                if (!startTimer)
                {
                    startTimer = true;
                }

                if (startTimer)
                {
                    timer += Time.deltaTime;
                    if (timer >= waitForPlop)
                    {
                        sfxMan.playSoundEffect(plop);
                        startTimer = false;
                    }
                }

                //get a random time of waiting
                time = Random.Range(min_time, max_time);

                //sinker shake after the random time
                Invoke("BallShake", time);
            }
        }
        
        // Always update score every frame
		if(IngameDate.GetInstance().getCurrentDay() < 3) {
        	if(fishScore >= fishTotal) {
            	switch(IngameDate.GetInstance().getCurrentDay()){
					case 0:
						objectiveTextUI.updateObjectiveText("Congratulations, you’re a Weekend Fisherman! You win!");
						break;
					case 1:
						objectiveTextUI.updateObjectiveText("Congratulations, you’re a Fly Fisherman! You win!");
						break;
					case 2:
						objectiveTextUI.updateObjectiveText("Congratulations, you’re a Reel Fisherman! You win!");
						break;
				}
        	} 
        	else {
            	objectiveTextUI.updateObjectiveText("Catch some fish!");
        	}
		}
        
        // Update variables if day changes
        if (lastDayUpdated != IngameDate.GetInstance().getCurrentDay())
        {
            fishScore = 0;
            GlobalValueTracker.Instance.fishCaught = 0;
        }

        lastDayUpdated = IngameDate.GetInstance().getCurrentDay();
    }

    //exit the pop up
    public void DisCaughtFishPanel()
    {
        fish_caught_panel.SetActive(false);
        isCaught = false;
        fish.SetActive(false);
    }

    //sinker shake
    private void BallShake()
    {
        //track the state
        if (isThrown)
        {
            Invoke("FishEscape", 2.0f);
            Debug.Log("fish come");
            isFishCome = true;
            ball.GetComponent<ThrowBall>().ShakeBall();
            sfxMan.playSoundEffect(waterShake);
        }
    }
    private void FishEscape()
    {
        if (isThrown&&isFishCome)
        {
            Debug.Log("fish escape");
            isFishCome = false;
        }
    }

    public void clearScore()
    {
        fishScore = 0;
    }
}
