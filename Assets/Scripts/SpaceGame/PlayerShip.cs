using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Interactable
{
    [SerializeField] private Action action;
    [SerializeField] private Inventory inventory;

	public float health = 100;

	private void Start()
	{
		if (action != null)
		{
			action.onEnter += OnInteractStart;
			action.onEnter += OnInteractActive;
		}
	}

	void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            inventory.OnUse();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            inventory.OnStopUse();
        }
    }

	public override void OnInteractActive(GameObject gameObject)
	{
		//
	}

	public override void OnInteractEnd(GameObject gameObject)
	{
		//
	}

	public override void OnInteractStart(GameObject gameObject)
	{
		//
	}
}
