using UnityEngine;
using System.Collections;
using System;

public class GameSceneUIManager : MonoBehaviour
{
    public static GameSceneUIManager self;
    public UIPanel m_Pl_Pause = null;
    public UIPanel m_Pl_GameOver = null;
    public UILabel m_Lbl_Time = null;
    public UIButton m_Btn_Pause = null;
    public UILabel m_Lbl_StarCount = null;
    private DateTime m_CurTime;

    void Awake()
    {
        self = this;
    }

    void Start()
    {
        m_CurTime = new DateTime();
        Time.timeScale = 1;
        ChangePanelPauseP(false);
        m_Pl_GameOver.alpha = 0;
    }

    void Update()
    {
        ShowLblTime();
    }

    public void Pause()
    {
        InputManager.self.AddLeftForce();

        if (Time.timeScale == 0)
        {
            ChangePanelPauseP(false);
            GameSceneController.self.curState = GameSceneController.STATE.RUN;
            Time.timeScale = 1;
        }
        else
        {
            ChangePanelPauseP(true);
            GameSceneController.self.curState = GameSceneController.STATE.PAUSE;
            Time.timeScale = 0;
        }
    }

    public void Replay()
    {
        Application.LoadLevel("GameScene");
    }

    void ChangePanelPauseP(bool b)
    {
        if (b)
        {
            m_Btn_Pause.gameObject.SetActive(false);
            m_Pl_Pause.alpha = 1;
        }
        else
        {
            m_Btn_Pause.gameObject.SetActive(true);
            m_Pl_Pause.alpha = 0;
        }
    }

    void ShowLblTime()
    {
        m_CurTime = m_CurTime.AddSeconds(Time.deltaTime);
        m_Lbl_Time.text = m_CurTime.ToString("mm:ss");

        if (m_CurTime.Second > 0 && m_CurTime.Second < 20)
        {
            GameSceneController.g_CurStage = 1;
        }
        if (m_CurTime.Second > 20 && m_CurTime.Second < 40)
        {
            GameSceneController.g_CurStage = 2;
        }
        if (m_CurTime.Second > 40 && m_CurTime.Second < 60)
        {
            GameSceneController.g_CurStage = 3;
        }
        if (m_CurTime.Second > 60 && m_CurTime.Second < 80)
        {
            GameSceneController.g_CurStage = 4;
        }
        if (m_CurTime.Second > 80 && m_CurTime.Second < 100)
        {
            GameSceneController.g_CurStage = 5;
        }
    }

    public void GameOverShow()
    {
        m_Btn_Pause.gameObject.SetActive(false);
        m_Pl_GameOver.alpha = 1;
    }

    public void ChangemStarCount()
    {
        m_Lbl_StarCount.text = "Star:" + GameSceneController.g_CurStarCount.ToString();
    }
}
