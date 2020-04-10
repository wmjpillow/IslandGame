using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_move : MonoBehaviour
{
    public float movement_speed, x, y, z, timer, direction;
    Camera cameraVar;
    public int direction_length, frames_passed;
    bool remove;
    private Renderer fishRenderer;

    // Start is called before the first frame update
    void Start()
    {
        movement_speed = 1f * Time.deltaTime; //set the movement speed of fish
        remove = false;
        x = 0; //initialize the speeds of the fish in all directions to 0
        y = 0;
        z = 0;
        int directionLength = Random.Range(1, 4); //determine length of flight in determined direction
        timer = 0.0f;
        determineDirection();
        fishRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fishRenderer.isVisible)
        {

            timer += Time.deltaTime; //get how many seconds have passed
            int seconds = (int)timer % 60;

            if (remove) //fish offscreen?
            {
                Destroy(gameObject);
            }

            if (direction_length > seconds) //should it still be moving in defined direction?
            {
                Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); //camera bounds
                pos.x = Mathf.Clamp01(pos.x);
                pos.y = Mathf.Clamp01(pos.y);

                if (transform.position == Camera.main.ViewportToWorldPoint(pos)) //if in camera view
                {
                    transform.Translate(x, y, z); //move
                }
                else //else
                {
                    transform.Translate(x, y, z); //Completely move object off screen
                    remove = true; //mark for delete
                    print("Object Off Screen: Removed");
                }

                frames_passed++;
            }
            else //should switch direction
            {
                determineDirection();
                direction_length = Random.Range(1, 4); //determine length of flight in said direction
                timer = 0;
            }
        }
    }

    public void determineDirection()
    {
        float random_speed = Random.Range(0.25f, 1.0f); //set a random speed
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
                y = diagonal_speed;
                z = 0;
                break;
            case 2: //direction left
                x = -movement_speed;
                y = diagonal_speed;
                z = 0;
                break;
            case 3: //direction down
                x = diagonal_speed;
                y = movement_speed;
                z = 0;
                break;
            default: //direction up
                x = diagonal_speed;
                y = -movement_speed;
                z = 0;
                break;
        }
    }
}
