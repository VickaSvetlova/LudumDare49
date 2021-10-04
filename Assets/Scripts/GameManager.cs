using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager main;
    public int sceneIndex { get { return SceneManager.GetActiveScene().buildIndex; } } 
    [SerializeField] private int nextSceneIndex { get { return sceneIndex + 1;  } }
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerController playerPrefab;
    public bool isLoadingNewScene { get; private set; }

    private void Awake() {
        main = this;
    }

    private void Start() {
        if (sceneIndex > 0) Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        UIController.main.ShowInventory(sceneIndex > 0);
    }

    public void Restart() {
        if (isLoadingNewScene) return;
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    [ContextMenu("LoadNext")]
    public void LoadNextScene() {
        isLoadingNewScene = true;
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }

    public void LoadMainMenu() {
        isLoadingNewScene = true;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Exit() {
        Application.Quit();
    }

}
