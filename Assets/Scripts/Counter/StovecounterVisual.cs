using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject sizzlingParticles;
    [SerializeField] private GameObject stoveOnVisual;

    public void SetActiveParticles()
    {
        sizzlingParticles.SetActive(true);
        stoveOnVisual.SetActive(true);
    }

    public void HideParticles()
    {
        sizzlingParticles.SetActive(false); 
        stoveOnVisual.SetActive(false);
    }

}
