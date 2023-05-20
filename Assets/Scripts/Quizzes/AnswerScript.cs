using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizzManager quizzManager;

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer!");

            quizzManager.questionsRight++;

            //if the first question is correct
            if (quizzManager.questionsRight == 1)
            {
                //enable rain for some seconds
                StartCoroutine(quizzManager.MakeRain());
            }

            StartCoroutine(quizzManager.ShowInfoAboutQuestion());

            quizzManager.Correct();
        }
        else
        {
            Debug.Log("Wrong Answer!");
            quizzManager.Correct();
        }
    }
}
