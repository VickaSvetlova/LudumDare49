using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

    public static InventorySystem main { get; private set; }

    public List<int> items;

    private void Awake() {
        if (main) {
            Destroy(gameObject);
        } else {
            main = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool HasItem(ItemType type) {
        if (type == ItemType.Null) return true;
        return items[(int)type - 1] > 0;
    }

    public void AddItem(Item item) {
        items[(int)item.itemType - 1]++;
        Destroy(item.gameObject);
        UIController.main.UpdateItemCounts(items);
    }

    public void RemoveItem(ItemType type) {
        if (type == ItemType.Null) return;
        items[(int)type - 1]--;
        UIController.main.UpdateItemCounts(items);
    }

}
