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

    public void nextItem()
    {
        if(currentItem == items[0])
        {
            currentItem = items[1];
            currentItem.Equip();
            return;
		}

		if (currentItem == items[1])
		{
			currentItem = items[0];
			currentItem.Equip();
            return;
		}
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
