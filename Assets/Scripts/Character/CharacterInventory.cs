using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour {

    public Character character { get; private set; }

    [SerializeField] private List<int> items;

    private void Awake() {
        character = GetComponent<Character>();
    }

    public bool HasItem(int id) {
        return items[id] > 0;
    }

    public void AddItem(Item item) {
        items[item.id]++;
        Destroy(item.gameObject);
        UIController.main.UpdateItemCounts(items);
    }

    public void RemoveItem(int id) {
        items[id]--;
        UIController.main.UpdateItemCounts(items);
    }

}
