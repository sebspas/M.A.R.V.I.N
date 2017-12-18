using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public enum PlayerPhase
    {
        Spawn,
        Ice,
        Desert,
        Forest,
        Final
    }

    public static PlayerPhase playerPhase;

    private void Awake()
    {
        playerPhase = PlayerPhase.Spawn;
    }

    public static void NextPhase()
    {
        switch (playerPhase)
        {
            case PlayerPhase.Spawn:
                playerPhase = PlayerPhase.Ice;
                break;
            case PlayerPhase.Ice:
                playerPhase = PlayerPhase.Desert;
                break;
            case PlayerPhase.Desert:
                playerPhase = PlayerPhase.Forest;
                break;
            case PlayerPhase.Forest:
                playerPhase = PlayerPhase.Final;
                break;
            case PlayerPhase.Final:
                break;
            default:
                break;
        }
    }

    public void CallStop()
    {
        Invoke("StopGame", 2.5f);
    }

    public void StopGame()
    {
        // we stop the game
        UIManager.isPaused = true;
        Time.timeScale = 0;
    }
}
