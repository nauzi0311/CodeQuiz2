using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineDetail : MonoBehaviour
{
    GameObject Id,director;
    // Start is called before the first frame update
    void Start()
    {
        Id = transform.parent.Find("ID").gameObject;
        director = GameObject.Find("SceneDirector");
    }

    void OnClick(){
        int _id = Int32.Parse(Id.GetComponent<TextMeshProUGUI>().text);
        StartCoroutine(director.GetComponent<ResultScene>().DetailPost(_id));
    }
}
