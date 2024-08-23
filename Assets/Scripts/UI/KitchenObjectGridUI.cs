using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectGridUI : MonoBehaviour
{

    [SerializeField] private KitchenObjectIconUI iconTemplate;

    private void Start()
    {
        iconTemplate.Hide();

    }

    /// <summary>
    /// ÏÔÊ¾Ê³²ÄµÄUI
    /// </summary>
    /// <param name="kitchenObjectSO"></param>
    public void ShowKitchenObjectUI(KitchenObjectSO kitchenObjectSO)
    {
        KitchenObjectIconUI newIconUI= GameObject.Instantiate(iconTemplate,transform);
        newIconUI.Show(kitchenObjectSO.sprite);

    }
}
