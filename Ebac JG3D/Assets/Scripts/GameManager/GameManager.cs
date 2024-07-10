using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Ebac.StateMachine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player; // Referência ao GameObject do personagem

    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE,
        RUN,
        JUMP,
        STOP
    }

    public StateMachine<GameStates> stateMachine;

    private float stateChangeTimer = 0f;
    private float stateChangeInterval = 10f;
    private GameStates[] cyclicStates = new GameStates[] { GameStates.JUMP, GameStates.RUN, GameStates.STOP };
    private int currentCyclicStateIndex = 0;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.GAMEPLAY, new StateBase());
        stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameStates.WIN, new StateBase());
        stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

        // Inicialize GMStateRun com referência ao player
        GMStateRun gmStateRun = new GMStateRun();
        gmStateRun.definePersonagem(player);
        stateMachine.RegisterStates(GameStates.RUN, gmStateRun);

        stateMachine.RegisterStates(GameStates.JUMP, new GMStateJump());
        stateMachine.RegisterStates(GameStates.STOP, new GMStateStop());

        stateMachine.SwitchState(GameStates.INTRO);
    }

    private void Update()
    {
        stateChangeTimer += Time.deltaTime;
        if (stateChangeTimer >= stateChangeInterval)
        {
            stateChangeTimer = 0f;
            currentCyclicStateIndex = (currentCyclicStateIndex + 1) % cyclicStates.Length;
            stateMachine.SwitchState(cyclicStates[currentCyclicStateIndex]);
        }

        // Update the state machine
        stateMachine.Update();
    }
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = GetComponent<T>();
        else
            Destroy(gameObject);
    }
}
