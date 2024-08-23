using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//≤÷ø‚¿‡πÒÃ®
public class ContainCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private ContainCounterVisual containCounterVisual;
   

    public override void Interact(Player player)
    {

        if (player.IsHaveKitchenObject()) { return; }

        CreateKitchenObject(kitchenObjectSO.prefab);

        TransferKitchenObject(this, player);
        containCounterVisual.PlayOpen();

    }
    

}
