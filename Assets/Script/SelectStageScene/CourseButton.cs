using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CourseButton : MonoBehaviour
{
    public int course_count;
    List<string> CourseTitle = new List<string>();
    [SerializeField] GameObject prefab_CourseItem;
    GameObject parent;
    public bool available;
    public bool is_open = false;
    Animator Icon;
    List<GameObject> objlist = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        if(!available){
            transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.8f);
        }
        for(int i = 0; i < course_count;i++){
            string item = "第" + (i+1).ToString() + "回";
            CourseTitle.Add(item);
        }
    }

    public void OnClick(){
        if(available){
            Vector3 _pos = transform.localPosition;
            float course_item_delta_y = 150.0f;
            float course_delta_y = 190.0f;
            if(!is_open){
                is_open = !is_open;
                for(int i = 0; i < CourseTitle.Count; i++){
                    objlist.Add(Instantiate(prefab_CourseItem));
                    var obj = objlist[i];
                    obj.transform.SetParent(transform.parent,false);
                    obj.GetComponent<CourseItem>().course = transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>().text;
                    obj.GetComponent<CourseItem>().SetTitle(CourseTitle[i]);
                    obj.transform.localPosition = _pos + new Vector3(0.0f,-(course_delta_y/2) - 150*(i + 1) + (course_item_delta_y/2),0);
                    obj.GetComponent<CourseItem>().SetAvailable(i + 1);
                    // if(i == 0){
                    //     obj.GetComponent<CourseItem>().available = true;
                    // }
                }
                parent.GetComponent<CourseManager>().Adjuster(1);
            }else{
                is_open = !is_open;
                for(int i = 0; i < CourseTitle.Count; i++){
                    Destroy(objlist[i]);
                }
                objlist.Clear();
                parent.GetComponent<CourseManager>().Adjuster(1);
            }
        }
    }
}
