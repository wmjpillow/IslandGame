using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer
{
    private List<Observer> _interestedObservers;

    //Respond to changes.
    public virtual void HandleSubjectChanges(Subject s){
        Debug.Log("I Noticed a change!");
    }
}
