using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    //״̬�л��͵��ø��¼�
    public event EventHandler OnStateChanged;

    private enum State {
        WaitToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private Player player;
    private State state;

    /*�ȴ���ʼʱ�� */
    private float waitingToStarTimer = 1;
    /*�࿪ʼ����ʱ */
    private float countDownToStartTimer = 3;
    /*��Ϸ����ʱ�� */
    private float gamePlayingTimer = 20;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        TurnToWaitngToStart();
    }

    private void Update(){
        switch (state){
            case State.WaitToStart:
                waitingToStarTimer -= Time.deltaTime;
                if (waitingToStarTimer <= 0){
                    TurnToCountDownToStart();
                }
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0){
                    TurnToGamePlaying();
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0){
                    TurnToGameOver();
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }

    private void TurnToWaitngToStart(){
        state = State.WaitToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToCountDownToStart(){
        state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGamePlaying(){
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGameOver(){
        state = State.GameOver;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DisablePlayer(){
        player.enabled= false;
    }
    private void EnablePlayer() {
        player.enabled= true;
    }
    
    /// <summary>
    /// �жϵ�ǰ״̬�Ƿ��ǵ���ʱ״̬
    /// </summary>
    public bool IsCountDownState(){
        return state==State.CountDownToStart;
    }

   /// <summary>
   /// �жϵ�ǰ״̬�Ƿ��ǿ�ʼ��Ϸ״̬
   /// </summary>
    public bool IsGamePlayingState() {
        return state==State.GamePlaying ;
    }

    public bool IsGameOverState(){
        return state==State.GameOver ;
    }

    /// <summary>
    /// ��ȡ����ʱ��ʱ��
    /// </summary>
    public float GetCountDownTimer(){
        return countDownToStartTimer;
    }

}
