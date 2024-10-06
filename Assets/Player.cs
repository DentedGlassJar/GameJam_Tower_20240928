using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject GameOver;

    Rigidbody2D rigid_body;
    Animator animator;

    Direction direction = Direction.Right;

    int Coins = 0;
    void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            GameObject.Find("Coins").GetComponent<Text>().text = "Coins: " + ++Coins;
        }
        else if(collision.collider.tag == "Enemy")
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        Time.timeScale = 0;
        GameOver.SetActive(true);
        GameOver.GetComponent<Text>().text = "Game has ended\nYour score: " + Coins + "\nPress Space to restart";
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy (g);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Coin"))
            {
                Destroy(g);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                Destroy(g);
            }
            GameOver.SetActive(false);
            Time.timeScale = 1;
            Coins = 0;
            GameObject.Find("Coins").GetComponent<Text>().text = "Coins: 0";
            GameObject.Find("EnemysSpawner").GetComponent<EnemysSpawner>().GameplayTime = 0;
            GameObject.Find("EnemysSpawner").GetComponent<EnemysSpawner>().SpawningTime = 0;
        }
        float MovementSpeed = 2f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigid_body.velocity = new Vector2(0, MovementSpeed);
            animator.Play("Left");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rigid_body.velocity = new Vector2(0, -MovementSpeed);
            animator.Play("Right");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid_body.velocity = new Vector2(-MovementSpeed, 0);
            direction = Direction.Left;
            animator.Play("Left");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid_body.velocity = new Vector2(MovementSpeed, 0);
            direction = Direction.Right;
            animator.Play("Right");
        }
        else
        {
            if (direction == Direction.Right)
                animator.Play("RightIdle");
            else
                animator.Play("LeftIdle");
            rigid_body.velocity = Vector2.zero; 
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
