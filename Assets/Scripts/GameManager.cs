using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager main;
    [SerializeField] private int sceneIndex;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerController playerPrefab;

    private void Awake() {
        main = this;
    }

    private void Start() {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void Restart() {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadRandomScene() {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadIntro() {
        SceneManager.LoadScene(sceneIndex);
    }

}
