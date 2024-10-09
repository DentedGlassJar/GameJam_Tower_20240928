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

    [SerializeField] GameObject SpiderBoss;
    [SerializeField] GameObject BatBoss;
    [SerializeField] GameObject RatBoss;
    [SerializeField] GameObject SnakeBoss;

    bool SpiderBossSpawned = false;
    bool BatBossSpawned = false;
    bool RatBossSpawned = false;
    bool SnakeBossSpawned = false;

    public bool BossesSpawned
    {
        get { return SpiderBossSpawned; }
        set
        {
            SpiderBossSpawned = value;
            BatBossSpawned = value;
            RatBossSpawned = value;
            SnakeBossSpawned = value;
        }
    }

    GameObject EnemyType;

    Vector2 StartOfMap = new Vector2(-8.86f, 4.8f);
    Vector2 EndOfMap = new Vector2(8.9f, -5f);

    public float SpawningTime = 2f;
    float LevelDuration = 10f;
    public float GameplayTime = 0;
    int a = 0;
    void Start()
    {
        EnemyType = Spider;
    }

    float RandomValueBetween(float min, float max)
    {
        return Random.value * (max - min) + min;
    }

    void SpawnEnemy(GameObject _EnemyType)
    {
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
        GameObject Enemy = Instantiate(_EnemyType, EnemyStartPosition, Quaternion.identity);
        Enemy.GetComponent<Enemy>().direction = EnemyStartDirection;
        Enemy.name = _EnemyType.name;
    }

    void SpawnMinion(string ParentName, GameObject _EnemyType)
    {
        GameObject Parent = GameObject.Find(ParentName);
        if(Parent == null)
            return;

        GameObject Enemy = Instantiate(_EnemyType, Parent.transform.position, Quaternion.identity);
        Enemy.GetComponent<Enemy>().direction = (Direction)Random.Range(0, 4);
    }
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
            if (!SnakeBossSpawned)
            {
                SpawnEnemy(SnakeBoss);
                SnakeBossSpawned = true;
            }
            GameObject.Find("LevelNumber").GetComponent<Text>().text = "8th Floor";
        }
        else if(GameplayTime > 6 * LevelDuration)
        {
            EnemyType = SnakeSkeleton;
            if (!RatBossSpawned)
            {
                SpawnEnemy(RatBoss);
                RatBossSpawned = true;
            }
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
            if(!BatBossSpawned)
            {
                SpawnEnemy(BatBoss);
                BatBossSpawned = true;
            }
            
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
            if (!SpiderBossSpawned)
            {
                SpawnEnemy(SpiderBoss);
                SpiderBossSpawned = true;
            }
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
            SpawnEnemy(EnemyType);
            a++;

            if(a == 2)
            {
                a = 0;
                for(int i = 0; i < 2; i++)
                {
                    SpawnMinion("SpiderBoss", Spider);
                    SpawnMinion("BatBoss", Bat);
                    SpawnMinion("RatBoss", Rat);
                    SpawnMinion("SnakeBoss", SnakeSkeleton);
                }
                
            }
        }
    }
}
