using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailBackButton : MonoBehaviour
{
    GameObject director;
    [SerializeField]
    string ToPage;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        if(ToPage == ""){
            director.GetComponent<GameDirector>().MoveScene("DetailPage",GameDirector.GetFromPage());
        }else{
            director.GetComponent<GameDirector>().MoveScene("DetailPage",ToPage);
        }
    }
}
