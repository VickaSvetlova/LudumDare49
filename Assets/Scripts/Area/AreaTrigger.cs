using System.Collections;
using UnityEngine;

public class AreaTrigger : MonoBehaviour {
    
    [SerializeField] private Area areaIn;
    [SerializeField] private Area areaOut;

    private void OnTriggerExit(Collider other) {
        if (other.tag != TagList.Player) return;
        if (transform.InverseTransformPoint(other.transform.position).z > 0) {
            Area.Change(areaIn);
        } else {
            Area.Change(areaOut);
        }
    }

}