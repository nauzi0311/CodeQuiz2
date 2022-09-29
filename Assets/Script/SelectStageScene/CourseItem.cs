using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CourseItem : MonoBehaviour
{
    GameObject director,image;
    public bool available = false;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
        image = transform.Find("Image").gameObject;
        if(!available){
            image.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.8f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        if(available){
            director.GetComponent<SelectStageScene>().MoveScene("QuizPage");
        }
    }

    public void SetTitle(string _t){
        Debug.Log(_t);
        transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>().text = _t;
    }
}
