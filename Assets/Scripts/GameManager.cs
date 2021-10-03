using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager main;
    private int levelIndex;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerController playerPrefab;

    private void Awake() {
        main = this;
    }

    private void Start() {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void Restart() {
        
    }

}
