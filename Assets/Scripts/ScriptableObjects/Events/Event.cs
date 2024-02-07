using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Event")]
public class Event : ScriptableObjectBase
{
    public UnityAction onEventRaised;

    public void RaiseEvent()
    {
        onEventRaised?.Invoke();
    }

    public void Subscribe(UnityAction function)
    {
        onEventRaised += function;
    }

    public void Unsubscribe(UnityAction function)
    {
        onEventRaised -= function;
    }
}