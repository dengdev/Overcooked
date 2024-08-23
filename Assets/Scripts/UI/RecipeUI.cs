using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipenameText;
    [SerializeField] private Transform kitchenobjectParent;
    [SerializeField] private Image iconUITemplate;


    private void Start()
    {
        iconUITemplate.gameObject.SetActive(false);
    }
    public void UpdateUI(RecipeSO recipeSO)
    {
        recipenameText.text=recipeSO.recipeName;    
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
             Image newIcon= GameObject.Instantiate(iconUITemplate);
            newIcon.transform.SetParent(kitchenobjectParent);
            newIcon.sprite = kitchenObjectSO.sprite;
            newIcon.gameObject.SetActive(true);
        }
    }
}
