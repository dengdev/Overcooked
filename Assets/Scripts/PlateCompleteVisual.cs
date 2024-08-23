using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public class KitchenObjectSO_Modle
    {
        public KitchenObjectSO ModleSO;
        public GameObject obj;
    }
    [SerializeField] public List<KitchenObjectSO_Modle> modelMap;

    /// <summary>
    /// 显示食材的模型
    /// </summary>
    public void ShowKitchenObject(KitchenObjectSO obj)
    {
        foreach (var item in modelMap)
        {
            if (item.ModleSO == obj)
            {
                item.obj.SetActive(true);
                return;
            }
        }
    }



}
