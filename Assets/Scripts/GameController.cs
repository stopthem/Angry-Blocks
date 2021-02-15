using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private ShotUI m_shotUI;
    private ShootingScript m_shootingScript;

    public GameObject[] blocks;

    public List<GameObject> levels;

    private GameObject m_level1;
    private GameObject m_level2;

    private Vector2 m_level1Pos;
    private Vector2 m_level2Pos;

    [HideInInspector] public int shotCount;
    public int ballsCount;
    [HideInInspector] public int score;

    private GameObject ballContainer;
    public GameObject gameOver;

    private GameObject[] balls;

    public TextMeshProUGUI ballsCountText;

    private bool firstShot;
    private void Awake()
    {
        m_shotUI = GameObject.Find("CannonCanvas").GetComponent<ShotUI>();
        ballContainer = GameObject.Find("BallsContainer");
        m_shootingScript = GameObject.Find("cannonImage").GetComponent<ShootingScript>();
    }

    private void Start()
    {
        PlayerPrefs.DeleteKey("Level");

        ballsCount = PlayerPrefs.GetInt("BallsCount", ballsCount);
        ballsCountText.text = ballsCount.ToString();
        Physics2D.gravity = new Vector2(0, -17);
        SpawnLevel();
        GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn", true);
    }

    private void Update()
    {
        
        CheckBlocks();
        CheckIfGameOver();


        if (shotCount >= 2)
        {
            firstShot = false;
        }
        else
        {
            firstShot = true;
        }
    }

    private void CheckIfGameOver()
    {
        if (ballContainer.transform.childCount == 0 && shotCount == 4)
        {
            gameOver.SetActive(true);
            GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn", false);
        }
    }
    private void CheckBlocks()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length < 1 && ballContainer.transform.childCount == 0)
        {
            // RemoveBalls();
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            SpawnLevel();

            if (firstShot)
            {
                score += 5;
            }
            else
            {
                score += 3;
            }
        }
    }

    private void SpawnLevel()
    {
        if (PlayerPrefs.GetInt("Level", 0) == 0)
        {
            SpawnNewLevel(0, 17, 3, 5);
        }

        if (PlayerPrefs.GetInt("Level", 0) == 1)
        {
            SpawnNewLevel(1, 18, 3, 5);
        }

        if (PlayerPrefs.GetInt("Level", 0) == 2)
        {
            SpawnNewLevel(2, 19, 3, 6);
        }

        if (PlayerPrefs.GetInt("Level", 0) == 3)
        {
            SpawnNewLevel(5, 20, 3, 6);
        }
        if (PlayerPrefs.GetInt("Level", 0) == 4)
        {
            SpawnNewLevel(11, 25, 4, 7);
        }

        if (PlayerPrefs.GetInt("Level", 0) == 5)
        {
            SpawnNewLevel(12, 27, 5, 8);
        }

        if (PlayerPrefs.GetInt("Level", 0) == 6)
        {
            SpawnNewLevel(15, 30, 5, 9);
        }

        if (PlayerPrefs.GetInt("Level", 0) == 7)
        {
            SpawnNewLevel(20, 45, 7, 10);
        }
    }

    private void SpawnNewLevel(int numberLevel1, int numberLevel2, int min, int max)
    {
        if (shotCount > 1)
        {
            Camera.main.GetComponent<CameraTransitions>().RotateCameraToFront();
        }

        shotCount = 1;

        m_level1Pos = new Vector2(9.5f, 1f);
        m_level2Pos = new Vector2(9.5f, -3.4f);

        m_level1 = levels[numberLevel1];
        m_level2 = levels[numberLevel2];

        if (m_level1 != null && m_level2 != null)
        {
            Instantiate(m_level1, m_level1Pos, Quaternion.identity);
            Instantiate(m_level2, m_level2Pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("null");
        }
        SetBlocksCount(min, max);
    }

    private void SetBlocksCount(int min, int max)
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < blocks.Length; i++)
        {
            int count = Random.Range(min, max);
            blocks[i].GetComponent<Block>().SetStartingCount(count);
        }
    }


    // private void RemoveBalls()
    // {
    //     balls = GameObject.FindGameObjectsWithTag("Ball");
    //     for (int i = 0; i < balls.Length; i++)
    //     {
    //         Destroy(balls[i]);
    //     }
    // }

    public void CheckShotCount()
    {
        if (shotCount == 1)
        {
            m_shotUI.SetShotText("SHOT");
            m_shotUI.SetShotNumberText(shotCount + " / 3");
            m_shotUI.Flash();
        }

        if (shotCount == 2)
        {
            m_shotUI.SetShotText("SHOT");
            m_shotUI.SetShotNumberText(shotCount + " / 3");
            m_shotUI.Flash();
        }

        if (shotCount == 3)
        {
            m_shotUI.SetShotText("FINAL");
            m_shotUI.SetShotNumberText("SHOT");
            m_shotUI.Flash();
        }

    }
}
