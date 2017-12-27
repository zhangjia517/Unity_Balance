using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Transform trans;

    void Start()
    {
        trans = this.gameObject.transform;
    }

    void Update()
    {
        if ((trans.rotation.eulerAngles.z < 180 && trans.rotation.eulerAngles.z > 90) || (trans.rotation.eulerAngles.z > 180 && trans.rotation.eulerAngles.z < 270))
        {
            GameSceneController.self.GameOver();
            Destroy(trans.GetComponent<FixedJoint>());
            Destroy(trans.Find("Balance").GetComponent<BoxCollider>());
            iTween.MoveTo(trans.Find("Balance").gameObject, iTween.Hash("path", iTweenPath.GetPath("Path12"),
                                                                                                                          "time", 8f,
                                                                                                                          "easetype", "linear"));
        }
    }
}
