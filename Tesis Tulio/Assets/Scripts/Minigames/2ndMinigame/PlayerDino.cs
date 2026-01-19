using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDino : MonoBehaviour
{
    private Rigidbody2D rb;
    bool isJumping = false;

    public GameObject stand;
    public GameObject crouch;
    public ManagerDino gameManager;

    private AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpSound = GetComponent<AudioSource>();

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 50;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && isJumping == false)
        {
            rb.velocity = new Vector3(0, 20, 0);
            isJumping = true;
            jumpSound.Play();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle"))
        {
            gameManager.GameOver();
        }
    }
}
