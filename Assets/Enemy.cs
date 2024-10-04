using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    Up = 0,
    Down = 1,
    Right = 2,
    Left = 3
}

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;
    Rigidbody2D rigid;

    public Direction direction;
    int WalkLength = 500;
    [SerializeField] float velocity = 3f;

    Vector2 StartOfMap = new Vector2(-8.86f, 9.46f);
    Vector2 EndOfMap = new Vector2(8.9f, -5f);
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    

    

    private void Move(Direction d)
    {
        switch (d)
        {
            case Direction.Up:
                rigid.velocity = new Vector2(0, velocity);
                animator.Play("Left");
                break;
            case Direction.Down:
                rigid.velocity = new Vector2(0, -velocity);
                animator.Play("Right");
                break;
            case Direction.Right:
                rigid.velocity = new Vector2(velocity, 0);
                animator.Play("Right");
                break;
            case Direction.Left:
                rigid.velocity = new Vector2(-velocity, 0);
                animator.Play("Left");
                break;
            default:
                break;
        }
    }

    void CheckIfOutOfMap()
    {
        if (transform.position.x < StartOfMap.x || transform.position.x > EndOfMap.x || transform.position.y > StartOfMap.y || transform.position.y < EndOfMap.y)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % WalkLength == 0)
        {
            direction = (Direction)Random.Range(0, 4);
            WalkLength = 120;
        }
        if(Time.frameCount % 20 == 0)
        {
            const float MinDist = 2f;
            const float MinDistNarrow = 1f;
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                if (Mathf.Abs(transform.position.x - g.transform.position.x) < MinDist && Mathf.Abs(transform.position.y - g.transform.position.y) < MinDistNarrow && g.GetComponent<Rigidbody2D>().velocity.x != 0)
                {
                    float r = Random.value;
                    if (r < 0.45f)
                    {
                        direction = Direction.Up;
                    }
                    else if(r < 0.9f)
                    {
                        direction = Direction.Down;
                    }
                }
                else if (Mathf.Abs(transform.position.y - g.transform.position.y) < MinDist && Mathf.Abs(transform.position.x - g.transform.position.x) < MinDistNarrow && g.GetComponent<Rigidbody2D>().velocity.y != 0)
                {
                    float r = Random.value;
                    if (r < 0.45f)
                    {
                        direction = Direction.Right;
                    }
                    else if(r < 0.9f)
                    {
                        direction = Direction.Left;
                    }
                }
            }
        }
        Move(direction);

        CheckIfOutOfMap();
    }
}
