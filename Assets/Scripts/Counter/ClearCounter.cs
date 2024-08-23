using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    /// <summary>
    /// 与柜台交互
    /// </summary>
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {//手上有东西

            if (player.GetKitchenObject().TryGetComponent(out PlateKitchenObject plateKitchenObject))
            {//是盘子
                if (IsHaveKitchenObject() == false)
                {//当前柜台为空,手上有盘子
                    TransferKitchenObject(player, this);
                }
                else
                {//当前柜台有食材,把食材放到盘子里
                    bool isSuccess = plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO());
                    if (isSuccess) { DestoryKitchenObject(); }
                }
            }
            else
            {//手上是普通食材
                if (IsHaveKitchenObject() == false)
                {//柜台为空，食材从手到柜台
                    TransferKitchenObject(player, this);
                }
                else
                {//当前柜台不为空
                    if(GetKitchenObject().TryGetComponent(out PlateKitchenObject plateKitchenObject1))
                    {//如果柜台上是盘子
                        if (plateKitchenObject1.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            player.DestoryKitchenObject();
                        }
                    }else Debug.Log("台子上已经有食物了");
                }
            }
            

        }else
        {//手上没食材
            if (IsHaveKitchenObject())
            {
                if (IsHaveKitchenObject() == false)
                { Debug.Log("柜台上没有食材");}
                else
                {//当前柜台不为空
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
