using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public static InputManager self;
    public int m_AddForce = 300;
    public Rigidbody m_LeftWeight = null;
    public Rigidbody m_RightWeight = null;

    void Awake()
    {
        self = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                m_LeftWeight.AddForce(0, -m_AddForce, 0);
            }
            else
            {
                m_RightWeight.AddForce(0, -m_AddForce, 0);
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                AddLeftForce();
            }
            else
            {
                AddRightForce();
            }
        }
    }

    public void AddLeftForce()
    {
        m_LeftWeight.AddForce(0, -m_AddForce, 0);
    }

    public void AddRightForce()
    {
        m_RightWeight.AddForce(0, -m_AddForce, 0);
    }
}