using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour
{
    public static ArrayList curUsePaths = new ArrayList();

    private Rigidbody m_ConnectedBody;
    private int m_DelayTime;
    private FixedJoint m_CurFixedJoint;
    private int m_FixedCount = 0;
    private int tPathIndex;
    private RaycastHit hit;
    private int m_Probability;

    void Start()
    {
        switch (GameSceneController.g_CurStage)
        {
            case 1:
                m_Probability = 10;
                break;
            case 2:
                m_Probability = 20;
                break;
            case 3:
                m_Probability = 40;
                break;
            case 4:
                m_Probability = 60;
                break;
            case 5:
                m_Probability = 80;
                break;
            default:
                break;
        }
        if (Random.Range(0, 100) < m_Probability)
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
            this.transform.GetComponent<Rigidbody>().mass = 1.9f;
        }

        m_DelayTime = Random.Range(3, 11);
        tPathIndex = Random.Range(1, 7);
        if (!curUsePaths.Contains(tPathIndex))
        {
            curUsePaths.Add(tPathIndex);
            m_ConnectedBody = GameObject.Find("Player/Balance").GetComponent<Rigidbody>();

            this.gameObject.transform.position = iTweenPath.GetPath("Path" + tPathIndex)[0];
            iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath("Path" + tPathIndex),
                                                       "time", 3f,
                                                       "oncomplete", "OnMoveComplete",
                                                       "easetype", "linear"));
        }
        else
        {
            GameSceneController.curBirdIndex--;
            Destroy(this.gameObject);
        }
    }

    void OnMoveComplete()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);

        if (Random.Range(0, 10) == 1)
        {
            iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath("Path11"),
                                                         "time", 3f,
                                                         "oncomplete", "OnBirdOver",
                                                         "easetype", "linear"));
        }
        else if (Physics.Raycast(transform.position, down, out hit, 100))
        {
            if (hit.transform.name == "Balance")
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath("Path11"),
                                                  "time", 3f,
                                                  "oncomplete", "OnBirdOver",
                                                  "easetype", "linear"));
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == m_ConnectedBody.gameObject && m_FixedCount == 0)
        {
            m_FixedCount++;
            m_CurFixedJoint = m_ConnectedBody.gameObject.AddComponent<FixedJoint>();
            m_CurFixedJoint.connectedBody = this.GetComponent<Rigidbody>();
            Invoke("DestroyFixedJoint", m_DelayTime);
        }
    }

    void DestroyFixedJoint()
    {
        Destroy(m_CurFixedJoint);
        iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath("Path11"),
                                                  "time", 3f,
                                                  "oncomplete", "OnBirdOver",
                                                  "easetype", "linear"));
    }

    void OnBirdOver()
    {
        curUsePaths.Remove(tPathIndex);
        Destroy(this.gameObject);
    }
}