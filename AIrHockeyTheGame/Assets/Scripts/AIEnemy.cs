using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public float maxMovSpeed;
    private Rigidbody2D rb;
    private Vector2 startPos;
    public Rigidbody2D mice;
    public Transform playerBoundaryHolder;
    private Boundary playerBoundary;
    public Transform miceBoundaryHolder;
    private Boundary miceBoundary;
    private Vector2 targetPos;
    private bool isFirstTimeInOpponentsHalf = true;
    private float offSetXFromTarget;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;

        playerBoundary = new Boundary(playerBoundaryHolder.GetChild(0).position.y,
                                 playerBoundaryHolder.GetChild(1).position.y,
                                 playerBoundaryHolder.GetChild(2).position.x,
                                 playerBoundaryHolder.GetChild(3).position.x);

        playerBoundary = new Boundary(miceBoundaryHolder.GetChild(0).position.y,
                                 miceBoundaryHolder.GetChild(1).position.y,
                                 miceBoundaryHolder.GetChild(2).position.x,
                                 miceBoundaryHolder.GetChild(3).position.x);
    }

    private void FixedUpdate()
    {
        float movSpeed;

        if (mice.position.y < miceBoundary.Bottom)
        {
            if (isFirstTimeInOpponentsHalf)
            {
                isFirstTimeInOpponentsHalf = false;
                offSetXFromTarget = Random.Range(-1f, 1f);
            }
            movSpeed = maxMovSpeed * Random.Range(0.1f, 0.3f);
            targetPos = new Vector2(Mathf.Clamp(mice.position.x + offSetXFromTarget, playerBoundary.Left, playerBoundary.Right), startPos.y);
        }
        else
        {
            isFirstTimeInOpponentsHalf = true;
            movSpeed = Random.Range(maxMovSpeed * 0.4f, maxMovSpeed);
            targetPos = new Vector2(Mathf.Clamp(mice.position.x, playerBoundary.Left, playerBoundary.Right), 
                                    Mathf.Clamp(mice.position.y, playerBoundary.Bottom, playerBoundary.Top));
        }
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, movSpeed * Time.fixedDeltaTime));
    }
}
