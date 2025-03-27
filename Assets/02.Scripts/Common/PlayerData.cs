using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int Gold;
    public int Point;

    public int nowStage;
    public int nowEnemyCnt;

    public int[] UpgradeLvs;
    public Dictionary<int, int> WeaponDatas;
    public int nowWeaponIndex;

    public PlayerData()
    {
        UpgradeLvs = new int[3];
        WeaponDatas = new Dictionary<int, int>();
        WeaponDatas.Add(0, 0);
    }

    public void PlusEnemyCnt()
    {
        nowEnemyCnt++;
        if (nowEnemyCnt > 10)
        {
            nowEnemyCnt = 0;
            nowStage++;
        }
    }
}