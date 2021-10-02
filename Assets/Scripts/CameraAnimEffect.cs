using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimEffect : MonoBehaviour
{

    Transform cameraTransformV;

    // Start is called before the first frame update
    void Awake()
    {
        cameraTransformV = Camera.main.transform.parent;        
    }

    [ContextMenu("Anim")]
    private void Anim() {
        StopAllCoroutines();
        StartCoroutine(IEAnim(30));
    }

    IEnumerator IEAnim(float duration) {
        for (int i = 0; i < duration / 0.1f; i++) {
            yield return new WaitForSeconds(0.1f);
            cameraTransformV.localPosition = new Vector3(
                Random.Range(-0.05f, 0.05f),
                Random.Range(-0.05f, 0.05f),
                Random.Range(-0.05f, 0.05f)
            );
        }
        cameraTransformV.localPosition = Vector3.zero;
    }

}
