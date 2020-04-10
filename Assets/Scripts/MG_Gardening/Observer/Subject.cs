using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    private List<Observer> _interestedObservers;

    public Subject(){
        this._interestedObservers = new List<Observer>();
    }

    public void registerObserver(Observer observer){
        _interestedObservers.Add(observer);
    }

    public void unregisterObserver(Observer observer){
        _interestedObservers.Remove(observer);
    }

    protected void notifyObjects(){
        foreach(Observer o in _interestedObservers){
            o.HandleSubjectChanges(this);
        }
    }    

}
