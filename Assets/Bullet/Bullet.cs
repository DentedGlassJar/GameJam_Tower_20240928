using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    Vector2 StartOfMap = new Vector2(-8.86f, 9.46f);
    Vector2 EndOfMap = new Vector2(8.9f, -5f);

    [SerializeField] GameObject Coin;
    void Start()
    {
        
    }

    void CheckIfOutOfMap()
    {
        if (transform.position.x < StartOfMap.x || transform.position.x > EndOfMap.x || transform.position.y > StartOfMap.y || transform.position.y < EndOfMap.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(collision.collider.gameObject);
            Instantiate(Coin, this.transform.position, Quaternion.identity);
            
        }
        else
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOutOfMap();
    }
}
