using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour {

    public Character character { get; private set; }

    [SerializeField] private List<int> items;

    private void Awake() {
        character = GetComponent<Character>();
    }

    private void Start() {
        UIController.main.UpdateItemCounts(items);
    }

    public bool HasItem(ItemType type) {
        if (type == ItemType.Null) return true;
        return items[(int)type] > 0;
    }

    public void AddItem(Item item) {
        items[(int)item.itemType]++;
        Destroy(item.gameObject);
        UIController.main.UpdateItemCounts(items);
    }

    public void RemoveItem(ItemType type) {
        items[(int)type]--;
        UIController.main.UpdateItemCounts(items);
    }

}
