using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public static int Money { get; private set; } //we dont want other class to mess with money int at all

    public static void Spend(int cost)
    {
        //check if player has enough to spend
        if (cost > Money)
        {
            Debug.LogError("Player does not have enough money");
            return;
        }
        Money -= cost;
        UIManager.Instance.RenderPlayerStats();

    }
    public static void Earn(int income)
    {
        Money += income;
        UIManager.Instance.RenderPlayerStats();
    }

}
