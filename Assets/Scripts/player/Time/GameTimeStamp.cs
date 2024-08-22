using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimeStamp
{
    [Header("Internal Clock")]
    public int second;
    public int minute;
    public int hour;
    // Start is called before the first frame update
    public GameTimeStamp(int second ,int minute , int hour ){
        this.second = second;
        this.minute = minute;
        this.hour = hour;
    }

    public GameTimeStamp(GameTimeStamp timeStamp){
        this.second = timeStamp.second;
        this.minute = timeStamp.minute;
        this.hour = timeStamp.hour;
    }

    public void UpdateClock(){
        second++;
        if(second % 60 == 0){
            second = 0;
            minute++;
        }
        if(minute > 0 && minute % 60 == 0){
            minute = 0;
            hour++;
        }
    }

    public static int MinuteToSecond(int minute){
        return minute * 60;
    }

    public static int HourToMinute(int hour){
        return hour * 60;
    }

    public static int HourToSecond(int hour){
        return hour * 3600;
    }

    public static int CompareTimeStamps(GameTimeStamp timeStamp1, GameTimeStamp timeStamp2){
        int timeStamp1seconds = HourToSecond(timeStamp1.hour) + MinuteToSecond(timeStamp1.minute) + timeStamp1.second;
        int timeStamp2seconds = HourToSecond(timeStamp2.hour) + MinuteToSecond(timeStamp2.minute) + timeStamp2.second;
        int diff = timeStamp2seconds - timeStamp1seconds;
        return Mathf.Abs(diff);
    }
}
