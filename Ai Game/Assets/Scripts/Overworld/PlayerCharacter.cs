// PlayerMovement.cs
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements;

public class PlayerCharacter : Character
{
    public float moveTime = 0.2f; // Time in seconds to move from one grid cell to another
    public LayerMask obstacleLayer; // Layer to check for obstacles
    public LayerMask interactableLayer; // Layer to check for interactable objects
    private Vector2 movementInput;
    private Vector2 facingDirection = Vector2.right;
    private bool isMoving = false;

    private PlayerInputActions controls;
    private Coroutine moveCoroutine;
    private Vector2 boxPosition; // To visualize the collider box
    public float collisionBoxSize = 0.8f;
    private UIManager uiManager;

    private void Awake()
    {
        controls = new PlayerInputActions();
        uiManager = FindObjectOfType<UIManager>();
        DisableControls();
    }

    public void EnableControls()
    {
        controls.Enable();
        controls.Player.Move.started += OnMoveStarted;
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Interact.performed += OnInteract;
    }

    public void DisableControls()
    {
        controls.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        // check if there is an interactable object in front of the player with box
        Vector2 interactPosition = new Vector2(transform.position.x, transform.position.y) + facingDirection;
        Collider2D collider = Physics2D.OverlapBox(interactPosition, new Vector2(collisionBoxSize, collisionBoxSize), 0, interactableLayer);
        if (collider != null)
        {
            NPC interactionTarget = collider.gameObject.GetComponent<NPC>();
            if (interactionTarget != null)
            {
                Debug.Log("Interacting with " + interactionTarget.characterName);
                uiManager.SetCurrentNPC(interactionTarget);
                uiManager.OpenDialogueBox();
            }
            else
            {
                Debug.LogWarning("Interactable object does not have NPC component");
            }
            
        }
    }

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        if (!isMoving)
        {
            moveCoroutine = StartCoroutine(Move());
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    private IEnumerator Move()
    {
        isMoving = true;

        while (movementInput != Vector2.zero)
        {
            Vector2 startPosition = transform.position;
            facingDirection = GetGridMovement(movementInput);
            Vector2 endPosition = startPosition + facingDirection;
            // Set end position to the nearest grid point
            endPosition = RoundToNearestGridPoint(endPosition.x, endPosition.y);

            //find shifted end position to check for obstacles (its off by .5)
            boxPosition = endPosition + new Vector2(0.5f, 0.5f);
            
            // Check if the end position has a collider
            if (IsObstacle())
            {
                isMoving = false;
                yield break; // Stop moving if there is an obstacle
            }

            float elapsedTime = 0;
            while (elapsedTime < moveTime)
            {
                MovePosition(Vector2.Lerp(startPosition, endPosition, elapsedTime / moveTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            MoveToNearestGridPosition();
        }

        isMoving = false;
    }

    private void MoveToNearestGridPosition()
    {
        Vector2 currentPosition = transform.position;
        Vector2 nearestGridPosition = RoundToNearestGridPoint(currentPosition.x, currentPosition.y);
        MovePosition(nearestGridPosition);
    }

    public void SetPlayerPosition(Vector3 position)
    {
        CancelMove();

        MovePosition(position);
        MoveToNearestGridPosition();
    }

    private void MovePosition(Vector3 posiion)
    {
        posiion = new Vector3(posiion.x, posiion.y, transform.position.z);
        gameObject.transform.position = posiion;
    }

    private void CancelMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            isMoving = false;
        }
    }

    private Vector2 RoundToNearestGridPoint(float x, float y)
    {
        return new Vector2(Mathf.Round(x), Mathf.Round(y));
    }

    private Vector2 GetGridMovement(Vector2 input)
    {
        Vector2 movement = Vector2.zero;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            movement.x = Mathf.Sign(input.x);
        }
        else
        {
            movement.y = Mathf.Sign(input.y);
        }

        return movement;
    }

    private bool IsObstacle()
    {
        return Physics2D.OverlapBox(boxPosition, new Vector2(collisionBoxSize, collisionBoxSize), 0, obstacleLayer) != null;
    }

    private void OnDrawGizmos()
    {
        if (isMoving)
        {
            // Draw the overlap box at the end position
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxPosition, new Vector2(collisionBoxSize, collisionBoxSize));
        }
    }
}
