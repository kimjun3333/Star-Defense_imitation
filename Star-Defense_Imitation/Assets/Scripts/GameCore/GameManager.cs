using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public void Victory()
    {
        Debug.Log("게임 승리!");
    }
    public void GameOver()
    {
        Debug.Log("게임 패배");
    }
}
