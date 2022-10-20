using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    GameObject director;
    [SerializeField]
    string HerePage,ToPage;
    
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
            director.GetComponent<GameDirector>().MoveScene(HerePage,GameDirector.GetFromPage());
        }else{
            director.GetComponent<GameDirector>().MoveScene(HerePage,ToPage);
        }
    }
}
