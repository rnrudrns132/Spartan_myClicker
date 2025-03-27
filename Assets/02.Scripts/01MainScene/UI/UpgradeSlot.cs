using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSlot : MonoBehaviour
{
    UpgradeManager upgradeM => MainSceneManager.msm.upgradeManager;
    
    public UpgradeSO mySO;
    
    [SerializeField] private Image myImg;
    [SerializeField] private TextMeshProUGUI myNameText;
    [SerializeField] private TextMeshProUGUI myExpText;
    [SerializeField] private TextMeshProUGUI myCostText;

    public int nowUpgradeLv => GameManager.gm.nowData.UpgradeLvs[mySO.myIndex];
    public int nextUpgradeLv => nowUpgradeLv + 1;

    public void Initializer(UpgradeSO target)
    {
        mySO = target;
        myImg.sprite = mySO.mySpr;

        SetSlot();

        MainSceneManager.msm.OnGoldChanged += SetCost;
        upgradeM.OnUpgrade += SetSlot;
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

        myCostText.color = MainSceneManager.msm.HasGold(cost) ? Color.black : Color.red;
    }
    public void ClickUpgrade()
    {
        upgradeM.Upgrade(this);
    }
}
