using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Equipment")]

public class EquipmentData : ItemData{
    public enum Tooltype{Shovel, Hoe, Fork, Bucket, BucketWithWater, Seedbag};
    public Tooltype toolType;
}
