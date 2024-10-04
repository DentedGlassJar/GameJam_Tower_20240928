using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Bullet;

    Rigidbody2D rigid_body;
    Animator animator;
    void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float MovementSpeed = 2f;
        GameObject StartText = GameObject.Find("StartText");

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigid_body.velocity = new Vector2(0, MovementSpeed);
            animator.Play("PlayerWalkFwd");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rigid_body.velocity = new Vector2(0, -MovementSpeed);
            animator.Play("PlayerWalkFwd");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid_body.velocity = new Vector2(-MovementSpeed, 0);
            animator.Play("PlayerWalkFwd");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid_body.velocity = new Vector2(MovementSpeed, 0);
            animator.Play("PlayerWalkFwd");
        }
        else
        {
            rigid_body.velocity = Vector2.zero;
            animator.Play("Idle");
        }
        Vector2 BulletVelocity = Vector2.zero;
        Quaternion BulletRotation = Quaternion.identity;
        float BulletSpeed = 5f;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                BulletVelocity = new Vector2(-BulletSpeed, 0);
                BulletRotation = Quaternion.AngleAxis(90, Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                BulletVelocity = new Vector2(0, BulletSpeed);
                BulletRotation = Quaternion.identity;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                BulletVelocity = new Vector2(0, -BulletSpeed);
                BulletRotation = Quaternion.AngleAxis(180, Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                BulletVelocity = new Vector2(BulletSpeed, 0);
                BulletRotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            Instantiate(Bullet, this.transform.position, BulletRotation).GetComponent<Rigidbody2D>().velocity = BulletVelocity;
        }
    }
}
