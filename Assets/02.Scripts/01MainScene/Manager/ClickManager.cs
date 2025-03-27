using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    GameManager gm => GameManager.gm;
    MainSceneManager msm => MainSceneManager.msm;
    PlayerStat playerStat => gm.playerStat;

    [SerializeField] private ParticleSystem NormalEffect;
    [SerializeField] private ParticleSystem CriticalEffect;

    public void Initializer()
    {
        InitializeDamageAlert();
    }


    public void OnClick()
    {
        if (msm.fightManager.nowEnemy == null) return;

        float damage = playerStat.atkDamage;
        ParticleSystem effect;
        bool isCritical;
        if (Random.Range(0,1f) < playerStat.criticalProb)
        {
            damage *= playerStat.criticalDamage;
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
        msm.GetGold(playerStat.goldEarn);
        ShowDamageAlert(damage, isCritical);

        effect.transform.position = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        effect.Play();
    }

    float nowAutoAtkWait;
    private void Update()
    {
        if (playerStat.autoAttackDelay < float.MaxValue - 1) nowAutoAtkWait += Time.deltaTime;

        if (playerStat.autoAttackDelay <= nowAutoAtkWait)
        {
            OnClick();
            nowAutoAtkWait -= playerStat.autoAttackDelay;
        }
    }

    [SerializeField] private Transform DamageAlertParent;
    private Queue<DamageAlert> DamageAlertQueue;
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
