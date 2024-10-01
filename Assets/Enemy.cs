using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float velocity = 1.5f;

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
        Move(direction);

        CheckIfOutOfMap();
    }
}
