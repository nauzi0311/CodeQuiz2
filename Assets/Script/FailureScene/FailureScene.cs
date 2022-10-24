using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FailureScene : MonoBehaviour
{
    GameObject director,Source,Id,Quiz,Output;
    // Start is called before the first frame update
    void Start()
    {
        QuestData _this_quest = QuizScene.GetQuestDatas()[QuizScene._qcount++];
        Debug.Log(_this_quest.ToString());
        director = GameObject.Find("GameDirector");
        Source = GameObject.Find("Source");
        Id = GameObject.Find("Id");
        Quiz = GameObject.Find("QText");
        Output = GameObject.Find("Output");
        string _source = _this_quest.source;
        _source = _source.Replace("\t","  ");
        _source = _source.Replace("???","<color=\"red\">" + _this_quest.choice[_this_quest.answer-1] + "</color>");
        _source = _source.Replace("ffd70>","ffd700>");
        Source.GetComponent<TextMeshProUGUI>().text = _source;
        Id.GetComponent<TextMeshProUGUI>().text = _this_quest.id.ToString();
        Quiz.GetComponent<TextMeshProUGUI>().text = _this_quest.question;
        string output = _this_quest.output;
        output = output.Replace("???","<color=\"red\">" + _this_quest.choice[_this_quest.answer-1] + "</color>");
        Output.GetComponent<TextMeshProUGUI>().text = output;
    }

    public void MoveScene(){
        if(QuizScene._qcount == 7){
            GameDirector.CalculateInResult();
            director.GetComponent<GameDirector>().MoveScene("FailurePage","ResultPage");
        }else{
            director.GetComponent<GameDirector>().MoveScene("FailurePage","QuizPage");
        }
    }
}
