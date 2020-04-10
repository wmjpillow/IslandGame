using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butterfly_move : MonoBehaviour
{
    public float movement_speed, x, y, z, timer, direction;
    public int direction_length, frames_passed;
    public static int count;
    public static int score;
    public static int total;
    
    // Audio for catching butterflies
    private SFXManager sfxMan;
    public AudioClip catchButterfly;
    
    // Reference to Text UI
    private ObjectiveTextUI objectiveTextUI;
    private static int lastDayUpdated;

    // Start is called before the first frame update
    void Start()
    {
        movement_speed = .25f * Time.deltaTime; //set the movement speed of butterflys
        x = 0; //initialize the speeds of the butterflys in all directions to 0
        y = 0;
        z = 0;
        int directionLength = Random.Range(1, 4); //determine length of flight in determined direction
        timer = 0.0f;
        determineDirection();
        this.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
        sfxMan = FindObjectOfType<SFXManager>();
        
        // Initialize text
        objectiveTextUI = FindObjectOfType<ObjectiveTextUI>();
        
        // Set total to global tracker value goal
        total = GlobalValueTracker.Instance.butterfly_goal;
    }

    // Update is called once per frame
    void Update()
    {

        if(GlobalValueTracker.Instance.day == 4)
        {
            float timeLeft = 30;
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GlobalValueTracker.Instance.g1 = true;
            }
        }

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > -0.1 && screenPoint.x < 1.1 && screenPoint.y > -0.2 && screenPoint.y < 1.2;

        if (!CameraMove.Instance.GetCurrentSceneName().Equals("Butterfly"))
        {
            Destroy(this.gameObject);
        }

        timer += Time.deltaTime; //get how many seconds have passed
        int seconds = (int)timer % 60;

        if (direction_length > seconds)  //should it still be moving in defined direction?
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); //camera bounds
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);



            if (onScreen) //if in camera view
            {
                transform.Translate(x, y, z); //move
            }
            else //else
            {
                transform.Translate(x, y, z); //Completely move object off screen
                removeInstance();
            }
            frames_passed++;
        }
        else //should switch direction
        {
            determineDirection();
            direction_length = Random.Range(1, 4); //determine length of flight in said direction
            timer = 0;
        }
        
        // Update text
        if (IngameDate.GetInstance().getCurrentDay() < 3)
        {
            if (score >= total)
            {
                switch(IngameDate.GetInstance().getCurrentDay()){
                    case 0:
                        objectiveTextUI.updateObjectiveText("Congratulations, you’re a Star Butterfly Catcher! You win!");
                        break;
                    case 1:
                        objectiveTextUI.updateObjectiveText("Congratulations, you’re a Champion Butterfly Catcher! You win!");
                        break;
                    case 2:
                        objectiveTextUI.updateObjectiveText("Congratulations, you’re an Ace Butterfly Catcher! You win!");
                        break;
                }
            }
            else
            {
                objectiveTextUI.updateObjectiveText("Catch some butterflies!");
            }
        }

        // Update variables if day changes
        if (lastDayUpdated != IngameDate.GetInstance().getCurrentDay())
        {
            score = 0;
            GlobalValueTracker.Instance.butterflyCaught = 0;
        }

        lastDayUpdated = IngameDate.GetInstance().getCurrentDay();
    }

    public void determineDirection()
    {
        float random_speed = Random.Range(0.25f, 0.75f); //set a random speed
        movement_speed = random_speed * Time.deltaTime;

        direction = Random.Range(1, 4);
        float diagonal_speed = Random.Range(0.0f, 0.25f); //set movement in the other direction (diagonal?)
        int ldur = Random.Range(0, 1); //determine if diagonal is left right up or down
        if (ldur == 0)
        {
            ldur = -1;
        }
        diagonal_speed = diagonal_speed * Time.deltaTime * ldur;

        switch (direction)
        {
            case 1: //direction right
                x = movement_speed;
                y = 0;
                z = diagonal_speed;
                break;
            case 2: //direction left
                x = -movement_speed;
                y = 0;
                z = diagonal_speed;
                break;
            case 3: //direction down
                x = diagonal_speed;
                y = 0;
                z = movement_speed;
                break;
            default: //direction up
                x = diagonal_speed;
                y = 0;
                z = -movement_speed;
                break;
        }
    }

    public void spawnInstance()
    {
        direction_length = Random.Range(1, 5); //determine length of flight in said direction
        timer = 0;
        Vector3 v3Pos = new Vector3(0f, 0f, 0f);
        int spawnLoc = Random.Range(1, 5);
        switch (spawnLoc)
        {
            case 1: //along left wall
                v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(-0.05f, Random.Range(0.01f, 0.99f), 2f));
                break;
            case 2: //along top wall
                v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.01f, 0.99f), 1.09f, 2f));
                break;
            case 3: //along right wall
                v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(1.05f, Random.Range(0.01f, 0.99f), 2f));
                break;
            case 4: //along bottom wall
                v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.01f, 0.99f), -0.09f, 2f));
                break;
        }

        GameObject child = Instantiate(this.gameObject, v3Pos, Quaternion.identity);
    }

    public void removeInstance()
    {
        spawnInstance();
        if (count < GlobalValueTracker.Instance.max_butterfly)
        {
            spawnInstance();
            count++;
        }
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.name == "Net-head")
        {
            sfxMan.playSoundEffect(catchButterfly);
            score++;
            GlobalValueTracker.Instance.butterflyCaught = score;
            print(score);
            removeInstance();
        }
    }
}
