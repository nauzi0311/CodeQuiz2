using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CourseManager : MonoBehaviour
{
    // Start is called before the first frame update
    UnityEngine.Vector2 start_delta;
    [SerializeField] int course_count;
    void Start()
    {
        start_delta = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Adjuster(int course_num){
        float sum = 0.0f;
        string _opened = "Course" + (course_num).ToString();
        float _openy = transform.Find(_opened).gameObject.GetComponent<CourseButton>().course_count * 150.0f;
        // course_num >= 1;
        for(int j = course_num; j < course_count; j++){
            string _target = "Course" + (j + 1).ToString();
            transform.Find(_target).gameObject.GetComponent<RectTransform>().localPosition += new UnityEngine.Vector3(0,-_openy,0);
        }
        for(int i = 0; i < transform.childCount; i++){
            sum += transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
        }
        if(sum > start_delta.y){
            gameObject.GetComponent<RectTransform>().sizeDelta = new UnityEngine.Vector2(start_delta.x, sum);
        }
    }
}
