using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Item[] items;
    public Item currentItem {  get; private set; }

    private void Start()
    {
        currentItem = items[0];
        currentItem.Equip();
    }

    public void OnUse()
    {
        currentItem?.Use();
    }

    public void OnStopUse()
    {
        currentItem?.StopUse();
    }
}
