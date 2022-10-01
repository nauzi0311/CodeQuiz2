using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CourseItem : MonoBehaviour
{
    GameObject director,image;
    public bool available = false;
    public string course;
    public int times;
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
        Debug.Log(course + "    " + times);
        if(available){
            director.GetComponent<SelectStageScene>().MoveScene("QuizPage",course,times);
        }
    }

    public void SetTitle(string _t){
        Debug.Log(_t[1..^1]);
        transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>().text = _t;
    }

    public void SetAvailable(int course_num){
        times = course_num;
        if(GameDirector.userdata.level >= course_num){
            available = true;
        }else{
            available = false;
        }
    }
}
