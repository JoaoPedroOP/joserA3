using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizzManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QuestionAndAnswers;
    public GameObject[] options;
    public int currentQuestionIndex;

    public Text QuestionText;

    private void Start()
    {
        //controll to define when to generate the questions
        generateQuestion();
    }

    void generateQuestion()
    {
        //only generate questions if there's one available
        if (QuestionAndAnswers.Count > 0)
        {
            //for now the question is random picked 
            currentQuestionIndex = Random.Range(0, QuestionAndAnswers.Count);

            QuestionText.text = QuestionAndAnswers[currentQuestionIndex].Question;

            //put the answers of the questions on the btns
            SetAnswers();
        }
        else Debug.Log("Out of Questions!");
    }

    void SetAnswers()
    {
        //we have for each option an answer that needs to be displayed
        for(int i = 0; i < options.Length; i++)
        {
            //initialize the answer of questions 
            options[i].GetComponent<AnswerScript>().isCorrect = false;

            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                QuestionAndAnswers[currentQuestionIndex].Answers[i];

            if (QuestionAndAnswers[currentQuestionIndex].CorrectAnswer==i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    //method to generate the next question
    public void correct()
    {
        QuestionAndAnswers.RemoveAt(currentQuestionIndex);
        generateQuestion();
    }
}
