using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 5f;
    [Range(0, 90)]
    public float viewAngle = 45f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Color viewColor = Color.green;

    private void Start()
    {
        // Set the initial view cone when the game starts
        DrawFieldOfView();
    }

    private void LateUpdate()
    {
        // Update the view cone in the LateUpdate to ensure accurate rendering
        DrawFieldOfView();

        // Check for player in the field of view
        if (PlayerInFOV())
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        Debug.Log("Player in FOV! Player is killed.");
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
            float angleToTarget = Vector2.Angle(transform.up, dirToTarget);

            if (angleToTarget < viewAngle / 2)
            {
                float distToTarget = Vector2.Distance(transform.position, target.transform.position);

                // Check for obstacles between the enemy and the player
                if (!Physics2D.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    // Player is within FOV
                    Debug.Log("Player detected in FOV!");
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

