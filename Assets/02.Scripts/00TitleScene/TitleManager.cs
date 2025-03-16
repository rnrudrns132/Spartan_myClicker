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
        gm.nowData = new GameData();
        gm.SaveGame();
        gm.MoveScene(1);
        gm.PlaySFX(SFXEnum.UI_CLICK);
    }
    public void LoadAndStartGame()
    {
        if (gm.LoadGame())
        {
            gm.MoveScene(1);
            gm.PlaySFX(SFXEnum.UI_CLICK);
        }
        else gm.ShowAlert("저장된 데이터가 없습니다");
    }
}
