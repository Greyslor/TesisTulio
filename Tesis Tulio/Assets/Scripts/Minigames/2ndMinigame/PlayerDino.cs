using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDino : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask ground;
    private Rigidbody2D rb;
    bool isJumping = false;

    public GameObject stand;
    public GameObject crouch;
    public ManagerDino gameManager;

    private AudioSource jumpSound;
    private Animator dinoAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dinoAnimator = GetComponent<Animator>();
        jumpSound = GetComponent<AudioSource>();

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 50;

    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);
        dinoAnimator.SetBool("IsGrounded", isGrounded);

        if (Input.GetKey("space") && isJumping == false)
        {
            if (isGrounded) 
            {
                rb.AddForce(Vector2.up * upForce);
                isJumping = true;
                //jumpSound.Play();
            }
        }

        if (Input.GetKey("down") && isJumping == false)
        {
            stand.SetActive(false);
            crouch.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle"))
        {
            gameManager.GameOver();
        }
    }
}
