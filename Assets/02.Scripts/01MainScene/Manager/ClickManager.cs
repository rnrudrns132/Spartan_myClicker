using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    GameManager gm => GameManager.gm;
    MainSceneManager msm => MainSceneManager.msm;

    [SerializeField] private ParticleSystem NormalEffect;
    [SerializeField] private ParticleSystem CriticalEffect;

    public void Initializer()
    {
        msm.weaponManager.OnNowWeaponChanged += SetStat;
        msm.upgradeManager.OnUpgrade += SetStat;

        SetStat();
        InitializeDamageAlert();
    }

    void SetStat()
    {
        WeaponSO targetWeapon = gm.weaponSOs[gm.nowData.nowWeaponIndex];
        int targetWeaponUpg = gm.nowData.WeaponDatas[targetWeapon.myIndex];

        atkDamage = targetWeapon.ReturnAtk(targetWeaponUpg);
        criticalProb = targetWeapon.ReturnCriticalProb(targetWeaponUpg) * 0.01f;

        float autoAtkValue = gm.upgradeSOs[1].ReturnValue(gm.nowData.UpgradeLvs[1]);
        if (autoAtkValue <= 0) autoAttackDelay = float.MaxValue;
        else autoAttackDelay = 1 / autoAtkValue;

        criticalDamage = 1.5f + (gm.upgradeSOs[0].ReturnValue(gm.nowData.UpgradeLvs[0]) * 0.01f);
        goldEarn = (int)(5 * (1 + gm.upgradeSOs[2].ReturnValue(gm.nowData.UpgradeLvs[2]) * 0.01f));
    }

    int atkDamage;
    float criticalProb;
    float criticalDamage;

    float autoAttackDelay;

    int goldEarn;

    public void OnClick()
    {
        if (msm.fightManager.nowEnemy == null) return;

        float damage = atkDamage;
        ParticleSystem effect;
        bool isCritical;
        if (Random.Range(0,1f) < criticalProb)
        {
            damage *= criticalDamage;
            gm.PlaySFX(SFXEnum.CRITICAL);
            effect = CriticalEffect;
            isCritical = true;
        }
        else
        {
            gm.PlaySFX(SFXEnum.ATTACK);
            effect = NormalEffect;
            isCritical = false;
        }

        msm.fightManager.nowEnemy.TakeDamage(damage);
        msm.GetGold(goldEarn);
        ShowDamageAlert(damage, isCritical);

        effect.transform.position = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        effect.Play();
    }

    float nowAutoAtkWait;
    private void Update()
    {
        if (autoAttackDelay < float.MaxValue - 1) nowAutoAtkWait += Time.deltaTime;

        if (autoAttackDelay <= nowAutoAtkWait)
        {
            OnClick();
            nowAutoAtkWait -= autoAttackDelay;
        }
    }

    public Transform DamageAlertParent;
    public Queue<DamageAlert> DamageAlertQueue;
    void InitializeDamageAlert()
    {
        DamageAlertQueue = new Queue<DamageAlert>();
        for (int i = 0; i < DamageAlertParent.childCount; i++)
        {
            DamageAlertQueue.Enqueue(DamageAlertParent.GetChild(i).GetComponent<DamageAlert>());
        }
    }
    void ShowDamageAlert(float damage, bool isCritical)
    {
        DamageAlert now = DamageAlertQueue.Dequeue();
        now.ShowNum((int)damage, isCritical);
        DamageAlertQueue.Enqueue(now);
    }
}
