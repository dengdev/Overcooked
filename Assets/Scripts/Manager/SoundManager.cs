using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private AudioClipRefsSO audioClipRefsSO;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {//监测事件
        OrderManger.Instance.OnRecipeCompleted += OrderManger_OnRecipeCompleted;
        OrderManger.Instance.OnRecioeFailed += OrderManger_OnRecioeFailed;
        CuttingCunter.OnCut += CuttingCunter_OnCut;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickup += KitchenObjectHolder_OnPickup;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }

    private void TrashCounter_OnObjectTrashed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.trash);
    }

    private void KitchenObjectHolder_OnPickup(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup);
    }

    private void KitchenObjectHolder_OnDrop(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectDrop);
    }

    private void CuttingCunter_OnCut(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.chop);
    }
    private void OrderManger_OnRecioeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail);
    }

    private void OrderManger_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess );
    }

    /// <summary>
    /// 控制玩家脚步声
    /// </summary>
    public void PlayStepSound(float volum = 1f)
    {
        PlaySound(audioClipRefsSO.footstep,volum);
    }

    private void PlaySound(AudioClip[] clips, float volume = 1.0f)
    {
        PlaySound(clips, Camera.main.transform.position);
    }
    private void PlaySound(AudioClip[]clips,Vector3 position, float volume = 1.0f)
    {
        int index= UnityEngine.Random.Range(0,clips.Length);
        AudioSource.PlayClipAtPoint(clips[index],position,volume);

    }


}
