using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO_", menuName = "ScrtiptableObject/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public int myIndex;
    public Sprite mySpr;
    public string myName;

    public int BaseATK;
    public float BaseCriticalProb;

    public int AtkPerLv;
    public float CriticalProbPerLv;

    public int myBuyCost;
    public int[] myUpgCost;

    public int ReturnAtk(int lv)
    {
        return BaseATK + AtkPerLv * lv;
    }
    public float ReturnCriticalProb(int lv)
    {
        return BaseCriticalProb + CriticalProbPerLv * lv;
    }
    public string ReturnName(int lv)
    {
        return $"{myName} Lv.{lv}";
    }
    public string ReturnExp(int lv)
    {
        return $"공격력: {ReturnAtk(lv)}<br>치명타 확률: {ReturnCriticalProb(lv):0.0}%";
    }
    public int ReturnUpgCost(int targetLv)
    {
        int lastIndex = myUpgCost.Length - 1;
        if (targetLv <= lastIndex) return myUpgCost[targetLv];
        else return myUpgCost[lastIndex] + (targetLv - lastIndex) * 10;
    }
}
