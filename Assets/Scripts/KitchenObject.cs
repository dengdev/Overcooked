using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kichenObjectSO;

    //��ȡʳ�ĵ����ݶ���
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kichenObjectSO;
    }
}
