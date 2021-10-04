using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager main;
    [SerializeField] private int sceneIndex;
    [SerializeField] private int nextSceneIndex;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerController playerPrefab;
    public bool isLoadingNewScene { get; private set; }

    private void Awake() {
        main = this;
    }

    private void Start() {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        UIController.main.ShowInventory(sceneIndex > 0);
    }

    public void Restart() {
        if (isLoadingNewScene) return;
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadNextScene() {
        isLoadingNewScene = true;
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }

    public void LoadIntro() {
        isLoadingNewScene = true;
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

}
