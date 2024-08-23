using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject selectCounter;

    /// <summary>
    /// 与柜台交互
    /// </summary>
    public virtual void Interact(Player player)
    {
        Debug.LogWarning("交互方法没有重写");
    }


    public virtual void InteractOperate(Player player) {}

    /// <summary>
    /// 选中柜台
    /// </summary>
    public void SelectCounter(){ selectCounter.SetActive(true); }

    /// <summary>
    /// 取消选中柜台
    /// </summary>
    public void ConcalSelect(){selectCounter.SetActive(false);}



}
