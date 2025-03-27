using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int AttackedAnimParam = Animator.StringToHash("Attacked");
    private readonly int DeadAnimParam = Animator.StringToHash("Dead");
    
    public EnemySO mySO;
    public int myLv;
    Animator myAnim;
    
    public void Initializer(EnemySO target, int lv)
    {
        myAnim = GetComponent<Animator>();
        mySO = target;
        myLv = lv;

        MaxHP = mySO.ReturnHP(myLv);
        nowHP = MaxHP;
    }

    public float MaxHP;
    public float nowHP;

    public event Action OnTakeDamage;
    public void TakeDamage(float amt)
    {
        myAnim.SetTrigger(AttackedAnimParam);

        nowHP -= amt;
        OnTakeDamage?.Invoke();
        if (nowHP <= 0)
        {
            Dead();
        }
    }

    public event Action OnDead;
    void Dead()
    {
        myAnim.SetTrigger(DeadAnimParam);

        nowHP = 0;
        OnDead?.Invoke();
    }

    public void DeadEnd()
    {
        GameManager.gm.nowData.PlusEnemyCnt();
        GameManager.gm.SaveGame();
        MainSceneManager.msm.fightManager.SpawnEnemy();
        Destroy(gameObject);
    }
}
