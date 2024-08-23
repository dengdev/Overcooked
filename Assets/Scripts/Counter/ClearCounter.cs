using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    /// <summary>
    /// ���̨����
    /// </summary>
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {//�����ж���

            if (player.GetKitchenObject().TryGetComponent(out PlateKitchenObject plateKitchenObject))
            {//������
                if (IsHaveKitchenObject() == false)
                {//��ǰ��̨Ϊ��,����������
                    TransferKitchenObject(player, this);
                }
                else
                {//��ǰ��̨��ʳ��,��ʳ�ķŵ�������
                    bool isSuccess = plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO());
                    if (isSuccess) { DestoryKitchenObject(); }
                }
            }
            else
            {//��������ͨʳ��
                if (IsHaveKitchenObject() == false)
                {//��̨Ϊ�գ�ʳ�Ĵ��ֵ���̨
                    TransferKitchenObject(player, this);
                }
                else
                {//��ǰ��̨��Ϊ��
                    if(GetKitchenObject().TryGetComponent(out PlateKitchenObject plateKitchenObject1))
                    {//�����̨��������
                        if (plateKitchenObject1.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            player.DestoryKitchenObject();
                        }
                    }else Debug.Log("̨�����Ѿ���ʳ����");
                }
            }
            

        }else
        {//����ûʳ��
            if (IsHaveKitchenObject())
            {
                if (IsHaveKitchenObject() == false)
                { Debug.Log("��̨��û��ʳ��");}
                else
                {//��ǰ��̨��Ϊ��
                    TransferKitchenObject(this, player);
                }
            }
        }
        
    }

    public override void InteractOperate(Player player)
    {
        base.InteractOperate(player);
    }
}
