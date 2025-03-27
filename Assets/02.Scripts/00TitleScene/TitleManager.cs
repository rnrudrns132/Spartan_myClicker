using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    GameManager gm => GameManager.gm;

    private void Start()
    {
        gm.FadeIn();
        gm.PlayBGM(BGMEnum.TITLE);
    }

    public void StartNewGame()
    {
        gm.nowData = new PlayerData();
        gm.SaveGame();
        StartGame();
    }
    public void LoadAndStartGame()
    {
        if (gm.LoadGame())
        {
            StartGame();
        }
        else gm.ShowAlert("저장된 데이터가 없습니다");
    }

    private void StartGame()
    {
        gm.MoveScene(1);
        gm.PlaySFX(SFXEnum.UI_CLICK);
        gm.InitStat();
    }
}
