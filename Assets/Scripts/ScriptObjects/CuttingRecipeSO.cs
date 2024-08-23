using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CuttingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingCountMax;
}

[CreateAssetMenu()]
public class CuttingRecipeListSO : ScriptableObject
{
    public  List<CuttingRecipe> list;

    /// <summary>
    /// �и�ʳ������кõĶ������û�У��򷵻ؿ�
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public KitchenObjectSO GetOutput(KitchenObjectSO input)
    {
        foreach (CuttingRecipe c in list)
        {
            if(c.input == input)
            {
                return c.output;
            }
        }
        return null;
    }

    public bool TryGetCuttingRecipe(KitchenObjectSO input,out CuttingRecipe cuttingRecipe)
    {
        foreach (CuttingRecipe c in list)
        {
            if (c.input == input)
            {
                cuttingRecipe= c;
                return true;
            }
        }
        cuttingRecipe = null;
        return false;
    }


}
