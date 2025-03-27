using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WeaponManager : MonoBehaviour
{
    GameManager gm => GameManager.gm;
    MainSceneManager msm => MainSceneManager.msm;

    [SerializeField] private TextMeshProUGUI nowWeaponNameText;
    [SerializeField] private Image nowWeaponImage;
    [SerializeField] private TextMeshProUGUI nowWeaponExpText;

    public void Initializer()
    {
        SetNowWeapon();
        InitializeInventory();

        OnNowWeaponChanged += SetNowWeapon;
        OnNowWeaponChanged += gm.SetStat;
    }

    void SetNowWeapon()
    {
        WeaponSO nowWeapon = gm.weaponSOs[gm.nowData.nowWeaponIndex];
        int nowWeaponUpg = gm.nowData.WeaponDatas[nowWeapon.myIndex];

        nowWeaponNameText.text = nowWeapon.ReturnName(nowWeaponUpg);
        nowWeaponImage.sprite = nowWeapon.mySpr;
        nowWeaponExpText.text = nowWeapon.ReturnExp(nowWeaponUpg);
    }

    [SerializeField] private GameObject WeaponInventory;
    [SerializeField] private GameObject WeaponSlotPrefab;
    [SerializeField] private Transform WeaponSlotParent;
    void InitializeInventory()
    {
        foreach(var t in gm.weaponSOs)
        {
            WeaponSlot nowSlot = Instantiate(WeaponSlotPrefab, WeaponSlotParent).GetComponent<WeaponSlot>();
            nowSlot.Initializer(t);
        }
    }
    public void OpenInventory()
    {
        WeaponInventory.SetActive(true);
        gm.PlaySFX(SFXEnum.UI_CLICK);
    }
    public void CloseInventory()
    {
        WeaponInventory.SetActive(false);
        gm.PlaySFX(SFXEnum.UI_CLOSE);
    }

    public event Action OnNowWeaponChanged;

    public void BuyWeapon(WeaponSlot target)
    {
        if (msm.HasPoint(target.mySO.myBuyCost))
        {
            msm.UsePoint(target.mySO.myBuyCost);
            gm.nowData.WeaponDatas.Add(target.mySO.myIndex, 0);

            target.SetSlot();
            gm.PlaySFX(SFXEnum.UPGRADE);
        }
        else gm.ShowAlert("포인트가 부족합니다.");
    }
    public void UpgWeapon(WeaponSlot target)
    {
        int cost = target.mySO.ReturnUpgCost(gm.nowData.WeaponDatas[target.mySO.myIndex] + 1);
        if (msm.HasPoint(cost))
        {
            msm.UsePoint(cost);
            gm.nowData.WeaponDatas[target.mySO.myIndex] += 1;

            target.SetSlot();
            gm.PlaySFX(SFXEnum.UPGRADE);

            if (gm.nowData.nowWeaponIndex == target.mySO.myIndex)
            {
                OnNowWeaponChanged?.Invoke();
            }
        }
        else gm.ShowAlert("포인트가 부족합니다.");
    }
    public void EquipWeapon(WeaponSlot target)
    {
        gm.nowData.nowWeaponIndex = target.mySO.myIndex;
        OnNowWeaponChanged?.Invoke();
        gm.PlaySFX(SFXEnum.UI_CLICK);
    }
}
