using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    [SerializeField] private List<KitchenObjectSO> mSO_List ;
    [SerializeField] private PlateCompleteVisual plateCompleteVisual;

    [SerializeField] private KitchenObjectGridUI kitchenObjectGridUI;
    private List<KitchenObjectSO> kitchenObjectSOList = new();

    //往盘子上添加食材
    public bool AddKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            Debug.Log("已经添加过该食材了");
            return false;}
        if(mSO_List.Contains(kitchenObjectSO)==false)
        {
            Debug.Log("该食材不能放到盘子里");
            return false;}

        plateCompleteVisual.ShowKitchenObject(kitchenObjectSO);
        kitchenObjectGridUI.ShowKitchenObjectUI(kitchenObjectSO);
        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectList()
    {
        return kitchenObjectSOList;
    }
    
}
