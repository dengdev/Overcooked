using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{
    [SerializeField] private Transform recipeParent;
    [SerializeField] private RecipeUI recipeUITemplate;//����ʵ����������Ҫ��ʾ
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
    /// �¼�����ʱ����UI�ĸ���
    /// </summary>
    private void OrderManger_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    /// <summary>
    /// ������ʳ�׵�UI���ٵ�
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

        //�õ��Ѿ����ɵĶ���
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
