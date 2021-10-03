using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Axe,
    Hammer,
    Soda
}

public class Item : InteractiveObject
{
    public ItemType itemType;

    public override void Use(Character character) {
        base.Use(character);
        character.inventory.AddItem(this);
    }

}
