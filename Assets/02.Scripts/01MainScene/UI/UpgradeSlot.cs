using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSlot : MonoBehaviour
{
    public UpgradeSO mySO;
    public Image myImg;
    public TextMeshProUGUI myNameText;
    public TextMeshProUGUI myExpText;
    public TextMeshProUGUI myCostText;

    public int nowUpgradeLv => GameManager.gm.nowData.UpgradeLvs[mySO.myIndex];
    public int nextUpgradeLv => nowUpgradeLv + 1;

    public void Initializer(UpgradeSO target)
    {
        mySO = target;
        myImg.sprite = mySO.mySpr;

        SetSlot();

        MainSceneManager.msm.OnGoldChanged += SetCost;
        MainSceneManager.msm.upgradeManager.OnUpgrade += SetSlot;
    }
    void SetSlot()
    {
        myNameText.text = mySO.ReturnName(nowUpgradeLv);
        myExpText.text = mySO.ReturnExp(nowUpgradeLv);
        SetCost();
    }
    void SetCost()
    {
        int cost = mySO.ReturnUpgCost(nextUpgradeLv);
        myCostText.text = cost.ToString();

        if (MainSceneManager.msm.HasGold(cost)) myCostText.color = Color.black;
        else myCostText.color = Color.red;
    }
    public void ClickUpgrade()
    {
        MainSceneManager.msm.upgradeManager.Upgrade(this);
    }
}
