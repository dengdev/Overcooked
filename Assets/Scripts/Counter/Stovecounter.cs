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
        if (player.IsHaveKitchenObject()){//������ʳ�ģ���ʳ�ĵ���̨
            if (IsHaveKitchenObject() == false){//��ǰ��̨Ϊ��
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
                else Debug.Log("��ʳ�Ĳ��ܽ������");
            }
            else Debug.Log("̨����û��λ����");
        }
        else{//����ûʳ�ģ��ӹ�̨��ʳ��
            if (IsHaveKitchenObject()){
                if (IsHaveKitchenObject() == false) Debug.Log("̨�����ǿյ�");
                else{//��̨�ж���
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
    /// ��ʼ����
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
            Debug.LogWarning("�޷���ȡBurning��ʳ�ף��޷�����Burning");
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
