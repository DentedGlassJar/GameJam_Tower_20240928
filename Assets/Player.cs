using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid_body;
    void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float MovementSpeed = 2f;
        GameObject StartText = GameObject.Find("StartText");

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigid_body.velocity = new Vector2(0, MovementSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rigid_body.velocity = new Vector2(0, -MovementSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid_body.velocity = new Vector2(-MovementSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid_body.velocity = new Vector2(MovementSpeed, 0);
        }
        else
        {
            rigid_body.velocity = Vector2.zero;
        }
    }
}
