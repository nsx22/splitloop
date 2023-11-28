using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LavaController : MonoBehaviour
{
    public GameObject explosion;
    public AudioSource explosionSound;
    public GameObject player1;
    public GameObject player2;
    public TimerController tc;
    public EndController ec;

    void OnTriggerEnter2D(Collider2D col)
    {
        Instantiate(explosion, col.transform.position, Quaternion.identity);
        explosionSound.Play();
        col.gameObject.SetActive(false);
        StartCoroutine(Death());
    }
    public IEnumerator Death()
    {
        ec.playerCount = 2;
        tc.timerAnim.SetTrigger("ResetTimer");
        
        player1.GetComponent<PlayerMovement>().canMove = false;
        player2.GetComponent<PlayerMovement>().canMove = false;
        tc.TimerStop();

        yield return new WaitForSeconds(1);
        tc.timerResetSound.Play();
        
        player1.SetActive(true);
        player2.SetActive(true);
        player1.transform.position = new Vector3(-5f, -2.25f, 0f);
        player2.transform.position = new Vector3(5f, -2.25f, 0f);

        yield return new WaitForSeconds(2f);

        player1.GetComponent<PlayerMovement>().canMove = true;
        player2.GetComponent<PlayerMovement>().canMove = true;
        player1.transform.position = new Vector3(-5f, -2.25f, 0f);
        player2.transform.position = new Vector3(5f, -2.25f, 0f);
        tc.TimerReset();

    }
}
