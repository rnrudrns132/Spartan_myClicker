using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    private void Awake()
    {
        if (gm != null)
        {
            Destroy(gameObject);
            return;
        }

        gm = this;
        DontDestroyOnLoad(gameObject);

        saveManager.Initiailzer();
        soundManager.Initiailzer();
    }

    #region 씬 전환

    [SerializeField] private Image FadeBlindImg;
    Coroutine FadeCoroutine;
    public int nowSceneIndex;

    public void FadeIn()
    {
        if (FadeCoroutine != null) StopCoroutine(FadeCoroutine);
        FadeCoroutine = StartCoroutine(Fade(true));
    }
    public void MoveScene(int targetSceneIndex)
    {
        if (FadeCoroutine != null) StopCoroutine(FadeCoroutine);
        FadeCoroutine = StartCoroutine(MoveSceneCo(targetSceneIndex));
    }
    IEnumerator Fade(bool nowFadeIn)
    {
        Color targetColor;
        Color delta;

        if (nowFadeIn)
        {
            targetColor = new Color(0, 0, 0, 0);
            delta = new Color(0, 0, 0, -1);
        }
        else
        {
            FadeBlindImg.gameObject.SetActive(true);
            targetColor = new Color(0, 0, 0, 1);
            delta = new Color(0, 0, 0, 1);
        }

        while(true)
        {
            FadeBlindImg.color += delta * Time.deltaTime;

            float alpha = FadeBlindImg.color.a;
            alpha = Mathf.Clamp01(alpha);

            if (Mathf.Abs(targetColor.a - alpha) < 0.001f)
            {
                FadeBlindImg.color = targetColor;
                break;
            }
            yield return null;
        }

        if (nowFadeIn) FadeBlindImg.gameObject.SetActive(false);
    }
    IEnumerator MoveSceneCo(int targetSceneIndex)
    {
        yield return StartCoroutine(Fade(false));
        SceneManager.LoadScene(targetSceneIndex);
        nowSceneIndex = targetSceneIndex;
    }

    #endregion

    #region 경고 알림

    [SerializeField] private GameObject AlertObj;
    [SerializeField] private TextMeshProUGUI AlertText;
    Coroutine AlertCoroutine;
    public void ShowAlert(string msg)
    {
        AlertText.text = msg;
        AlertObj.SetActive(true);

        PlaySFX(SFXEnum.ALERT);

        if (AlertCoroutine != null) StopCoroutine(AlertCoroutine);
        AlertCoroutine = StartCoroutine(AlertCo());
    }
    IEnumerator AlertCo()
    {
        yield return new WaitForSecondsRealtime(2);
        AlertObj.SetActive(false);
    }

    #endregion

    #region 저장

    [SerializeField] private SaveManager saveManager;
    public PlayerData nowData;
    public void SaveGame()
    {
        saveManager.SaveData(nowData);
    }
    public bool LoadGame()
    {
        if (saveManager.TryLoadData(out PlayerData data))
        {
            nowData = data;
            return true;
        }
        else return false;
    }

    #endregion

    #region 사운드

    [SerializeField] private SoundManager soundManager;
    public void PlayBGM(BGMEnum target)
    {
        soundManager.PlayBGM(target);
    }
    public void PlaySFX(SFXEnum target)
    {
        soundManager.PlaySFX(target);
    }


    #endregion

    #region 능력치

    public PlayerStat playerStat;

    public void InitStat()
    {
        playerStat = new PlayerStat();
        SetStat();
    }
    
    public void SetStat()
    {
        WeaponSO targetWeapon = weaponSOs[nowData.nowWeaponIndex];
        int targetWeaponUpg = nowData.WeaponDatas[targetWeapon.myIndex];

        playerStat.atkDamage = targetWeapon.ReturnAtk(targetWeaponUpg);
        playerStat.criticalProb = targetWeapon.ReturnCriticalProb(targetWeaponUpg) * 0.01f;

        float autoAtkValue = upgradeSOs[1].ReturnValue(nowData.UpgradeLvs[1]);
        if (autoAtkValue <= 0) playerStat.autoAttackDelay = float.MaxValue;
        else playerStat.autoAttackDelay = 1 / autoAtkValue;

        playerStat.criticalDamage = 1.5f + (upgradeSOs[0].ReturnValue(nowData.UpgradeLvs[0]) * 0.01f);
        playerStat.goldEarn = (int)(5 * (1 + upgradeSOs[2].ReturnValue(nowData.UpgradeLvs[2]) * 0.01f));
    }

    #endregion

    [SerializeField] private DataManager dataManager;
    public UpgradeSO[] upgradeSOs => dataManager.upgradeSOs;
    public WeaponSO[] weaponSOs => dataManager.weaponSOs;
    public EnemySO[] enemySOs => dataManager.enemySOs;
}

public class PlayerStat
{
    public int atkDamage;
    public float criticalProb;
    public float criticalDamage;

    public float autoAttackDelay;

    public int goldEarn;
}