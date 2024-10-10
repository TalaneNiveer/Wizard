using UnityEngine;

public class Adventurer : MonoBehaviour
{
    private Vector3 targetPosition;

    void Start()
    {
        // Set initial position to align with the grid
        targetPosition = transform.position;
    }

    public void TakeTurn()
    {
        // Move in a random cardinal or intercardinal direction (1 unit)
        Vector3[] directions = {
            Vector3.up, Vector3.down, Vector3.left, Vector3.right,
            new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, 1, 0), new Vector3(-1, -1, 0)
        };

        // Choose a random direction and update the target position
        Vector3 direction = directions[Random.Range(0, directions.Length)];
        targetPosition += direction;

        // Snap to the grid (optional)
        targetPosition = new Vector3(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y), 0);

        // Move adventurer to the target position
        transform.position = targetPosition;
    }
}
