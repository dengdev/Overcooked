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
    /// 得到柜台上的食材
    /// </summary>
    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObject.GetKitchenObjectSO(); }

    /// <summary>
    /// 判断玩家当前是否持有食材
    /// </summary>
    public bool IsHaveKitchenObject() { return kitchenObject != null;}


    /// <summary>
    /// 每次设置食材实例化后的位置
    /// </summary>
    public void SetKitchenObject(KitchenObject kitchenObject) { 
        if(kitchenObject != null&&this.kitchenObject!=kitchenObject&&this is BaseCounter )
        {//把食物放在柜台上，播放声音
            OnDrop?.Invoke(this, EventArgs.Empty);
        }else if(kitchenObject != null && this.kitchenObject != kitchenObject && this is Player)
        {//拿起食物
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
        this.kitchenObject = kitchenObject;
        kitchenObject.transform.localPosition = Vector3.zero;

    }
    public Transform GetHoldPoint() { return holdPoint; }
    /// <summary>
    /// 转移食材的位置
    /// </summary>
    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (sourceHolder.GetKitchenObject() == null)
        {
            Debug.LogWarning("原持有者上不存在食材，转移失败");
            return;
        }
        if (targetHolder.GetKitchenObject() != null)
        {
            Debug.LogWarning("目标持有者上存在食材，转移失败");
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
    /// 物品转移之后清除引用
    /// </summary>
    public void ClearKitchenObject() { this.kitchenObject = null; }


    /// <summary>
    /// 销毁物品再清除引用
    /// </summary>
    public void DestoryKitchenObject()
    {
        Destroy(kitchenObject.gameObject);
        ClearKitchenObject();

    }
    /// <summary>
    /// 实例化食材
    /// </summary>
    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }


}
