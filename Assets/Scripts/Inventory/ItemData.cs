using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Item")]
public class ItemData : ScriptableObject{
    public string itemDescription;
    public Sprite itemSprite;
    public GameObject gameModel;
    public int cost;
}
