using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        myAnim.SetTrigger("Attacked");

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
        myAnim.SetTrigger("Dead");

        nowHP = 0;
        OnDead?.Invoke();
    }

    public void DeadEnd()
    {
        GameManager.gm.nowData.nowEnemyCnt++;
        if (GameManager.gm.nowData.nowEnemyCnt > 10)
        {
            GameManager.gm.nowData.nowEnemyCnt = 0;
            GameManager.gm.nowData.nowStage++;
            GameManager.gm.SaveGame();
        }
        MainSceneManager.msm.fightManager.SpawnEnemy();
        Destroy(gameObject);
    }
}
