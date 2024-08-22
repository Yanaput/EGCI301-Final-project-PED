using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour, ITimeTracker
{
    SeedData seedToGrow;
    [Header("Crop Stages")]
    public GameObject seed;
    private GameObject seedling;
    private GameObject harvestable;

    int growth;
    int maxGrowth;
    GameTimeStamp timeStartPlant;
    GameTimeStamp wateredTime;
    public enum CropState{
        Seed, Seedling, Harvestable
    }
    public CropState cropState;
    public void Plant(SeedData seedToGrow, GameTimeStamp wateredTime){
        this.seedToGrow = seedToGrow;
        this.wateredTime = wateredTime;
        seedling = Instantiate(seedToGrow.seedling, transform);
        ItemData cropToYield = seedToGrow.cropToYield;
        harvestable = Instantiate(cropToYield.gameModel, transform);
        
        maxGrowth = seedToGrow.secondToGrow;

        SwitchState(CropState.Seed);
        timeStartPlant = TimeManager.Instance.GetGameTimeStamp();
        TimeManager.Instance.RegisterTracker(this);
    }

    public void Grow(){
        growth++;
        if(growth >= maxGrowth/2 && cropState == CropState.Seed){
            SwitchState(CropState.Seedling);
        }
        if(growth >= maxGrowth){
            SwitchState(CropState.Harvestable);
        }
    }

    public void ClockUpdate(GameTimeStamp timeStamp){
        int secondsElapsed = GameTimeStamp.CompareTimeStamps(timeStartPlant, timeStamp);
        if(secondsElapsed == maxGrowth/2){
            SwitchState(CropState.Seedling);
        }
        else if(secondsElapsed == maxGrowth){
            SwitchState(CropState.Harvestable);
        }
    }

    public void DestroyCrop(){
        if (this != null && gameObject != null) {
            Destroy(this);
            Destroy(gameObject);
        }
    }

       void SwitchState(CropState stateToSwitch){
        //check if the Crop is not null in case it was destroyed
        if(this != null){
            seed.SetActive(false);
            seedling.SetActive(false);
            harvestable.SetActive(false);
            
            switch(stateToSwitch){
                case CropState.Seed:
                    seed.SetActive(true);
                    break;
                case CropState.Seedling:
                    seedling.SetActive(true);
                    break;
                case CropState.Harvestable:
                    harvestable.SetActive(true);
                    break;
            }
            cropState = stateToSwitch;
        }
        return;
    }
}
