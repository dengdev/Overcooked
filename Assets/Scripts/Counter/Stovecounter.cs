using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stovecounter : BaseCounter
{
    [SerializeField] private FryingRecipeListSO fryingRecipeList;
    [SerializeField] private FryingRecipeListSO burningRecipeList;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    [SerializeField] private ProgressBarUI progressBar;
    [SerializeField] private AudioSource sound;


    public enum StoveState{
        Idle,Frying,Burning
    }

    private FryingRecipe fryingRecipe;
    private  float fryingTimer=0;
    private StoveState state = StoveState.Idle;
    public override void Interact(Player player){
        if (player.IsHaveKitchenObject()){//手上有食材，放食材到柜台
            if (IsHaveKitchenObject() == false){//当前柜台为空
                if (fryingRecipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe fryingRecipe1)){
                    TransferKitchenObject(player, this);
                    StartFrying(fryingRecipe1);
                }
                else if (burningRecipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe burningRecipe1)){
                    TransferKitchenObject(player, this);
                    StartBurning(burningRecipe1);
                }
                else Debug.Log("该食材不能进行烹饪");
            }
            else Debug.Log("台面上没有位置了");
        }
        else{//手上没食材，从柜台拿食材
            if (IsHaveKitchenObject()){
                if (IsHaveKitchenObject() == false) Debug.Log("台面上是空的");
                else{//柜台有东西
                    TurnToIdle();
                    TransferKitchenObject(this, player);
                }
            }
        }    
    }

    private void Update(){
        switch (state){
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTimer += Time.deltaTime;
                progressBar.UpdateProgress(fryingTimer/ fryingRecipe.fryingTime);
                if (fryingTimer >= fryingRecipe.fryingTime){
                    DestoryKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    
                    burningRecipeList.TryGetFryingRecipe(GetKitchenObject().GetKitchenObjectSO(),
                        out FryingRecipe newfryingRecipe);
                    StartBurning(newfryingRecipe);
                }
                break;
            case StoveState.Burning:
                fryingTimer += Time.deltaTime;
                progressBar.UpdateProgress(fryingTimer / fryingRecipe.fryingTime);
                if (fryingTimer >= fryingRecipe.fryingTime){
                    DestoryKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    TurnToIdle();
                }
                    break;
            default:
                break;
        }
    }

    /// <summary>
    /// 开始煎肉
    /// </summary>
    private void StartFrying(FryingRecipe fryingRecipe){
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Frying;
        progressBar.Show();
        stoveCounterVisual.SetActiveParticles();
        sound.Play();  
    }

    private void StartBurning(FryingRecipe fryingRecipe){
        if(fryingRecipe == null){
            Debug.LogWarning("无法获取Burning的食谱，无法进行Burning");
            TurnToIdle();
            return;
        }
        progressBar.Show();
        stoveCounterVisual.SetActiveParticles();
        fryingTimer = 0;
        this.fryingRecipe= fryingRecipe;
        state = StoveState.Burning;
        sound.Play();
    }

    private void TurnToIdle(){
        state = StoveState.Idle;
        stoveCounterVisual.HideParticles();
        progressBar.Hide();
        sound.Pause();
    }
}
