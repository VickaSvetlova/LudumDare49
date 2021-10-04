using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Null,
    Key,
    Hammer,
    Axe,
    Soda,
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
        InventorySystem.main.AddItem(this);
    }

    public override string GetName() {
        return "Take " + base.GetName();
    }

}
