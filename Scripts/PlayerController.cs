using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    [SerializeField] private Camera cam;
    public NavMeshAgent agent;
    [SerializeField] private Animator anim;
    public Interactable focus;
    public bool _moving;
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                MoveToPoint(hit.point);
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    StartCoroutine(CanInteract());
                }
            }
        }
        if (agent.velocity != new Vector3(0, 0, 0))
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Dancing", false);
            _moving = true;
        }
        else
        {
            anim.SetBool("Walking", false);
            _moving = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && !_moving)
        {
            anim.SetBool("Dancing", true);
        }
    }

    IEnumerator CanInteract()
    {
        yield return new WaitForSeconds(0.5f);
        while (_moving)
            yield return null;
        if (!_moving)
            focus.Interaction();
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        MoveToPoint(newFocus.transform.position);
        agent.stoppingDistance = newFocus.radius;
    }

    void RemoveFocus()
    {
        focus = null;
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
