using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public PlayerController _player;
    private bool _interacted = false;
    [SerializeField] private UnityEvent _event;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Interaction()
    {
        if (!_interacted && !_player._moving)
        {
            _event.Invoke();
            _interacted = true;
        }
    }
}
