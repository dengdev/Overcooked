using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    public static event EventHandler OnDrop;
    public static event EventHandler OnPickup;

    [SerializeField] private Transform holdPoint;

    private KitchenObject kitchenObject;

    /// <summary>
    /// �õ���̨�ϵ�ʳ��
    /// </summary>
    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObject.GetKitchenObjectSO(); }

    /// <summary>
    /// �ж���ҵ�ǰ�Ƿ����ʳ��
    /// </summary>
    public bool IsHaveKitchenObject() { return kitchenObject != null;}


    /// <summary>
    /// ÿ������ʳ��ʵ�������λ��
    /// </summary>
    public void SetKitchenObject(KitchenObject kitchenObject) { 
        if(kitchenObject != null&&this.kitchenObject!=kitchenObject&&this is BaseCounter )
        {//��ʳ����ڹ�̨�ϣ���������
            OnDrop?.Invoke(this, EventArgs.Empty);
        }else if(kitchenObject != null && this.kitchenObject != kitchenObject && this is Player)
        {//����ʳ��
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
        this.kitchenObject = kitchenObject;
        kitchenObject.transform.localPosition = Vector3.zero;

    }
    public Transform GetHoldPoint() { return holdPoint; }
    /// <summary>
    /// ת��ʳ�ĵ�λ��
    /// </summary>
    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (sourceHolder.GetKitchenObject() == null)
        {
            Debug.LogWarning("ԭ�������ϲ�����ʳ�ģ�ת��ʧ��");
            return;
        }
        if (targetHolder.GetKitchenObject() != null)
        {
            Debug.LogWarning("Ŀ��������ϴ���ʳ�ģ�ת��ʧ��");
            return;
        }
        targetHolder.AddKitchenObject(sourceHolder.GetKitchenObject());
        sourceHolder.ClearKitchenObject();
    }

    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject.transform.SetParent(holdPoint);
        SetKitchenObject(kitchenObject);
    }

    


    /// <summary>
    /// ��Ʒת��֮���������
    /// </summary>
    public void ClearKitchenObject() { this.kitchenObject = null; }


    /// <summary>
    /// ������Ʒ���������
    /// </summary>
    public void DestoryKitchenObject()
    {
        Destroy(kitchenObject.gameObject);
        ClearKitchenObject();

    }
    /// <summary>
    /// ʵ����ʳ��
    /// </summary>
    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }


}
