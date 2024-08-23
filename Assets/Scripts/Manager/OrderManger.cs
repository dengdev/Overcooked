using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderManger : MonoBehaviour
{
    public static OrderManger Instance {  get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecioeFailed;

    [SerializeField] private RecipeListSO recipeSOList;
    [SerializeField] private int orderMaxCount = 5;
    [SerializeField] private float orderRate = 2;

    private List<RecipeSO> orderRecipeSOList = new();

    private float orderTimer = 0;
    private bool isStartOder = false;
    private int orderCount = 0;
    private int successDeliveryCount = 0;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        GameManager.Instance.OnStateChanged += Gamemanager_OnStateChanged2;
    }

    /// <summary>
    /// ��ǰ���µ�״̬�Ϳ�ʼ�µ�
    /// </summary>
    private void Gamemanager_OnStateChanged2(object sender, EventArgs e){
        if (GameManager.Instance.IsGamePlayingState()){
            StartSpawnOrder();
        }
    }

    private void Update(){
        if (isStartOder){
            OrderUpdate();
        }
    }

    private void OrderUpdate(){
        orderTimer += Time.deltaTime;
        if (orderTimer>=orderRate){
            orderTimer = 0;
            OrderANewRecipe();
        }
    }

    /// <summary>
    /// �µ�����
    /// </summary>
    private void OrderANewRecipe(){
        if (orderCount >= orderMaxCount) return;
        orderCount++;
        int index = UnityEngine.Random.Range(0, recipeSOList.recipeSOList.Count);
        orderRecipeSOList.Add(recipeSOList.recipeSOList[index]);
        OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
    }

    /// <summary>
    /// �ҵ���Ӧ��ʳ��
    /// </summary>
    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject){
        RecipeSO correctRecipe = null;
        foreach (var item in orderRecipeSOList){
           if( IsCorrect(item, plateKitchenObject)) {
                correctRecipe= item;break;
           }
        }
        if (correctRecipe == null) { 
            print("û�ж�Ӧ��ʳ�ף��ϲ�ʧ��");
            OnRecioeFailed?.Invoke(this, EventArgs.Empty);
        }
        else { 
            orderRecipeSOList.Remove(correctRecipe);
            orderCount--;//��������
            OnRecipeCompleted?.Invoke(this, EventArgs.Empty); 
            successDeliveryCount++;
            print("�ж�Ӧ��ʳ�ף��ϲ˳ɹ�");
        }
    }

    /// <summary>
    /// �ж�һ�����ף�����������ʳ���Ƿ����
    /// </summary>
    public bool IsCorrect(RecipeSO recipe, PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> list1 = recipe.kitchenObjectSOList;
        List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectList();

        if(list1.Count!=list2.Count) return false;

        foreach (var item in list1){
            if(list2.Contains(item)==false) return false;
        }
        foreach (var item in list2){
            if (list1.Contains(item) == false) return false;
        }
        return true;
    }
    
    public List<RecipeSO> GetOrderList(){
        return orderRecipeSOList;
    }

    /// <summary>
    /// �µ��б��ڿ�ʼ��Ϸ״̬�²����ɣ���ʼ���ɶ���
    /// </summary>
    public void StartSpawnOrder(){
        isStartOder = true;
    }

    /* �õ����� */
    public int GetSuccessDeliveryCount()
    {
        return successDeliveryCount;
    }

}
