using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    bool clicked = true;
    bool canMove;
    Vector2 playerSize;
    Rigidbody2D rb;
    public Transform BoundaryHolder;

    Boundary playerBoundary;

    struct Boundary
    {
        public float Top, Bottom, Left, Right;
        public Boundary(float top, float bottom, float left, float right)
        {
            Top = top; Bottom = bottom; Left = left; Right = right;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSize = GetComponent<SpriteRenderer>().bounds.extents;
        rb = GetComponent<Rigidbody2D>();

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (clicked)
            {
                clicked = false;
                if ((mousePos.x >= transform.position.x && mousePos.x < transform.position.x + playerSize.x ||
               mousePos.x <= transform.position.x && mousePos.x > transform.position.x - playerSize.x) &&
               (mousePos.y >= transform.position.y && mousePos.y < transform.position.y + playerSize.y ||
               mousePos.y <= transform.position.y && mousePos.y > transform.position.y - playerSize.y))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right), 
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Bottom, playerBoundary.Top));
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            clicked = true;
        }
    }
}
