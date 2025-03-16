using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UpgradeSO", menuName ="ScriptableObject/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    public int myIndex;
    public Sprite mySpr;
    public string myName;
    public string myExp;

    public float ValuePerLv;
    public int[] UpgradeCost;

    public float ReturnValue(int lv)
    {
        return ValuePerLv * lv;
    }
    public int ReturnUpgCost(int targetLv)
    {
        int lastIndex = UpgradeCost.Length - 1;
        if (targetLv <= lastIndex) return UpgradeCost[targetLv];
        else return UpgradeCost[lastIndex] + (targetLv - lastIndex) * 10;
    }
    public string ReturnName(int lv)
    {
        return $"{myName} {lv}";
    }
    public string ReturnExp(int lv)
    {
        return string.Format(myExp, ReturnValue(lv));
    }
}
