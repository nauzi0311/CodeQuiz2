using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    [SerializeField] string Topage;
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        director.GetComponent<ResultScene>().MoveScene(Topage);
    }
}
