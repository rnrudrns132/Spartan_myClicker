using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    GameManager gm => GameManager.gm;
    MainSceneManager msm => MainSceneManager.msm;

    [SerializeField] private UpgradeSlot[] upgradeSlots;

    public void Initializer()
    {
        for (int i = 0; i < 3; i++)
        {
            upgradeSlots[i].Initializer(gm.upgradeSOs[i]);
        }
        OnUpgrade += gm.SetStat;
    }
    public event Action OnUpgrade;
    public void Upgrade(UpgradeSlot targetSlot)
    {
        int cost = targetSlot.mySO.ReturnUpgCost(targetSlot.nextUpgradeLv);
        if (msm.HasGold(cost))
        {
            msm.UseGold(cost);
            gm.nowData.UpgradeLvs[targetSlot.mySO.myIndex] = targetSlot.nextUpgradeLv;

            gm.PlaySFX(SFXEnum.UPGRADE);

            OnUpgrade?.Invoke();
        }
        else gm.ShowAlert("골드가 부족합니다");
    }
}
