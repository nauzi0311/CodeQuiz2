using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailCanvas : MonoBehaviour
{
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
    }

    public void OnClick(){
        director.GetComponent<BadgeScene>().DeleteDetail();
    }
}
