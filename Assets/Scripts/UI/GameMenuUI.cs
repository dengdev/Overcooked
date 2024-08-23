using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;


    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        quitButton.onClick.AddListener(() =>
        {//只在发布程序中有效
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
