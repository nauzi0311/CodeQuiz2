using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceButton : MonoBehaviour
{
    GameObject director;
    [SerializeField] int return_num;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
        TextMeshProUGUI choice = GetComponentInChildren<TextMeshProUGUI>();
        if(return_num != 0){
            choice.text = QuizScene.GetQuestDatas()[QuizScene._qcount].choice[return_num-1];
        }
    }

    public void OnClick(){
        director.GetComponent<QuizScene>().MoveScene(return_num);
    }
}
