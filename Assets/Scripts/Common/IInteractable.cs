using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
	public abstract void OnInteractStart(GameObject gameObject);
	public abstract void OnInteractActive(GameObject gameObject);
	public abstract void OnInteractEnd(GameObject gameObject);
}
