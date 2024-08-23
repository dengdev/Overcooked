using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject selectCounter;

    /// <summary>
    /// ���̨����
    /// </summary>
    public virtual void Interact(Player player)
    {
        Debug.LogWarning("��������û����д");
    }


    public virtual void InteractOperate(Player player) {}

    /// <summary>
    /// ѡ�й�̨
    /// </summary>
    public void SelectCounter(){ selectCounter.SetActive(true); }

    /// <summary>
    /// ȡ��ѡ�й�̨
    /// </summary>
    public void ConcalSelect(){selectCounter.SetActive(false);}



}
