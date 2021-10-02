
using UnityEngine;

public sealed class LayerList {

    public static int Default { get; } = LayerMask.NameToLayer("Default");
    public static int TransparentFX { get; } = LayerMask.NameToLayer("TransparentFX");
    public static int IgnoreRaycast { get; } = LayerMask.NameToLayer("Ignore Raycast");
    public static int HideForLocalPlayer { get; } = LayerMask.NameToLayer("HideForLocalPlayer");
    public static int Water { get; } = LayerMask.NameToLayer("Water");
    public static int UI { get; } = LayerMask.NameToLayer("UI");
    public static int Player { get; } = LayerMask.NameToLayer("Player");
    public static int InteractiveObject { get; } = LayerMask.NameToLayer("InteractiveObject");

}


public static class LayerHelper {

    public static LayerMask GetMask(int[] layers) {
        var mask = new LayerMask();
        foreach (var layer in layers) {
            mask += (1 << layer);
        }
        return mask;
    }

    public static void SetLayerRecursively(this GameObject go, int layer) {
        go.layer = layer;
        foreach (Transform t in go.transform) {
            t.gameObject.SetLayerRecursively(layer);
        }
    }

}
