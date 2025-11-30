using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            Time.timeScale = 2.0f;

    }
#endif
    public void Victory()
    {
        Debug.Log("게임 승리!");
        EventManager.Instance.Trigger(EventType.GameWin, null);
    }
    public void GameOver()
    {
        
        Debug.Log("게임 패배");
        EventManager.Instance.Trigger(EventType.GameOver, null);
    }
}
