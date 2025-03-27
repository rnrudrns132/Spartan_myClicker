using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainSceneManager : MonoBehaviour
{
    GameManager gm => GameManager.gm;
    public static MainSceneManager msm;
    private void Start()
    {
        msm = this;

        gm.FadeIn();
        gm.PlayBGM(BGMEnum.MAIN);

        InitializeResource();

        upgradeManager.Initializer();
        weaponManager.Initializer();
        clickManager.Initializer();
        fightManager.Initializer();

        AfterInit();
    }

    void AfterInit()
    {
        OnPointChanged?.Invoke();
        OnGoldChanged?.Invoke();
    }

    #region 자원

    [SerializeField] private TextMeshProUGUI PointText;
    [SerializeField] private TextMeshProUGUI GoldText;
    public event Action OnPointChanged;
    public event Action OnGoldChanged;
    void InitializeResource()
    {
        OnPointChanged += SetPoint;
        OnGoldChanged += SetGold;
    }
    void SetPoint()
    {
        PointText.text = gm.nowData.Point.ToString();
    }
    void SetGold()
    {
        GoldText.text = gm.nowData.Gold.ToString();
    }
    public void GetPoint(int amt)
    {
        gm.nowData.Point += amt;
        OnPointChanged?.Invoke();
    }
    public void UsePoint(int amt)
    {
        gm.nowData.Point -= amt;
        OnPointChanged?.Invoke();
    }
    public bool HasPoint(int amt)
    {
        if (gm.nowData.Point >= amt) return true;
        else return false;
    }
    public void GetGold(int amt)
    {
        gm.nowData.Gold += amt;
        OnGoldChanged?.Invoke();
    }
    public void UseGold(int amt)
    {
        gm.nowData.Gold -= amt;
        OnGoldChanged?.Invoke();
    }
    public bool HasGold(int amt)
    {
        if (gm.nowData.Gold >= amt) return true;
        else return false;
    }

    #endregion

    public UpgradeManager upgradeManager;
    public WeaponManager weaponManager;
    public ClickManager clickManager;
    public FightManager fightManager;
}
