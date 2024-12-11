using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private FancyMouse mouseInteraction;
    [SerializeField] private FancyMouse mouseCombat;
    public LayerMask interactionMask;
    public LayerMask enemyMask;
    
    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100, interactionMask))
        {
            Cursor.SetCursor(mouseInteraction.cursorTexture, mouseInteraction.hotSpot, mouseInteraction.cursorMode);
        }
        else if (Physics.Raycast(ray, out hit, 100, enemyMask))
        {
            Cursor.SetCursor(mouseCombat.cursorTexture, mouseCombat.hotSpot, mouseCombat.cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, mouseInteraction.cursorMode);
        }
    }
}
