using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLeft : MonoBehaviour
{
    //Variables
    public float speed;
    public float start;
    public float end;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        if (transform.position.x <= end)
        {
            if (gameObject.CompareTag("obstacle"))
            {
                Destroy(this.gameObject);
            }
            else
            {
                transform.position = new Vector2(start, transform.position.y);
            }

        }
    }
}
