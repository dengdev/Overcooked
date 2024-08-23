using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeListSO : ScriptableObject
{
    public List<FryingRecipe> list;

    /// <summary>
    /// 得到煎肉的食谱
    /// </summary>
    /// <param name="input">输入食材</param>
    /// <param name="fryingRecipe">输出食材</param>
    /// <returns></returns>
    public bool TryGetFryingRecipe(KitchenObjectSO input, out FryingRecipe fryingRecipe)
    {
        foreach (FryingRecipe c in list)
        {
            if (c.input == input)
            {
                fryingRecipe = c;
                return true;
            }
        }
        fryingRecipe = null;
        return false;
    }

}



[Serializable]
public class FryingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTime;
}
