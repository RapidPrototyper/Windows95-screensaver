using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeManager : MonoBehaviour
{
    public GameObject pipeSegmentPrefab;  // Prefab for straight segments (cube)
    public GameObject pipeBendPrefab;     // Prefab for bends (spheres)

    public float segmentLength = 1.0f;    // Length of each pipe segment
    public int maxSegments = 100;         // Max number of segments before creating a new pipe
    public float delayBetweenSegments = 0.1f;  // Delay between creating each segment

    private Vector3 currentDirection = Vector3.forward;   // Starting direction
    private Vector3 currentPosition = Vector3.zero;       // Starting position

    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // To avoid overlapping

    private Color currentPipeColor; // Current color of the pipe

    private void Start()
    {
        // Set an initial color for the pipe
        currentPipeColor = Random.ColorHSV();
        StartCoroutine(GeneratePipe());
    }

    IEnumerator GeneratePipe()
    {
        for (int i = 0; i < maxSegments; i++)
        {
            // Step 1: Create a straight pipe segment at the current position
            GameObject newSegment = Instantiate(pipeSegmentPrefab, currentPosition, Quaternion.LookRotation(currentDirection));
            newSegment.transform.localScale = new Vector3(0.2f, 0.2f, segmentLength); // Adjust the size of the pipe
            newSegment.GetComponent<Renderer>().material.color = currentPipeColor;   // Assign the current color to the pipe segment

            // Step 2: Mark this position as occupied
            occupiedPositions.Add(currentPosition);

            // Step 3: Move the current position forward by the segment's length
            currentPosition += currentDirection * segmentLength;

            // Step 4: Check if the new position is already occupied. If so, start a new pipe elsewhere.
            if (occupiedPositions.Contains(currentPosition))
            {
                StartNewPipe();  // Start a new pipe at a different random position.
            }

            // Step 5: Wait for a short period before creating the next segment
            yield return new WaitForSeconds(delayBetweenSegments);

            // Step 6: Randomly create a bend in the pipe
            if (Random.value < 0.4f)  // 40% chance to make a bend
            {
                CreateBend();  // Create a bend and change the direction of the pipe
                currentPipeColor = Random.ColorHSV();  // Change the color after the bend
            }
        }
    }

    void CreateBend()
    {
        // Create a bend at the current position
        GameObject newBend = Instantiate(pipeBendPrefab, currentPosition, Quaternion.identity);
        newBend.GetComponent<Renderer>().material.color = currentPipeColor;  // Set the current color for the bend

        // Choose a new random direction for the pipe (but avoid reversing direction)
        Vector3[] possibleDirections = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.up, Vector3.down };
        Vector3 newDirection;

        do
        {
            newDirection = possibleDirections[Random.Range(0, possibleDirections.Length)];
        } while (newDirection == -currentDirection);  // Avoid going backward

        currentDirection = newDirection;  // Update the pipe's direction
    }

    void StartNewPipe()
    {
        // Start the pipe at a new random position
        currentPosition = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(-10, 10));

        // Pick a random direction for the pipe to start moving in
        currentDirection = new Vector3(
            Mathf.Round(Random.Range(-1, 2)),  // x-axis
            Mathf.Round(Random.Range(-1, 2)),  // y-axis
            Mathf.Round(Random.Range(-1, 2))   // z-axis
        );

        // Change the color when starting a new pipe
        currentPipeColor = Random.ColorHSV();
    }



    public void Reload() 
    {
        //OnReload button Press:
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
