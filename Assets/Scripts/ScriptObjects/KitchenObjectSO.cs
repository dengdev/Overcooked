using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//直接创建对象，并保存在本地
[CreateAssetMenu]
public class KitchenObjectSO :ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;//图标
    public string objectname;

}
