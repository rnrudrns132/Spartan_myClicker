using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    GameManager gm => GameManager.gm;
    WeaponManager weaponM => MainSceneManager.msm.weaponManager;
    
    public WeaponSO mySO;

    [SerializeField] private Image myImg;
    [SerializeField] private GameObject myImgBlind;
    [SerializeField] private TextMeshProUGUI myNameText;
    [SerializeField] private TextMeshProUGUI myExpText;

    [SerializeField] private TextMeshProUGUI myBuyCostText;
    [SerializeField] private TextMeshProUGUI myUpgCostText;

    [SerializeField] private GameObject BuyBtnObj;
    [SerializeField] private GameObject UpgBtnObj;
    [SerializeField] private GameObject EquipBtnObj;

    public void Initializer(WeaponSO target)
    {
        mySO = target;
        myImg.sprite = mySO.mySpr;

        SetSlot();

        MainSceneManager.msm.OnPointChanged += SetCost;
        weaponM.OnNowWeaponChanged += SetSlot;
    }

    public void SetSlot()
    {
        if (gm.nowData.WeaponDatas.TryGetValue(mySO.myIndex, out int upgLv))
        {
            myNameText.text = mySO.ReturnName(upgLv);
            myExpText.text = mySO.ReturnExp(upgLv);
            myImgBlind.SetActive(false);

            BuyBtnObj.SetActive(false);
            UpgBtnObj.SetActive(true);
            
            EquipBtnObj.SetActive(gm.nowData.nowWeaponIndex != mySO.myIndex);
        }
        else
        {
            myNameText.text = "???";
            myExpText.text = mySO.ReturnExp(0);
            myImgBlind.SetActive(true);

            BuyBtnObj.SetActive(true);
            UpgBtnObj.SetActive(false);
            EquipBtnObj.SetActive(false);
        }
        SetCost();
    }
    void SetCost()
    {
        if (gm.nowData.WeaponDatas.TryGetValue(mySO.myIndex, out int upgLv))
        {
            int cost = mySO.ReturnUpgCost(upgLv + 1);
            myUpgCostText.text = cost.ToString();

            myUpgCostText.color = MainSceneManager.msm.HasPoint(cost) ? Color.black : Color.red;
        }
        else
        {
            myBuyCostText.text = mySO.myBuyCost.ToString();
            myBuyCostText.color = MainSceneManager.msm.HasPoint(mySO.myBuyCost) ? Color.black : Color.red;
        }
    }

    public void ClickBuy()
    {
        weaponM.BuyWeapon(this);
    }
    public void ClickUpg()
    {
        weaponM.UpgWeapon(this);
    }
    public void ClickEquip()
    {
        weaponM.EquipWeapon(this);
    }
}
