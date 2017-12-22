using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int level;
    private enum GameStateEn
    {
        WaitingForPlayers,
        Starting,
        Playing,
        Lost,
        Win
    };

    private GameStateEn _state;
    private KinectManager manager;
    private bool winCondition;

    public int maxTime;
    public int maxLife;

    public int P1Life;
    private float P1TimeLeft;

    public int P2Life;
    private float P2TimeLeft;

    // Use this for initialization
    public GameController getInstance()
    {
        return this;
    }
	void Start () {
        manager = KinectManager.Instance;
        level = 0;
        _state = GameStateEn.WaitingForPlayers;
	}
	
	// Update is called once per frame
	void Update () {
		switch(_state)
        {
            case GameStateEn.WaitingForPlayers:
                WaitForPlayers();
                break;
            case GameStateEn.Starting:
                StartGame();
                break;
            case GameStateEn.Playing:
                Play();
                break;
            case GameStateEn.Win:
                OnWin();
                break;
            case GameStateEn.Lost:
                OnLost();
                break;
        }
	}

    private void OnLost()
    {
        throw new NotImplementedException();
    }

    private void OnWin()
    {
        throw new NotImplementedException();
    }

    private void Play()
    {
          while (P1Life > 0 && P2Life > 0)
        {
            if (P1TimeLeft > 0)
            {
                P1TimeLeft -= Time.deltaTime;
                
            } else
            {
                P1Life--;
            }

            if (P2TimeLeft > 0)
            {
                P2TimeLeft -= Time.deltaTime;
            }
            else
            {
                P2Life--;
            }




        }

          if (winCondition)
        {
            _state = GameStateEn.Win;
        } else
        {
            _state = GameStateEn.Lost;
        }
    }

    private void StartGame()
    {
        Debug.Log("Starting Game");
        LoadLevel(level);
        P1TimeLeft = maxTime;
        P2TimeLeft = maxTime;

        P1Life = maxLife;
        P2Life = maxLife;
        _state = GameStateEn.Playing;
    }

    private void LoadLevel(int level)
    {
        Debug.Log("Loading Level");
        return;
    }

    private void WaitForPlayers()
    {
        uint P1ID = manager.GetPlayer1ID();
        uint P2ID = manager.GetPlayer2ID();

        if (P1ID != 0 && P2ID != 0)
        {
            _state = GameStateEn.Starting;
        } else
        {
            Debug.Log("Waiting for Players");
        }
    }
}
