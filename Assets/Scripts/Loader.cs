using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene{//枚举值与场景索引一一对应
        GameMenuScene,
        LoadingScene,
        GameScene
    }

    private static Scene targetScene;

    public static void Load(Scene target)
    {
        targetScene = target;
        SceneManager.LoadScene((int)Scene.LoadingScene);
    }


    public static void loadBack()
    {
        SceneManager.LoadScene((int)targetScene);
    }
}