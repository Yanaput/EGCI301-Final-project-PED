using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public enum LandStatus{
        Soil, Farmland, Watered
    }
    public Material soilMat, farmLandMat, WateredFarmLandMat;
    new Renderer renderer;
    public LandStatus landStatus;
    public GameObject select;
    int landDryTime = 400;
    GameTimeStamp timeWatered;
    [Header("Crops")]
    public GameObject cropPrefab;
    CropBehaviour cropPlanted = null;

    void Start(){
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Soil);
        Select(false);
        TimeManager.Instance.RegisterTracker(this);
    }

    public void SwitchLandStatus(LandStatus statusToSwitch){
        landStatus = statusToSwitch;
        Material materialToSwitch = soilMat;
        switch (statusToSwitch){
            case LandStatus.Soil:
                materialToSwitch = soilMat;
                break;
            case LandStatus.Farmland:
                materialToSwitch = farmLandMat;
                break;
            case LandStatus.Watered:
                materialToSwitch = WateredFarmLandMat;
                timeWatered = TimeManager.Instance.GetGameTimeStamp();
                break;
        }
        renderer.material = materialToSwitch;
    }

    public void Select(bool toggle){
        select.SetActive(toggle);
    }

    public void Interact(){
        ItemData toolSlot = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);

        if (toolSlot == null){
            return; 
        }

        EquipmentData equipmentTool = toolSlot as EquipmentData;
        if(equipmentTool != null){
            EquipmentData.Tooltype toolType = equipmentTool.toolType;

            switch (toolType){
                case EquipmentData.Tooltype.Hoe:
                    SwitchLandStatus(LandStatus.Farmland);
                    break;
                case EquipmentData.Tooltype.BucketWithWater:
                    if(landStatus == LandStatus.Farmland){
                        SwitchLandStatus(LandStatus.Watered);
                    }
                    break;
                case EquipmentData.Tooltype.Shovel:
                    if(cropPlanted != null){
                        Destroy(cropPlanted.gameObject);
                    }
                    SwitchLandStatus(LandStatus.Soil);
                    break;
                default:
                    Debug.Log("incrroect type");
                    break;
            }
            return;
        }

        SeedData seedTool = toolSlot as SeedData;

        if(seedTool != null && landStatus == LandStatus.Watered && cropPlanted == null){
            GameObject cropObject = Instantiate(cropPrefab, transform);
            cropObject.transform.position = new Vector3(transform.position.x, 0.36f, transform.position.z);

            cropPlanted = cropObject.GetComponent<CropBehaviour>();
            cropPlanted.Plant(seedTool, timeWatered);

            //Consume the item
            InventoryManager.Instance.ConsumeItem(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));
        }
    }

    public void ClockUpdate(GameTimeStamp timeStamp){
        if(landStatus == LandStatus.Watered){
            int secondsElapsed = GameTimeStamp.CompareTimeStamps(timeWatered, timeStamp);
            if(secondsElapsed == landDryTime){
                if(cropPlanted != null){
                    Destroy(cropPlanted.gameObject);
                }
                SwitchLandStatus(LandStatus.Soil);
            }
        }
    }
}
