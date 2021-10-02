using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {

    public new string name;

    protected virtual void Awake() {
        gameObject.SetLayerRecursively(LayerList.InteractiveObject);
        gameObject.SetTagRecursively(TagList.InteractiveObject);
    }

    public virtual void Use(Character character) {

    }

}
