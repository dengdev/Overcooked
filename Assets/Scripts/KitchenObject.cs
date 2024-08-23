using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kichenObjectSO;

    //获取食材的数据对象
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kichenObjectSO;
    }
}
