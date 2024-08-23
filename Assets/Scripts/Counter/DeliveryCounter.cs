using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if(player.IsHaveKitchenObject()&&player.GetKitchenObject().
            TryGetComponent(out PlateKitchenObject plateKitchenObject))
        {
           
            OrderManger.Instance.DeliveryRecipe(plateKitchenObject);
            player.DestoryKitchenObject();
        }

    }

}
