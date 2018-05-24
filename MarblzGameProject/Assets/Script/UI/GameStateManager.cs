using System.Collections;
using System;
using UnityEngine;

public class GameStateManager : SA.Common.Pattern.Singleton<GameStateManager> {


    public static Action OnGameStarted = delegate { };
    public static Action OnGamePaused = delegate { };
    public static Action OnGameResumed = delegate { };
    public static Action OnGameRestarted = delegate { };
    public static Action OnGameOver = delegate { };

    private GameState _gameState;

    

    /// <summary>
    /// Get/Set
    /// </summary>
    public GameState State
    {
        get
        {
            return _gameState;
        }
        set
        {
            Debug.Log("Set new state: " + value);
            _gameState = value;
        }
    }

    //--------------------------------------
    //Built-in UNITY functions
    //--------------------------------------
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    ////
    // public functions
    ////
    public void Init()
    {

    }


    public void StartGame()
    {
        State = GameState.Live;
        Debug.Log("NEWGAMESTATE" + _gameState);
        if (OnGameStarted != null)
           OnGameStarted();
    }
   

    public void PauseGame()
    {

        State = GameState.Pause;
        Debug.Log("NEWGAMESTATE"+_gameState);
       /* if (OnGamePaused != null)
            OnGamePaused();*/
    }

    public void ResumeGame()
    {
        State = GameState.Live;
        Debug.Log("NEWGAMESTATE" + _gameState);
        if (OnGameResumed != null)
           OnGameResumed();
    }

    public void MenuGame()
    {
        // if (_gameState == GameState.Menu)
        //      return;

        State = GameState.Menu;
        Debug.Log("NEWGAMESTATE" + _gameState);
        if (OnGameRestarted != null)
           OnGameRestarted();
    }


    public void OverGame() {
        State = GameState.GameOver;
        Debug.Log("NEWGAMESTATE" + _gameState);
        if (OnGameOver != null)
           
        OnGameOver();
    }
}
