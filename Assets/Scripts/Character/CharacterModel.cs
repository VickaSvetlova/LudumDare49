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
    public Transform torsoPoint;
    public Animator animator { get; protected set; }

    private Renderer[] renderers;
    private float prevAnimSpeed;

    void Awake() {
        animator = GetComponent<Animator>();
        renderers = GetComponentsInChildren<Renderer>();
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

    public void SetRenderType(RenderType value) {
        if (value == RenderType.None) {
            foreach (var renderer in renderers) {
                renderer.enabled = false;
            }
        } else {
            var shadowMode = ShadowCastingMode.On;
            switch (value) {
                case RenderType.Model: shadowMode = ShadowCastingMode.Off; break;
                case RenderType.Shadow: shadowMode = ShadowCastingMode.ShadowsOnly; break;
                case RenderType.ModelShadow: shadowMode = ShadowCastingMode.On; break;
            }
            foreach (var renderer in renderers) {
                renderer.enabled = true;
                renderer.shadowCastingMode = shadowMode;
            }
        }
    }
}
