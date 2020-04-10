using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameDate: Subject
{
  
    //Singleton
    private static IngameDate _igd;
    private int _currentDay;

    protected IngameDate() : base(){
        //Literally does nothing
        this._currentDay = 0;
    }

    public static IngameDate GetInstance()
    {
      if (_igd == null)
      {
        _igd = new IngameDate();
      }
      return _igd;
    }

    public void incrementDay(){
        this._currentDay += 1;
        notifyObjects();
    }

    public int getCurrentDay(){
      return this._currentDay;

    }
}
