using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject Spider;
    Vector2 StartOfMap = new Vector2(-8.86f, 9.46f);
    Vector2 EndOfMap = new Vector2(8.9f, -5f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % 200 == 0)
        {
            Vector2 EnemyStartPosition;
            Direction EnemyStartDirection = (Direction)Random.Range(0, 3);
            switch (EnemyStartDirection)
            {
                case Direction.Up:
                    EnemyStartPosition = new Vector2(Random.value * (EndOfMap.x - StartOfMap.x) + StartOfMap.x, EndOfMap.y);
                    break;
                case Direction.Down:
                    EnemyStartPosition = new Vector2(Random.value * (EndOfMap.x - StartOfMap.x) + StartOfMap.x, StartOfMap.y);
                    break;
                case Direction.Right:
                    EnemyStartPosition = new Vector2(StartOfMap.x, Random.value * (EndOfMap.y - StartOfMap.y) + StartOfMap.y);
                    break;
                case Direction.Left:
                    EnemyStartPosition = new Vector2(EndOfMap.x, Random.value * (EndOfMap.y - StartOfMap.y) + StartOfMap.y);
                    break;
                default:
                    EnemyStartPosition = StartOfMap;
                    break;
            }
            GameObject Enemy = Instantiate(Spider, EnemyStartPosition, Quaternion.identity);
            Enemy.GetComponent<Enemy>().direction = EnemyStartDirection;
        }
    }
}
