using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    GameObject Player;

    public Direction direction;
    [SerializeField] float velocity = 3f;
    [SerializeField] float EscapingChance = 0.6f;
    [SerializeField] float MinDistPlayer = 3f;

    int WalkLength;

    Vector2 StartOfMap = new Vector2(-8.86f, 9.46f);
    Vector2 EndOfMap = new Vector2(8.9f, -5f);
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        WalkLength = (int)(750f / velocity);
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
        if (Time.frameCount % (WalkLength / velocity) == 0)
        {
            direction = (Direction)Random.Range(0, 4);
            WalkLength = 180;
        }
        if(Time.frameCount % (30 / velocity) == 0)
        {
            if(Random.value < 0.5f)
            {
                if (transform.position.x - Player.transform.position.x > 0 && transform.position.x - Player.transform.position.x < MinDistPlayer)
                    direction = Direction.Left;
                else if (Player.transform.position.x - transform.position.x > 0 && Player.transform.position.x - transform.position.x < MinDistPlayer)
                    direction = Direction.Right;
            }
            else
            {
                if (transform.position.y - Player.transform.position.y > 0 && transform.position.y - Player.transform.position.y < MinDistPlayer)
                    direction = Direction.Down;
                else if (Player.transform.position.y - transform.position.y > 0 && Player.transform.position.y - transform.position.y < MinDistPlayer)
                    direction = Direction.Up;
            }
            
            const float MinDist = 2f;
            const float MinDistNarrow = 1f;
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                if (Mathf.Abs(transform.position.x - g.transform.position.x) < MinDist && Mathf.Abs(transform.position.y - g.transform.position.y) < MinDistNarrow && g.GetComponent<Rigidbody2D>().velocity.x != 0)
                {
                    float r = Random.value;
                    if (r < EscapingChance / 2f)
                    {
                        direction = Direction.Up;
                    }
                    else if(r < EscapingChance)
                    {
                        direction = Direction.Down;
                    }
                }
                else if (Mathf.Abs(transform.position.y - g.transform.position.y) < MinDist && Mathf.Abs(transform.position.x - g.transform.position.x) < MinDistNarrow && g.GetComponent<Rigidbody2D>().velocity.y != 0)
                {
                    float r = Random.value;
                    if (r < EscapingChance / 2f)
                    {
                        direction = Direction.Right;
                    }
                    else if(r < EscapingChance)
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
