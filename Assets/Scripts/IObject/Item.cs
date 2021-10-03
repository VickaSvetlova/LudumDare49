using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Axe,
    Hammer,
    Soda,
    Null,
}

public class Item : InteractiveObject
{
    public ItemType itemType;

    protected override void Awake() {
        base.Awake();
        gameObject.SetLayerRecursively(LayerList.Item);

    }

    public override void Use(Character character) {
        base.Use(character);
        character.inventory.AddItem(this);
    }

}
