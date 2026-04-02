using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private LayerMask interactLayer;

    public void TryInteract()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayer);

        if (collider == null)
        {
            Debug.Log("Радом нет объекта");
            return;
        }
        if (collider.TryGetComponent<IInteractable>(out var interactable))
        { 
        interactable.Interact();
        }
            
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
