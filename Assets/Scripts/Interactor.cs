using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] public List<GameObject> _interactableObjects = new();

    private void OnCollisionEnter(Collision collidedObject)
    {
        if (collidedObject.gameObject.tag == "Interactable")
        {
            _interactableObjects.Add(collidedObject.gameObject);
        }
    }

    private void OnCollisionExit(Collision collidedObject)
    {
        if (_interactableObjects.Contains(collidedObject.gameObject))
        {
            _interactableObjects.Remove(collidedObject.gameObject);
        }
    }

    private void Update()
    {
        CheckInteractionTrigger();
    }
    
    private void CheckInteractionTrigger()
    {
        if (Inputs.IsInteracting())
        {
            if (_interactableObjects.Count > 0)
            {
                IInteractable interactable = _interactableObjects[0].gameObject.GetComponent<IInteractable>();
                interactable.Interact();
            }
        }
    }
}
