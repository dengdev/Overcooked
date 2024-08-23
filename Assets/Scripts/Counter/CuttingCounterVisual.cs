using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator animator;
    private const string CUT = "Cut";
     void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayCut()
    {
        animator.SetTrigger(CUT);
    }
}
