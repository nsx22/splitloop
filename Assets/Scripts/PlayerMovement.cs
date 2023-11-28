using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    public bool isPlayerOne;

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private bool isJumping;

    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float oldVelocity;
    float crashThreshold = 10f;

    public ParticleSystem hitGroundPS;
    public GameObject finishedPS;

    public TimerController tc;
    public bool canMove = true;
    public AudioSource jumpSound;
    public AudioSource hitGroundSound;
    public AudioSource explosionSound;

    private void Start()
    {
        oldVelocity = rb.velocity.sqrMagnitude;
        crashThreshold *= crashThreshold;   // So it works with sqrMagnitude
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (oldVelocity - rb.velocity.sqrMagnitude > crashThreshold && IsGrounded())
        {
            hitGroundPS.Play();
            hitGroundSound.pitch = Random.Range(0.9f, 1.1f);
            hitGroundSound.Play();
        }
        oldVelocity = rb.velocity.sqrMagnitude;
    }
    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                tc.isCounting = true;
            }

            if (isPlayerOne)
            {
                if (Input.GetKey(KeyCode.A)) { horizontal = -1f; } else if (Input.GetKey(KeyCode.D)) { horizontal = 1f; } else { horizontal = 0f; }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow)) { horizontal = -1f; } else if (Input.GetKey(KeyCode.RightArrow)) { horizontal = 1f; } else { horizontal = 0f; }
            }

            if (IsGrounded())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (isPlayerOne)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    jumpBufferCounter = jumpBufferTime;
                }
                else
                {
                    jumpBufferCounter -= Time.deltaTime;
                }

                if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
                {
                    jumpSound.pitch = Random.Range(0.9f, 1.1f);
                    jumpSound.Play();
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                    jumpBufferCounter = 0f;

                    StartCoroutine(JumpCooldown());
                }

                if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

                    coyoteTimeCounter = 0f;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    jumpBufferCounter = jumpBufferTime;
                }
                else
                {
                    jumpBufferCounter -= Time.deltaTime;
                }

                if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
                {
                    jumpSound.pitch = Random.Range(0.9f, 1.1f);
                    jumpSound.Play();
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                    jumpBufferCounter = 0f;

                    StartCoroutine(JumpCooldown());
                }

                if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

                    coyoteTimeCounter = 0f;
                }
            }

            Flip();
        }
        else
        {
            horizontal = 0f;
            rb.velocity = Vector2.zero;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsGroundedLarge()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.25f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    public void FinishedLevel()
    {
        Instantiate(finishedPS, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
