using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    public bool canMove = true;

    private int i;

    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    void Update()
    {
        if (canMove)
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
            {
                i++;
                if (i == points.Length)
                {
                    i = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }

    public void StopIfAtTop()
    {
        if (i == points.Length - 1 && Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            canMove = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y + 0.4)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
