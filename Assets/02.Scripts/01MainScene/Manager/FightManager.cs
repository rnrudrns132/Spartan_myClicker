using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightManager : MonoBehaviour
{
    GameManager gm => GameManager.gm;
    MainSceneManager msm => MainSceneManager.msm;

    public void Initializer()
    {
        SpawnEnemy();
    }

    public Enemy nowEnemy;
    public void SpawnEnemy()
    {
        EnemySO nowTarget = gm.enemySOs[gm.nowData.nowStage % gm.enemySOs.Length];
        int enemyLv = gm.nowData.nowStage / gm.enemySOs.Length;

        nowEnemy = Instantiate(nowTarget.myPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity).GetComponent<Enemy>();
        nowEnemy.Initializer(nowTarget, enemyLv);
        nowEnemy.OnTakeDamage += SetHpBar;
        nowEnemy.OnDead += SetNowEnemyDead;

        StageNameText.text = $"{nowTarget.ReturnName(enemyLv)}의 숲";
        NowEnemyCntText.text = $"{gm.nowData.nowEnemyCnt}/10";
        NowEnemyNameText.text = nowTarget.ReturnName(enemyLv);
        SetHpBar();
    }
    
    [SerializeField] private TextMeshProUGUI StageNameText;
    [SerializeField] private TextMeshProUGUI NowEnemyCntText;
    [SerializeField] private TextMeshProUGUI NowEnemyNameText;
    [SerializeField] private Image hpGauge;
    void SetHpBar()
    {
        hpGauge.fillAmount = nowEnemy.nowHP / nowEnemy.MaxHP;
    }

    void SetNowEnemyDead()
    {
        msm.GetPoint(nowEnemy.mySO.ReturnPoint(nowEnemy.myLv));
        msm.GetGold(nowEnemy.mySO.ReturnGold(nowEnemy.myLv));

        nowEnemy = null;
        NowEnemyNameText.text = "";
    }
}
