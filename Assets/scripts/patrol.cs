using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class patrol : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    // Field of View variables
    public float viewRadius = 5f;
    [Range(0, 360)]
    public float viewAngle = 45f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Color viewColor = Color.green;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Set the initial view cone when the game starts
        DrawFieldOfView();
    }

    private void Update()
    {
        Patrol();
        DrawFieldOfView();

        // Check for player in the field of view
        if (PlayerInFOV())
        {
            KillPlayer();
        }
    }

    void Patrol()
    {
        // Move towards the current patrol point
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, patrolSpeed * Time.deltaTime);

        // Check if the enemy has reached the current patrol point
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            // Switch to the next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void KillPlayer()
    {
        Debug.Log("Player in FOV! Player is killed.");

        // Destroy the player GameObject
        Destroy(player);

        // Add your player death logic here
        // For demonstration purposes, we'll reload the scene after a delay
        Invoke("ReloadScene", 2f);
    }

    void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    bool PlayerInFOV()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (Collider2D target in targetsInViewRadius)
        {
            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;
            if (Vector2.Angle(transform.up, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector2.Distance(transform.position, target.transform.position);

                // Check for obstacles between the enemy and the player
                if (!Physics2D.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    // Player is within FOV
                    return true;
                }
            }
        }

        // Player is not within FOV
        return false;
    }

    void DrawFieldOfView()
    {
        int rayCount = 360;
        float angle = 0;
        float angleIncrease = viewAngle / rayCount;

        Vector3[] viewPoints = new Vector3[rayCount + 1];

        for (int i = 0; i <= rayCount; i++)
        {
            float angleRad = transform.eulerAngles.z - viewAngle / 2 + angle;
            Vector3 dir = new Vector3(Mathf.Sin(angleRad * Mathf.Deg2Rad), Mathf.Cos(angleRad * Mathf.Deg2Rad), 0);
            viewPoints[i] = transform.position + dir * viewRadius;

            // Check for obstacles in the line of sight
            if (Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask))
            {
                viewPoints[i] = transform.position + dir * Mathf.Clamp(Vector3.Distance(transform.position, viewPoints[i]), 0f, viewRadius);
            }

            angle += angleIncrease;
        }

        DrawFOVMesh(viewPoints);

        // Rotate the FOV based on the enemy's rotation if it has child objects
        if (transform.childCount > 0)
        {
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
        }
    }

    void DrawFOVMesh(Vector3[] vertices)
    {
        GetComponent<LineRenderer>().positionCount = vertices.Length;
        GetComponent<LineRenderer>().SetPositions(vertices);
        GetComponent<LineRenderer>().startColor = viewColor;
        GetComponent<LineRenderer>().endColor = viewColor;
        GetComponent<LineRenderer>().material.color = viewColor;
    }
}

