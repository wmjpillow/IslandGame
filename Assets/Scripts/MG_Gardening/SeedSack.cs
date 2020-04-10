using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSack : ClickableObject
{
    public override void TriggerInteract(){
        //I don't know, just put down something..
        //GardeningManager.Instance.SetInHand("iHonk");
        GardeningManager.Instance.SetInhandObjectType(GardeningManager.InhandObjectType.SEED);
        Debug.Log("We don't have other kind of seeds!");
    }
}
