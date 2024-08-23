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

    //�����������ʳ��
    public bool AddKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            Debug.Log("�Ѿ���ӹ���ʳ����");
            return false;}
        if(mSO_List.Contains(kitchenObjectSO)==false)
        {
            Debug.Log("��ʳ�Ĳ��ܷŵ�������");
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
