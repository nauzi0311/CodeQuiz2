using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class QuizScene : MonoBehaviour
{
    public static int _qcount = 0;
    // Start is called before the first frame update
    GameObject Output,qtext,display_source,director,correct_panel,incorrect_panel,Count;
    GameObject[] TestButton;
    static QuestData[] questions;

    void Start()
    {
        Debug.Log(questions[_qcount].ToString());
        director = GameObject.FindGameObjectWithTag("GameDirector");
        display_source = GameObject.FindGameObjectWithTag("Source");
        qtext = GameObject.FindGameObjectWithTag("QText");
        Output = GameObject.FindGameObjectWithTag("Output");
        correct_panel = GameObject.FindGameObjectWithTag("Correct");
        Count = GameObject.FindGameObjectWithTag("Count");
        correct_panel.SetActive(false);
        incorrect_panel = GameObject.FindGameObjectWithTag("Incorrect");
        incorrect_panel.SetActive(false);
        Invoke("SetQuestion",0.1f);
    }

    void SetQuestion(){
        string source = questions[_qcount].source;
        source = source.Replace("\t","  ");
        source = source.Replace("???","<color=\"red\">???</color>");
        source = source.Replace("ffd70>","ffd700>");
        string quiz = questions[_qcount].question;
        string output = questions[_qcount].output; 
        output = output.Replace("???","<color=\"red\">???</color>");
        display_source.GetComponent<TextMeshProUGUI>().text = source;
        qtext.GetComponent<TextMeshProUGUI>().text = quiz;
        Output.GetComponent<TextMeshProUGUI>().text = output;
    }

    public void MoveScene(int ans_num,float time = 0.5f){
        int second;
        bool correct = false;
        if(ans_num == 0){
            second = 0;
        }else if(ans_num == questions[_qcount].answer){
            correct = true;
            second = Count.transform.parent.gameObject.GetComponent<Timer>().GetCount();
            correct_panel.SetActive(true);
        }else{
            if(ans_num == 5){
                second = 61;
            }else{
                second = Count.transform.parent.gameObject.GetComponent<Timer>().GetCount();
            }
            incorrect_panel.SetActive(true);
        }
        GameDirector.UAnswer[_qcount] = ans_num;
        GameDirector.second_list[_qcount] = second;
        GameDirector.correct_list[_qcount] = correct;
        if(correct)_qcount++;
        StartCoroutine(DelayScene(correct,time));
    }

    private IEnumerator DelayScene(bool correct,float time = 1.0f){
        yield return new WaitForSeconds(0.5f);
        if(correct){
            if(_qcount == 7){
                GameDirector.CalculateInResult();
                director.GetComponent<GameDirector>().MoveScene("QuizPage","ResultPage");
            }else{
                director.GetComponent<GameDirector>().MoveScene("QuizPage","QuizPage",time);
            }
        }else{
            director.GetComponent<GameDirector>().MoveScene("QuizPage","FailurePage",time);
        }
    }

    public static void SetQuestions(QuestData[] _qs){
        questions = _qs;
    }

    public static void DeleteQuestions(){
        questions = null;
        _qcount = 0;
    }

    public static QuestData[] GetQuestDatas(){
        return questions;
    }
}
