
using UnityEngine;

public sealed class TagList {

    public const string Untagged = "Untagged";
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";
    public const string EditorOnly = "EditorOnly";
    public const string MainCamera = "MainCamera";
    public const string Player = "Player";
    public const string GameController = "GameController";
    public const string InteractiveObject = "InteractiveObject";

}

public static class TagHelper {

    public static void SetTagRecursively(this GameObject go, string tagName) {
        go.tag = tagName;
        foreach (Transform t in go.transform) {
            t.gameObject.SetTagRecursively(tagName);
        }
    }

}