using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookScript : MonoBehaviour
{
    public Text FishingNum;
    public Text SwimmingNum;
    public Text BirdsNum;
    public Text ButterflyNum;
    public Text GardeningNum;

    public GameObject FishingLine;
    public GameObject SwimmingLine;
    public GameObject BirdsLine;
    public GameObject ButterflyLine;
    public GameObject GardeningLine;

    public GameObject clownfishCheck;
    public GameObject damselfishCheck;
    public GameObject finchCheck;
    public GameObject sparrowCheck;
    public GameObject holderCheck;
    public GameObject monarchCheck;

    private int fishingNow = 0;
    private int fishingGoal = 10;
    private int SwimmingNow = 0;
    private int SwimmingGoal = 10;
    private int BirdsNow = 0;
    private int BirdsGoal = 10;
    private int ButterflyNow = 0;
    private int ButterflyGoal = 10;
    private int GardeningNow = 0;
    private int GardeningGoal = 0;

    public ThrowRod fishingScript;

    // Start is called before the first frame update
    void Start()
    {
        fishingGoal = GlobalValueTracker.Instance.fishing_goal;
        SwimmingGoal = GlobalValueTracker.Instance.swimming_goal;
        BirdsGoal = GlobalValueTracker.Instance.birds_goal;
        ButterflyGoal = GlobalValueTracker.Instance.butterfly_goal;
        GardeningGoal = GlobalValueTracker.Instance.gardening_goal;

        refreshFishing();
        refreshSwimming();
        refreshBirds();
        refreshButterfly();
        refreshGardening();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFishingNum(int fishing_now, int fishing_goal)
    {
        if (fishing_now != -1)
        {
            Debug.Log("getin!"+fishing_now);
            fishingNow = fishing_now;
        }
        if (fishing_goal != -1)
        {
            fishingGoal = fishing_goal;
        }
        refreshFishing();
    }

    public void refreshFishing()
    {
        FishingNum.text = fishingNow + "/" + fishingGoal;
        Debug.Log(FishingNum.text);
        if (fishingNow >= fishingGoal)
        {
            FishingLine.SetActive(true);
        }
        else
        {
            FishingLine.SetActive(false);
        }
    }

    public void SetSwimmingNum(int swimming_now, int swimming_goal)
    {
        if (swimming_now != -1)
        {
            SwimmingNow = swimming_now;
        }
        if (swimming_goal != -1)
        {
            SwimmingGoal = swimming_goal;
        }
        refreshSwimming();
    }
    public void refreshSwimming()
    {
        SwimmingNum.text = SwimmingNow + "/" + SwimmingGoal;
        if (SwimmingNow >= SwimmingGoal)
        {
            SwimmingLine.SetActive(true);
        }
        else
        {
            SwimmingLine.SetActive(false);
        }
    }

    public void SetBirdsNum(int birds_now, int birds_goal)
    {
        if (birds_now != -1)
        {
            BirdsNow = birds_now;
        }
        if (birds_goal != - 1)
        {
            BirdsGoal = birds_goal;
        }
        refreshBirds();
    }

    public void refreshBirds()
    {
        BirdsNum.text = BirdsNow + "/" + BirdsGoal;
        if (BirdsNow >= BirdsGoal)
        {
            BirdsLine.SetActive(true);
        }
        else
        {
            BirdsLine.SetActive(false);
        }
    }

    public void SetButterflyNum(int butterfly_now,int butterfly_goal)
    {
        if (butterfly_now != -1)
        {
            ButterflyNow = butterfly_now;
        }
        if (butterfly_goal != -1)
        {
            ButterflyGoal = butterfly_goal;
        }
        refreshButterfly();
    }
    public void refreshButterfly()
    {
        ButterflyNum.text = ButterflyNow + "/" + ButterflyGoal;
        if (ButterflyNow >= ButterflyGoal)
        {
            ButterflyLine.SetActive(true);
        }
        else
        {
            ButterflyLine.SetActive(false);
        }
    }

    public void SetGardeningNum(int gardening_now, int gardening_goal)
    {
        if (gardening_now != -1)
        {
            GardeningNow = gardening_now;
        }
        if (gardening_goal != -1)
        {
            GardeningGoal = gardening_goal;
        }
        refreshGardening();
    }

    public void refreshGardening()
    {
        GardeningNum.text = GardeningNow + "/" + GardeningGoal;
        if (GardeningNow >= GardeningGoal)
        {
            GardeningLine.SetActive(true);
        }
        else
        {
            GardeningLine.SetActive(false);
        }
    }

    public void refreshAll()
    {
        fishingNow = 0;
        fishingGoal = GlobalValueTracker.Instance.fishing_goal;
        fishingScript.clearScore();
        SwimmingNow = 0;
        SwimmingGoal = GlobalValueTracker.Instance.swimming_goal;
        BirdsNow = 0;
        BirdsGoal = GlobalValueTracker.Instance.birds_goal;
        ButterflyNow = 0;
        ButterflyGoal = GlobalValueTracker.Instance.butterfly_goal;
        GardeningNow = 0;
        GardeningGoal = GlobalValueTracker.Instance.gardening_goal;

        refreshFishing();
        refreshSwimming();
        refreshBirds();
        refreshButterfly();
        refreshGardening();
    }

    public void foundClownfish()
    {
        clownfishCheck.SetActive(true);
    }
    public void foundDamselfish()
    {
        damselfishCheck.SetActive(true);
    }
    public void foundFinch()
    {
        finchCheck.SetActive(true);
    }
    public void foundSparrow()
    {
        sparrowCheck.SetActive(true);
    }
    public void foundMonarch()
    {
        monarchCheck.SetActive(true);
    }
}
