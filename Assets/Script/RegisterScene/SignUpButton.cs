using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUpButton : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject director;
    void Start()
    {
        director = GameObject.Find("SceneDirector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        director.GetComponent<RegisterScene>().OnClick();
    }
}
