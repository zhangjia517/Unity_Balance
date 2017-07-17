using UnityEngine;
using System.Collections;

public class StarLogic : MonoBehaviour
{

    void Start()
    {
        Invoke("DestroyThis", 5f);
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Balance")
        {
            GameSceneController.g_CurStarCount++;
            GameSceneUIManager.self.ChangemStarCount();
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        GameSceneController.self.isExistStar = false;
        Destroy(this.gameObject);
    }
}
