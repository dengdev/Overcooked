using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{
    [SerializeField] private Transform recipeParent;
    [SerializeField] private RecipeUI recipeUITemplate;//用于实例化，不需要显示
    private void Start()
    {

        recipeUITemplate.gameObject.SetActive(false);
        OrderManger.Instance.OnRecipeSpawned += OrderManger_OnRecipeSpawned;
        OrderManger.Instance.OnRecipeCompleted += OrderManger_OnRecipeCompleted;
    }

    private void OrderManger_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    /// <summary>
    /// 事件触发时进行UI的更新
    /// </summary>
    private void OrderManger_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    /// <summary>
    /// 把所有食谱的UI销毁掉
    /// </summary>
    private void UpdateUI()
    {
        foreach(Transform child in recipeParent)
        {
            if(child != recipeUITemplate.transform)
            {
                Destroy(child.gameObject);
            }
        }

        //得到已经生成的订单
        List<RecipeSO>recipeSOList=  OrderManger.Instance.GetOrderList();

        foreach(RecipeSO recipeSO in recipeSOList)
        {
            RecipeUI recipeUI = GameObject.Instantiate(recipeUITemplate);
            recipeUI.transform.SetParent(  recipeParent);
            recipeUI.gameObject.SetActive(true);
            recipeUI.UpdateUI(recipeSO);
        }
    }
}
