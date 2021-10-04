using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helicopter : MonoBehaviour
{


    [SerializeField] private Transform rotator;

    public void Play() {
        GetComponent<Animation>().Play();
    }


    public void Finish() {
        GameManager.main.LoadMainMenu();
    }

    void FixedUpdate()
    {
        rotator.rotation *= Quaternion.AngleAxis(500f * Time.fixedDeltaTime, Vector3.up);
    }
}
