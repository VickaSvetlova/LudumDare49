using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public enum RenderType {
    None,
    Model,
    Shadow,
    ModelShadow,
}

public class CharacterModel : MonoBehaviour {

    public Transform headPoint;
    public float headHeight = 1.7f;
    public float headHeightCrowl = 0.7f;
    public Animator animator { get; protected set; }
    
    private float prevAnimSpeed;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void ChangeHeadHeight(bool isCrowling) {
        StopAllCoroutines();
        var headPos = headPoint.localPosition;
        headPos.y = (isCrowling) ? headHeightCrowl : headHeight;
        StartCoroutine(IEChangeHeadHeight(headPos));
    }

    IEnumerator IEChangeHeadHeight(Vector3 targetPos) {
        while (headPoint.localPosition != targetPos) {
            yield return new WaitForEndOfFrame();
            headPoint.localPosition = Vector3.MoveTowards(headPoint.localPosition, targetPos, 5f * Time.deltaTime);
        }
    }

    public void AnimationPause() {
        if (animator.speed == 0) return;
        prevAnimSpeed = animator.speed;
        animator.speed = 0;
    }

    public void AnimationPlay() {
        if (animator.speed != 0) return;
        animator.speed = prevAnimSpeed;
    }

    public void SetHideForLocal(bool value) {
        gameObject.SetLayerRecursively(value ? LayerList.HideForLocalPlayer : LayerList.Player);
    }

    public void DeathAnimEvent() {
        GameManager.main.Restart();
    }

}
