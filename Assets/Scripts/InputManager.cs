using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Reference to PlayerUnit and GameManager
    public PlayerUnit player;
    public GameManager gameManager; // Must be linked to GameManager

    // Distance for checking collisions on the Grid (should be 0.5f or according to grid size)
    [SerializeField] private float checkDistance = 0.5f;

    void Update()
    {
        if (player == null || gameManager == null) return;

        // Check for Element Shift (using 'E' key)
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check AP before shifting (Uses player.Stats for Encapsulation)
            if (!player.Stats.UseActionPoint(1))
            {
                Debug.Log("Cannot shift element: Not enough Action Points.");
                return;
            }
            player.ShiftElement();
            gameManager.EndPlayerTurn();
        }

        // Check for Movement (Arrow Keys)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryMoveAndInteract(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryMoveAndInteract(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryMoveAndInteract(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryMoveAndInteract(Vector3.right);
        }
    }

    private void TryMoveAndInteract(Vector3 direction)
    {
        // Target position (using simple addition for grid-based movement)
        Vector3 targetPosition = player.transform.position + direction;

        // 1. Try to use Action Point: Stop if AP runs out (Encapsulation)
        if (!player.Stats.UseActionPoint(1))
        {
            Debug.Log("Cannot move: Not enough Action Points.");
            return;
        }

        // 2. Check for interactive object at the target position
        IElementInteractive targetObject = FindInteractiveObjectAtPosition(targetPosition);

        bool movementSuccess = false;

        if (targetObject != null)
        {
            // A. Interact with object (Polymorphism)
            bool interactionSuccessful = false;

            // Safety check for destroyed objects
            if (targetObject is Component component && component.gameObject != null)
            {
                player.InteractWith(targetObject);
                interactionSuccessful = true;
            }

            // Logic after interaction:
            if (targetObject.Equals(null) && interactionSuccessful)
            {
                // A1. If object was destroyed (e.g., IceBlock), move Player to the target spot
                player.MoveTo(targetPosition);
                movementSuccess = true;
            }
            else if (!interactionSuccessful)
            {
                // A2. Interaction failed (e.g., hitting a wall/blocked path): Do not move, refund AP.
                player.Stats.ResetActionPoints(); // Refund AP
                Debug.Log("Collision blocked movement. AP returned.");
                return;
            }
            else if (interactionSuccessful)
            {
                // A3. Interaction was successful but object wasn't destroyed (e.g., walking over cooled lava, activating an Anchor): move Player
                player.MoveTo(targetPosition);
                movementSuccess = true;
            }
        }
        else
        {
            // B. No interactive object, move normally
            player.MoveTo(targetPosition);
            movementSuccess = true;
        }

        // End turn only if movement/action was completed successfully
        if (movementSuccess)
        {
            gameManager.EndPlayerTurn();
        }
        else
        {
            // If movement wasn't successful and AP was refunded, the turn ends.
            gameManager.EndPlayerTurn();
        }
    }

    // Method to find IElementInteractive object at the target grid position (using Physics2D)
    private IElementInteractive FindInteractiveObjectAtPosition(Vector3 position)
    {

        // Use OverlapBoxAll to detect any colliders at the target spot
        Collider2D[] hits = Physics2D.OverlapBoxAll(position, new Vector2(checkDistance, checkDistance), 0);

        foreach (Collider2D hit in hits)
        {
            // Try to get the IElementInteractive interface from the hit object
            IElementInteractive interactive = hit.GetComponent<IElementInteractive>();
            if (interactive != null)
            {
                // Return the object that implements the Interface
                return interactive;
            }
        }
        return null;
    }
}
