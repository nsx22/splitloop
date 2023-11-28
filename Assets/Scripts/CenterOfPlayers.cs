using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfPlayers : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject playerBounds;

    private void Update()
    {
        if (player1 != null && player2 != null && player1.activeSelf && player2.activeSelf)
        {
            playerBounds.SetActive(true);
            transform.position = new Vector3(Average(player1.transform.position.x, player2.transform.position.x),
                                            Average(player1.transform.position.y,
                                            player2.transform.position.y), 0);
        }
        else if (player1 == null || !player1.activeSelf)
        {
            playerBounds.SetActive(false);
            transform.position = player2.transform.position;
        }
        else if (player2 == null || !player2.activeSelf)
        {
            playerBounds.SetActive(false);
            transform.position = player1.transform.position;
        }
    }

    public float Average(float value1, float value2)
    {
        return ((value1 + value2) / 2);
    }
}
