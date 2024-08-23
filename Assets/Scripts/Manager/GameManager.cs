using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    //状态切换就调用该事件
    public event EventHandler OnStateChanged;

    private enum State {
        WaitToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private Player player;
    private State state;

    /*等待开始时间 */
    private float waitingToStarTimer = 1;
    /*距开始倒计时 */
    private float countDownToStartTimer = 3;
    /*游戏操作时间 */
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
    /// 判断当前状态是否是倒计时状态
    /// </summary>
    public bool IsCountDownState(){
        return state==State.CountDownToStart;
    }

   /// <summary>
   /// 判断当前状态是否是开始游戏状态
   /// </summary>
    public bool IsGamePlayingState() {
        return state==State.GamePlaying ;
    }

    public bool IsGameOverState(){
        return state==State.GameOver ;
    }

    /// <summary>
    /// 获取倒计时的时间
    /// </summary>
    public float GetCountDownTimer(){
        return countDownToStartTimer;
    }

}
