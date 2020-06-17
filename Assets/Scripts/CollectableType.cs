using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable Type", menuName = "Collectable Type")]
public class CollectableType : ScriptableObject
{
    public Sprite sprite;
    public string type;
    public int score;
    public float staminaIncrease;
}
