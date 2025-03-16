using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    GameManager gm => GameManager.gm;
    public WeaponSO mySO;

    public Image myImg;
    public GameObject myImgBlind;
    public TextMeshProUGUI myNameText;
    public TextMeshProUGUI myExpText;

    public TextMeshProUGUI myBuyCostText;
    public TextMeshProUGUI myUpgCostText;

    public GameObject BuyBtnObj;
    public GameObject UpgBtnObj;
    public GameObject EquipBtnObj;

    public void Initializer(WeaponSO target)
    {
        mySO = target;
        myImg.sprite = mySO.mySpr;

        SetSlot();

        MainSceneManager.msm.OnPointChanged += SetCost;
        MainSceneManager.msm.weaponManager.OnNowWeaponChanged += SetSlot;
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
            if (gm.nowData.nowWeaponIndex == mySO.myIndex) EquipBtnObj.SetActive(false);
            else EquipBtnObj.SetActive(true);
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

            if (MainSceneManager.msm.HasPoint(cost)) myUpgCostText.color = Color.black;
            else myUpgCostText.color = Color.red;
        }
        else
        {
            myBuyCostText.text = mySO.myBuyCost.ToString();
            if (MainSceneManager.msm.HasPoint(mySO.myBuyCost)) myBuyCostText.color = Color.black;
            else myBuyCostText.color = Color.red;
        }
    }

    public void ClickBuy()
    {
        MainSceneManager.msm.weaponManager.BuyWeapon(this);
    }
    public void ClickUpg()
    {
        MainSceneManager.msm.weaponManager.UpgWeapon(this);
    }
    public void ClickEquip()
    {
        MainSceneManager.msm.weaponManager.EquipWeapon(this);
    }
}
