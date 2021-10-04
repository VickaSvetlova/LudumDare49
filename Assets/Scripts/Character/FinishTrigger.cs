using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{

    [SerializeField] private Helicopter helicopter;

    private void OnTriggerExit(Collider other) {
        helicopter.Play();
    }

}
