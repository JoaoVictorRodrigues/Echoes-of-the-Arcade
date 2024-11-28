using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Rendering;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    public event Action onMiniGame;
    
    private bool isMoving;
    private Vector2 input;
    private Vector3 targetPosition;
    private Vector3 facingDirection;

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0; 

            if (input != Vector2.zero)
            {
                facingDirection = new Vector3(input.x, input.y, 0);

                targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;


                if(IsWalkable(targetPosition)) 
                    StartCoroutine(Move(targetPosition));
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    void Interact()
    {
        var interactPosition = transform.position + facingDirection;

        var collider = Physics2D.OverlapCircle(interactPosition, 0.3f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move (Vector3 targetPosition)
    {
        isMoving = true;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }
    private bool IsWalkable (Vector3 targetPosition)
    {
        if(Physics2D.OverlapCircle(targetPosition,0.3f, solidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }


}
