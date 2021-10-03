using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour {

    public Character character { get; private set; }

    private List<Item> items;

    private void Awake() {
        character = GetComponent<Character>();
    }

    public void AddItem(Item item) {
        items.Add(item);
    }
}
