using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemysSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject Spider;
    [SerializeField] GameObject SpiderBat;
    [SerializeField] GameObject Bat;
    [SerializeField] GameObject BatRat;
    [SerializeField] GameObject Rat;
    [SerializeField] GameObject RatSnake;
    [SerializeField] GameObject SnakeSkeleton;
    [SerializeField] GameObject Skeleton;

    GameObject EnemyType;

    Vector2 StartOfMap = new Vector2(-8.86f, 9.46f);
    Vector2 EndOfMap = new Vector2(8.9f, -5f);

    public float SpawningTime = 2f;
    float LevelDuration = 10f;
    public float GameplayTime = 0;
    void Start()
    {
        EnemyType = Spider;
    }

    float RandomValueBetween(float min, float max)
    {
        return Random.value * (max - min) + min;
    }

    // Update is called once per frame
    void Update()
    {
        GameplayTime += Time.deltaTime;
        if(GameplayTime > 8 * LevelDuration)
        {
            GameObject.Find("Player").GetComponent<Player>().GameEnd();
        }
        else if(GameplayTime > 7 * LevelDuration)
        {
            EnemyType = Skeleton;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "8th Floor";
        }
        else if(GameplayTime > 6 * LevelDuration)
        {
            EnemyType = SnakeSkeleton;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "7th Floor";
        }
        else if (GameplayTime > 5 * LevelDuration)
        {
            EnemyType = RatSnake;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "6th Floor";
        }
        else if (GameplayTime > 4 * LevelDuration)
        {
            EnemyType = Rat;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "5th Floor";
        }
        else if (GameplayTime > 3 * LevelDuration)
        {
            EnemyType = BatRat;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "4th Floor";
        }
        else if (GameplayTime > 2 * LevelDuration)
        {
            EnemyType = Bat;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "3rd Floor";
        }
        else if (GameplayTime > LevelDuration)
        {
            EnemyType = SpiderBat;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "2nd Floor";
        }
        else
        {
            EnemyType = Spider;
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "Ground Floor";
        }

        if (GameplayTime > SpawningTime)
        {
            SpawningTime += 0.5f + Random.value * 1f;
            Vector2 EnemyStartPosition;
            Direction EnemyStartDirection = (Direction)Random.Range(0, 3);
            switch (EnemyStartDirection)
            {
                case Direction.Up:
                    EnemyStartPosition = new Vector2(RandomValueBetween(0.2f, 0.8f) * (EndOfMap.x - StartOfMap.x) + StartOfMap.x, EndOfMap.y);
                    break;
                case Direction.Down:
                    EnemyStartPosition = new Vector2(RandomValueBetween(0.2f, 0.8f) * (EndOfMap.x - StartOfMap.x) + StartOfMap.x, StartOfMap.y);
                    break;
                case Direction.Right:
                    EnemyStartPosition = new Vector2(StartOfMap.x, RandomValueBetween(0.2f, 0.8f) * (EndOfMap.y - StartOfMap.y) + StartOfMap.y);
                    break;
                case Direction.Left:
                    EnemyStartPosition = new Vector2(EndOfMap.x, RandomValueBetween(0.2f, 0.8f) * (EndOfMap.y - StartOfMap.y) + StartOfMap.y);
                    break;
                default:
                    EnemyStartPosition = StartOfMap;
                    break;
            }
            GameObject Enemy = Instantiate(EnemyType, EnemyStartPosition, Quaternion.identity);
            Enemy.GetComponent<Enemy>().direction = EnemyStartDirection;
        }
    }
}
