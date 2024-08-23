using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{//控制脚步声的播放
    private Player player;
    private float stepSoundRate=0.15f;
    private float stepSoundTimer =0;

    private void Start(){
        player = GetComponent<Player>();
    }

    private void Update(){
        stepSoundTimer += Time.deltaTime;
        if(stepSoundTimer > stepSoundRate) {
            stepSoundTimer = 0;
            if (player.IsWalking){
                float volum = 1f;
                SoundManager.Instance.PlayStepSound(volum);
            }
            
        }
    }

}
