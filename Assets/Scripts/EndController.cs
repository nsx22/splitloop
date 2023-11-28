using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    private BoxCollider2D bc;
    public AudioSource endSound;
    public TimerController tc;

    public float playerCount = 2;

    public LevelLoader ll;

    private void Start()
    {
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        endSound.Play();
        playerCount -= 1;
        col.GetComponent<PlayerMovement>().FinishedLevel();
    }

    private void Update()
    {
        if (playerCount == 0)
        {
            tc.TimerStop();
            ll.LoadNextLevel();
        }
    }
}
