using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GlobalValueTracker : MonoBehaviour
{
    private static GlobalValueTracker _igd;
    private float _pollutionLevel = 0.0f;

    public static GlobalValueTracker Instance{get; private set;}

    public GameObject water;

    public int max_butterfly;
    public float tod;
    public int max_fish;
    public int bird_count;
    public int goldfinchCount;
    public int sparrowCount;
    public int max_bird;
    public int day;
    public bool update;
    public GameObject dead_coral;
    public float min_time;
    public float max_time;
    public int fishCaught;
    public int butterflyCaught;
    public int fishPhotographed;
    public int plantsHarvested;
    public int birdsFound;

    public int fishing_goal;
    public int swimming_goal;
    public int birds_goal;
    public int butterfly_goal;
    public int gardening_goal;

    public bool g1;
    public bool g2;
    public bool g3;
    public bool g4;
    public bool g5;

    public bool foundClown;
    public bool foundDamsel;
    public bool foundFinch;
    public bool foundSparrow;

    public GameObject notebook;
    public GameObject gardenOverlay;
    public GameObject titleOverlay, transport1, transport2, transport3, transport4, transport5;
    public GameObject notebookBtn;
    public GameObject quitBtn;
    public GameObject returnBtn;

    public Text objectiveText;

    public sun_controller sun_Controller;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    public static GlobalValueTracker GetInstance()
    {
      if (_igd == null)
      {
        _igd = new GlobalValueTracker();
      }
      return _igd;
    }

    void Start()
    {
        max_butterfly = 10;
        tod = 50;
        day = 1;
        max_fish = 15;
        min_time = 3.0f;
        max_time = 7.0f;
        update = false;
        fishing_goal = 6;
        swimming_goal = 12;
        birds_goal = 15;
        butterfly_goal = 10;
        gardening_goal = 0;
        max_bird = 2; //This number will be x4 (so if max_bird = 4, 16 birds can be on screen at a time

        fishCaught = 0;
        butterflyCaught = 0;
        fishPhotographed = 0;
        plantsHarvested = 0;
        birdsFound = 0;

        g1 = false;
        g2 = false;
        g3 = false;
        g4 = false;
        g5 = false;

        foundClown = false;
        foundDamsel = false;
        foundFinch = false;
        foundSparrow = false;

        transport1 = GameObject.Find("ClickToTransport_Butterfly");
        transport2 = GameObject.Find("ClickToTransport_Fishing");
        transport3 = GameObject.Find("ClickToTransport_Gardening");
        transport4 = GameObject.Find("ClickToTransport_Swimming");
        transport5 = GameObject.Find("ClickToTransport_Bird");
        transport1.SetActive(false);
        transport2.SetActive(false);
        transport3.SetActive(false);
        transport4.SetActive(false);
        transport5.SetActive(false);
    }

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    // Update is called once per frame
    void Update()
    {
        if (!update)
        {
            notebook.GetComponent<NotebookScript>().SetFishingNum(fishCaught, fishing_goal);
            notebook.GetComponent<NotebookScript>().SetSwimmingNum(fishPhotographed, swimming_goal);
            notebook.GetComponent<NotebookScript>().SetButterflyNum(butterflyCaught, butterfly_goal);
            notebook.GetComponent<NotebookScript>().SetBirdsNum(birdsFound, birds_goal);
            notebook.GetComponent<NotebookScript>().SetGardeningNum(plantsHarvested, gardening_goal);
        }
        if (butterflyCaught > 0)
        {
            notebook.GetComponent<NotebookScript>().foundMonarch();
        }
        if (foundClown)
        {
            notebook.GetComponent<NotebookScript>().foundClownfish();
        }
        if (foundDamsel)
        {
            notebook.GetComponent<NotebookScript>().foundDamselfish();
        }
        if (foundFinch)
        {
            notebook.GetComponent<NotebookScript>().foundFinch();
        }
        if (foundSparrow)
        {
            notebook.GetComponent<NotebookScript>().foundSparrow();
        }
        if (g1 && CameraMove.Instance.GetCurrentSceneName() == "Butterfly")
        {
            objectiveText.text = ("Catch some butterflies!");
            StartCoroutine(displayMinigameEndText("butterfly"));
            g1 = false;
        }
        if (g2 && CameraMove.Instance.GetCurrentSceneName() == "Fishing")
        {
            objectiveText.text = ("Catch some fish!");
            StartCoroutine(displayMinigameEndText("fishing"));
            g2 = false;
        }
        if (g3 && CameraMove.Instance.GetCurrentSceneName() == "Swimming")
        {
            objectiveText.text = ("Photograph some fish!");
            StartCoroutine(displayMinigameEndText("swimming"));
            g3 = false;
        }
        if (g4 && CameraMove.Instance.GetCurrentSceneName() == "Gardening")
        {
            objectiveText.text = ("Harvest some crops and plant new ones!");
            StartCoroutine(displayMinigameEndText("gardening"));
            g4 = false;
        }

        if (g5 && CameraMove.Instance.GetCurrentSceneName() == "Birdwatching")
        {
            // objectiveText.text = ("Count number of different types of birds!");
            // StartCoroutine(displayMinigameEndText("birdwatching"));
            // g5 = false;
            objectiveText.text = ("Bird Watching Game is coming soon!");
            StartCoroutine(displayMinigameEndText("birdwatching"));
            g5 = false;
        }

        if (CameraMove.Instance.GetCurrentSceneName() == "Default")
        {
            notebookBtn.SetActive(true);
            quitBtn.SetActive(true);
            returnBtn.SetActive(false);
        }
        else
        {
            notebookBtn.SetActive(false);
            quitBtn.SetActive(false);
            returnBtn.SetActive(true);
        }

        if (tod > 180)
        {
            day++;
            update = true;
            butterflyCaught = 0;
            fishCaught = 0;
            fishPhotographed = 0;
            plantsHarvested = 0;
            birdsFound = 0;
        }
        if(update && day == 2)
        {
            max_butterfly = 5;
            max_fish = 10;
            update = false;
            min_time = 5;
            max_time = 9;
            max_bird = 2;

            fishing_goal = 4;
            swimming_goal = 10;
            birds_goal = 15;
            butterfly_goal = 10;
            gardening_goal = 2;

            

            water.transform.position = new Vector3(water.transform.position.x, water.transform.position.y + 0.6f, water.transform.position.z);

            notebook.GetComponent<NotebookScript>().refreshAll();

            for (int i = 1; i < 6; i++)
            {
                GameObject.Find("coral" + i.ToString()).GetComponent<Renderer>().sharedMaterial = dead_coral.GetComponent<Renderer>().sharedMaterial;
            }
        }
        if (update && day == 3)
        {
            max_butterfly = 2;
            max_fish = 5;
            update = false;
            min_time = 7;
            max_bird = 1;
            max_time = 11;

            fishing_goal = 4;
            swimming_goal = 10;
            birds_goal = 15;
            butterfly_goal = 10;
            gardening_goal = 2;

            water.transform.position = new Vector3(water.transform.position.x, water.transform.position.y + 0.6f, water.transform.position.z);

            notebook.GetComponent<NotebookScript>().refreshAll();
            for (int i = 6; i < 16; i++)
            {
                GameObject.Find("coral" + i.ToString()).GetComponent<Renderer>().sharedMaterial = dead_coral.GetComponent<Renderer>().sharedMaterial;
            }
        }
        if (update && day == 4)
        {
            g1 = true;
            g2 = true;
            g3 = true;
            g4 = true;
            g5 = true;
            
            max_butterfly = 0;
            max_fish = 0;
            update = false;
            min_time = 30;
            max_bird = 0;
            max_time = 60;

            water.transform.position = new Vector3(water.transform.position.x, water.transform.position.y + 0.6f, water.transform.position.z);

            fishing_goal = 4;
            swimming_goal = 10;
            birds_goal = 15;
            butterfly_goal = 10;
            gardening_goal = 2;
            notebook.GetComponent<NotebookScript>().refreshAll();
            for (int i = 16; i < 23; i++)
            {
                GameObject.Find("coral" + i.ToString()).GetComponent<Renderer>().sharedMaterial = dead_coral.GetComponent<Renderer>().sharedMaterial;
            }
        }
        if(update && day == 5)
        {
            Invoke("endTitle", 3.1f);
            Invoke("endText", 5.0f);
            Invoke("credits", 9.0f);
            GameObject.Find("StartGame").SetActive(false);
            update = false;
        }

        if(butterflyCaught >= butterfly_goal && 
        fishPhotographed >= swimming_goal && 
        plantsHarvested >= gardening_goal &&
        fishCaught >= fishing_goal &&
        birdsFound >= birds_goal){
            //Jump to next day:
            Debug.Log("You completed all objectives!");
            day++;
            update = true;
            butterflyCaught = 0;
            fishCaught = 0;
            fishPhotographed = 0;
            plantsHarvested = 0;
            birdsFound = 0;
            sun_Controller.nextDay();
        }


    }

    public void addPollutionLevel(float f){
        this._pollutionLevel += f;
    }

    public int getButterflyCount(){
        return 30;
    }

    public int getFishCount(){
        return 30;
    }

    public float getWaterlevelOffset(){
        return 0.0f;
    }

    public void endTitle()
    {
        transport1.SetActive(false);
        transport2.SetActive(false);
        transport3.SetActive(false);
        transport4.SetActive(false);
        gardenOverlay.SetActive(false);
        titleOverlay.SetActive(true); 
        transport5.SetActive(false);
    }

    public void endText()
    {
        Text txt = GameObject.Find("AGarden").GetComponent<Text>();
        txt.text = "The Earth Was A Garden";
    }

    public void credits()
    {
        Text txt = GameObject.Find("AGarden").GetComponent<Text>();
        txt.fontSize = 20;
        txt.text = "Credits: " +
            "Programming:\n" +
            "Rory Sullivan, John Amaral, Tianze Chen\n" +
            "Shuxing Li, Xinxin Qian, Meijie Wang\n" +
            "Music/Audio:\n" +
            "John Amaral, Nicola Goldman\n" +
            "Art:\n" +
            "Olivia Bogs, Tianze Chen, Jack Riley\n" +
            "Yunjie Yang\n" +
            "Writting, Design, and Buissness:\n" +
            "Olivia Bogs, Remi Dumont, Nicola Goldman\n" +
            "Anne M. Johnson, Shuxing Li, Meijie Wang\n" +
            "Project Manager:\n" +
            "Lee Sheldon";
        Time.timeScale = 0;
    }
    
    // Waiting on final day to change text
    IEnumerator displayMinigameEndText(string minigame)
    {
        yield return new WaitForSeconds(5);
        switch (minigame)
        {
            case "butterfly":
                objectiveText.text = "Congratulations! you're a Master Butterfly Catcher! You win automatically!";
                break;
            case "fishing":
                objectiveText.text = "Congratulations! you're a World Class Fisherman! You win automatically!";
                break;
            case "swimming":
                objectiveText.text = "Congratulations! you're an Artist Photographer! You win automatically!";
                break;
            case "gardening":
                objectiveText.text = "Congratulations! you're a Master Gardener! You win automatically!";
                break;
            case "birdwatching":
                objectiveText.text = "Congratulations, you’re a Soaring Eagle! You win automatically!";
                break;
        }
    }

}
