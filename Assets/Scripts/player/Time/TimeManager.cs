using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance{get; private set;}

    [Header("Internal Clock")]
    [SerializeField]
    GameTimeStamp timeStamp;
    public float timeScale = 1.0f; 
    List<ITimeTracker> listeners = new List<ITimeTracker>();
    void Awake(){
        if(Instance != this && Instance != null){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }
    void Start(){
        timeStamp = new GameTimeStamp(0, 0, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate(){
        while(true){
            yield return new WaitForSeconds(1.0f/timeScale);
            Tick();
        }
    }
    
    public void Tick(){
        timeStamp.UpdateClock();
        foreach(ITimeTracker listener in listeners){
            listener.ClockUpdate(timeStamp);
        }
    }

    public GameTimeStamp GetGameTimeStamp(){
        return new GameTimeStamp(timeStamp);
    }

    public void RegisterTracker(ITimeTracker listener){
        listeners.Add(listener);
    }
    
    public void UnregisterTracker(ITimeTracker listener){
        listeners.Remove(listener);
    }
}
