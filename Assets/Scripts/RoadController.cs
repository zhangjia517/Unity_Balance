using UnityEngine;
using System.Collections;

public class RoadController : MonoBehaviour
{
    private bool isCreate = false;

    void Start()
    {

    }

    void Update()
    {
        if (GameSceneController.self.curState == GameSceneController.STATE.RUN)
        {
            this.transform.Translate(Vector3.back * GameSceneController.self.m_Speed);

            if (this.transform.position.z < -70)
            {
                CreateNewRoad();
            }

            if (this.transform.position.z < -80)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void CreateNewRoad()
    {
        if (isCreate) return;
        isCreate = true;
        GameObject tPrefab = Resources.Load("Road") as GameObject;
        GameObject tNewRoad = Instantiate(tPrefab) as GameObject;
        tNewRoad.transform.position = new Vector3(0, -5, 356);
        tNewRoad.transform.localScale = new Vector3(1, 1, 150);
    }
}