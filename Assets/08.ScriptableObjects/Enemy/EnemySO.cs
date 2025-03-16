using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO_", menuName = "ScriptableObject/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int myIndex;
    public int myHP;
    public string myName;

    public GameObject myPrefab;

    public int myPoint;
    public int myGold;

    public int ReturnHP(int lv)
    {
        return myHP * (lv + 1);
    }
    public int ReturnPoint(int lv)
    {
        return myPoint * (lv + 1);
    }
    public int ReturnGold(int lv)
    {
        return myGold * (lv + 1);
    }
    public string ReturnName(int lv)
    {
        return $"{myName} {lv}";
    }
}
