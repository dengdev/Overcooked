using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Plantscounter : BaseCounter
{

    
    [SerializeField] private KitchenObjectSO plateSO;
    [SerializeField] private float spawnrate = 3;//3������һ������
    [SerializeField] private int plateMax = 5;

    private List<KitchenObject> plateList = new();
    private float timer = 0;

    private void Update()
    {
        if (plateList.Count < plateMax)
        {
            timer += Time.deltaTime;
            if ( timer > spawnrate)
            {
                SpawnPlate();
            }
        }
    }
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {//������ʳ�ģ���ʳ�ĵ���̨
             Debug.Log("�����ж���������������");
        }
        else
        {//����ûʳ��
            if(plateList.Count > 0)
            {
                player.AddKitchenObject(plateList[plateList.Count-1]);
                plateList.RemoveAt(plateList.Count-1);
            }
        }
    }


    public void SpawnPlate()
    {
        timer = 0;
        KitchenObject plateKitchenObject = GameObject.Instantiate(plateSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
        plateKitchenObject.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * plateList.Count;
        plateList.Add(plateKitchenObject);
    }
}
