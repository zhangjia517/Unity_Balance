using UnityEngine;
using System.Collections;

public class GameSceneController : MonoBehaviour
{
    public static GameSceneController self;
    public static int g_CurStage = 1;
    public static int g_CurStarCount = 0;

    public Transform[] m_StarPositons;
    public GameObject m_Star = null;
    public float m_Speed = 0.1f;
    public GameObject m_PreBird = null;
    public static int curBirdIndex = 0;
    public STATE curState = STATE.RUN;
    public bool isExistStar = false;

    public enum STATE
    {
        RUN,
        PAUSE,
        GAME_OVER,
    }

    void Awake()
    {
        self = this;
        curState = STATE.RUN;
        BirdController.curUsePaths = new ArrayList();
    }

    void Start()
    {
        InvokeRepeating("InstanceBird", 0f, 1f);
    }

    void Update()
    {
        if (Random.Range(0, 500) == 1)
        {
            CreateStar();
        }
    }

    private void CreateStar()
    {
        if (isExistStar == false)
        {
            GameObject tStar = Instantiate(m_Star) as GameObject;
            tStar.transform.position = m_StarPositons[Random.Range(0, m_StarPositons.Length)].position;
            isExistStar = true;
        }
    }

    void InstanceBird()
    {
        curBirdIndex++;
        GameObject tBird = Instantiate(m_PreBird) as GameObject;
        tBird.name = "Bird" + curBirdIndex;
    }

    public void GameOver()
    {
        Invoke("GameOverLogic", 0.5f);
    }

    void GameOverLogic()
    {
        Time.timeScale = 0;
        GameSceneController.self.curState = GameSceneController.STATE.GAME_OVER;
        GameSceneUIManager.self.GameOverShow();
    }
}