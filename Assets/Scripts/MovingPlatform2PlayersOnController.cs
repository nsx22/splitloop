using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform2PlayersOnController : MonoBehaviour
{
    public int playerCount = 0;
    public int target = 2;
    public MovingPlatform mp;
    
    void Update()
    {
        if (playerCount == target)
        {
            mp.StopIfAtTop();
        }
        else
        {
            mp.canMove = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerCount++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCount--;
    }
}
