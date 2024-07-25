using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Build_SceneManager : SingleMonoBase<Build_SceneManager>
{
    private Vector3 spawnPoint;
    private bool isMainCity;

    protected override void Awake()
    {
        if(INSTANCE != null)
        {
            Debug.LogError(this + " 이미 존재함");
        }
        INSTANCE = this;
        DontDestroyOnLoad(INSTANCE);
    }

    public void LoadScene(Define.SceneType sceneType, Vector3 spawnPoint, bool isMainCity)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        this.spawnPoint = spawnPoint;
        this.isMainCity = isMainCity;

        SceneManagerEx.Instance.LoadScene(sceneType);
        GameManager.Instance.ChangeSceneInit(sceneType);
        //SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (isMainCity)
        {
            BelleController.INSTANCE.SetSpawnPoint(spawnPoint);
        }

        else
        {
            PlayerController.INSTANCE.SetSpawnPoint(spawnPoint);
        }
    }

}
