using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fish_swimming_move : MonoBehaviour
{
    public float movement_speed, x, y, z, timer, direction;
    Camera cameraVar;
    public int frames_passed;
    public static int count = 0;
	public static int total = 0;
	public static int score = 0;
    private bool camCaptured;
    
    // Keep track of which materials to use
    public Material orangeMaterial;
    public Material blueMaterial;

	// Reference to Text UI
	private ObjectiveTextUI objectiveTextUI;
    
    // Notebook parameters
    public GameObject notebook;
    private string fishType;

    private static int lastDayUpdated;

    // Start is called before the first frame update
    void Start()
    {
        movement_speed = 1f * Time.deltaTime; //set the movement speed of fish
        x = 0; //initialize the speeds of the fish in all directions to 0
        y = 0;
        z = 0;
        timer = 0.0f;
        determineDirection();
        camCaptured = false;
        
        //print("Fish count = " + count);
        
        // Kepp 15 fish on screen at all times
        if (count < GlobalValueTracker.Instance.max_fish && CameraMove.Instance.GetCurrentSceneName() == "Swimming")
        {
            //print("I AM HERE");
            spawnInstance();
            count += 1;
        }
        else if (!(CameraMove.Instance.GetCurrentSceneName() == "Swimming"))
        {
            Destroy(this.gameObject);
        }

		// Initialize text
		objectiveTextUI = FindObjectOfType<ObjectiveTextUI>();
        
        // Set total to global tracker value goal
        total = GlobalValueTracker.Instance.swimming_goal;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
		bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        bool inPlay = transform.position.x >= 20.0f && transform.position.x <= 30.0f && 
            transform.position.y >= 3.0f && transform.position.y <= 5.0f;

		// Check on mouse click if object is in view, and if is, add 1 to score
		if(Input.GetMouseButtonDown(0) && onScreen && !camCaptured) {
			score += 1;
            GlobalValueTracker.Instance.fishPhotographed = score;
            camCaptured = true;
        }

        // Check if camera left screen
        if (!CameraMove.Instance.GetCurrentSceneName().Equals("Swimming"))
        {
            count = 0;
            Destroy(this.gameObject);
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); //camera bounds
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);

        if (inPlay) //if in playing bounds of fish
        {
            transform.Translate(x, y, z); //move
        }
        else //else
        {
            transform.Translate(x, y, z); //Completely move object off screen
            removeInstance();
        }
        frames_passed++;

		print(score);

		if(IngameDate.GetInstance().getCurrentDay() < 3) {
			if(score >= total) {
				switch(IngameDate.GetInstance().getCurrentDay()){
					case 0:
						objectiveTextUI.updateObjectiveText("Congratulations, you’re a Snap Shooter! You win!");
						break;
					case 1:
						objectiveTextUI.updateObjectiveText("Congratulations, you’re an Amateur Photographer! You win!");
						break;
					case 2:
						objectiveTextUI.updateObjectiveText("Congratulations, you’re an Artist Photographer! You win!");
						break;
				}
			} 
			else 
			{
				objectiveTextUI.updateObjectiveText("Swimminng Game is coming soon!");
    		}
		}
        
        // Check if on screen, and if so, update notebook
        if (onScreen)
        {
            switch (fishType)
            {
                case "clownfish":
                    //notebook.GetComponent<NotebookScript>().foundClownfish();
                    GlobalValueTracker.Instance.foundClown = true;
                    break;
                case "damsel":
                    //notebook.GetComponent<NotebookScript>().foundDamselfish();
                    GlobalValueTracker.Instance.foundDamsel = true;
                    break;
            }
        }
        
        // Update variables if day changes
        if (lastDayUpdated != IngameDate.GetInstance().getCurrentDay())
        {
            score = 0;
            GlobalValueTracker.Instance.fishPhotographed = 0;
        }

        lastDayUpdated = IngameDate.GetInstance().getCurrentDay();
    }

    public void determineDirection()
    {
        float random_speed = Random.Range(0.25f, 1.0f); //set a random speed
        movement_speed = random_speed * Time.deltaTime;

        direction = Random.Range(1, 3);

        switch (direction)
        {
            case 1: //direction right
                x = 0;
                y = 0;
                z = -movement_speed;
                
                // Rotate 90 degrees to fix model
                transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
                
                break;
            case 2: //direction left
                x = 0;
                y = 0;
                z = -movement_speed;
                
                // Rotate -90 degrees to fix model
                transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
                
                break;
        }
    }

    public void spawnInstance()
    {
        Vector3 v3Pos = new Vector3(0f, 0f, 0f);
        int spawnLoc = Random.Range(1, 3);
        switch (spawnLoc)
        {
            case 1: //along left wall
                v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(-0.2f, Random.Range(-0.5f, 2.0f), Random.Range(0.5f, 1.0f)));
                break;
            case 2: //along right wall
                v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(2.0f, Random.Range(-0.5f, 5.0f), Random.Range(0.5f, 1.0f)));
                break;
        }
        
        // Randomly assign material to this GameObject
        int randMaterial = Random.Range(1, 3);
        switch (randMaterial)
        {
            case 1: // orange material
                GetComponent<Renderer>().material = orangeMaterial;
                fishType = "clownfish";
                break;
            case 2: // blue material
                GetComponent<Renderer>().material = blueMaterial;
                fishType = "damsel";
                break;
        }

        GameObject child = Instantiate(this.gameObject, v3Pos, Quaternion.identity);
        print("Fish Created.");
    }

    public void removeInstance()
    {
        spawnInstance();
        // if (count < 16)
        // {
        //     spawnInstance();
        //     count++;
        // }

        Destroy(this.gameObject);
        print("Fish Removed: Left the screen");
    }
}
