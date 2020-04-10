using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_move_right : MonoBehaviour
{
    public float movement_speed, x, y, z, timer, direction;
    public int direction_length, frames_passed;
    public static int count;
    public static int score;
    public static int total;
    public GameObject FinchLeft;
    public GameObject FinchRight;
    public GameObject SparrowLeft;
    public GameObject sparrowRight;
    public float random_speed;
    private bool camCaptured;
    private int type;

    // Reference to Text UI
    private ObjectiveTextUI objectiveTextUI;
    private static int lastDayUpdated;

    // Start is called before the first frame update
    void Start()
    {
        random_speed = Random.Range(.03f, 0.08f);
        x = 0; //initialize the speeds of the butterflys in all directions to 0
        y = 0;
        z = 0;
        int directionLength = 50; //determine length of flight in determined direction
        timer = 0.0f;
        this.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        type = Random.Range(1, 3);
        camCaptured = false;

        // Initialize text
        objectiveTextUI = FindObjectOfType<ObjectiveTextUI>();

        // Set total to global tracker value goal
        total = GlobalValueTracker.Instance.birds_goal;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalValueTracker.Instance.day == 4)
        {
            float timeLeft = 30;
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GlobalValueTracker.Instance.g1 = true;
            }
        }

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > -1 && screenPoint.x < 1 && screenPoint.y > -0.2 && screenPoint.y < 1.2;

        if(Input.GetMouseButtonDown(0) && onScreen && !camCaptured) {
            GlobalValueTracker.Instance.birdsFound += 1;
            camCaptured = true;
        }

        if (!CameraMove.Instance.GetCurrentSceneName().Equals("Birdwatching"))
        {
            Destroy(this.gameObject);
            GlobalValueTracker.Instance.bird_count = 0;
            GlobalValueTracker.Instance.goldfinchCount = 0;
            GlobalValueTracker.Instance.sparrowCount = 0;
            GlobalValueTracker.Instance.birdsFound = 0;
        }

        if (onScreen) //if in camera view
        {
            transform.Translate(0, 0, -random_speed); //move
        }
        else //else
        {
            removeInstance();
        }
        
        // if(IngameDate.GetInstance().getCurrentDay() < 3) {
        //     if(GlobalValueTracker.Instance.birdsFound >= total) {
        //         switch(IngameDate.GetInstance().getCurrentDay()){
        //             case 0:
        //                 objectiveTextUI.updateObjectiveText("Congratulations, you’re a Hatchling! You win!");
        //                 break;
        //             case 1:
        //                 objectiveTextUI.updateObjectiveText("Congratulations, you’re a Growing Nestling! You win!");
        //                 break;
        //             case 2:
        //                 objectiveTextUI.updateObjectiveText("Congratulations, you’re a Flying Fledgling! You win!");
        //                 break;
        //         }
        //     } 
        //     else 
        //     {
        //         // objectiveTextUI.updateObjectiveText("Count number of different birds!");
                
        //     }
        // }
        // objectiveTextUI.updateObjectiveText(GlobalValueTracker.Instance.birdsFound.ToString());
         objectiveTextUI.updateObjectiveText("Bird Watching Game is coming soon!");

        // Update variables if day changes
        if (lastDayUpdated != IngameDate.GetInstance().getCurrentDay())
        {
            GlobalValueTracker.Instance.birdsFound = 0;
            GlobalValueTracker.Instance.bird_count = 0;
            GlobalValueTracker.Instance.goldfinchCount = 0;
            GlobalValueTracker.Instance.sparrowCount = 0;
        }

        lastDayUpdated = IngameDate.GetInstance().getCurrentDay();
    }

    public void spawnInstance()
    {
        direction_length = 50; //determine length of flight in said direction
        timer = 0;
        Vector3 v3Pos = new Vector3(0f, 0f, 0f);
        int side = Random.Range(1, 3);
        int zRange = Random.Range(3, 10);
        if (side == 1) //left
        {
            v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(-300f, Random.Range(0.01f, 0.99f), zRange));
            if (type == 1)
            {
                GlobalValueTracker.Instance.goldfinchCount += 1;
                // GameObject child = Instantiate(FinchLeft, v3Pos, Quaternion.identity);
            }
            else
            {
                GlobalValueTracker.Instance.sparrowCount += 1;
                // GameObject child = Instantiate(SparrowLeft, v3Pos, Quaternion.identity);
            }
        }
        else //right
        {
            v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(1f, Random.Range(0.01f, 0.99f), zRange));
            if (type == 1)
            {
                GlobalValueTracker.Instance.goldfinchCount += 1;
                // GameObject child = Instantiate(FinchRight, v3Pos, Quaternion.identity);
            }
            else
            {
                GlobalValueTracker.Instance.sparrowCount += 1;
                // GameObject child = Instantiate(sparrowRight, v3Pos, Quaternion.identity);
            }
        }
    }

    public void removeInstance()
    {
        if (type == 1)
        {
            GlobalValueTracker.Instance.goldfinchCount -= 1;
        }
        else
        {
            GlobalValueTracker.Instance.sparrowCount -= 1;
        }
        spawnInstance();
        if (GlobalValueTracker.Instance.bird_count < GlobalValueTracker.Instance.max_bird)
        {
            spawnInstance();
            GlobalValueTracker.Instance.bird_count++;
        }
        Destroy(this.gameObject);
    }

}
