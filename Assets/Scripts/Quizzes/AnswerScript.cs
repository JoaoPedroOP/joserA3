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
            //0.1 = 10% 
            EnvConsciousnessManager.Instance.AddConsciousness(0.1f);

            //if the first question is correct
            if (quizzManager.questionsRight == 1)
            {
                //enable rain for some seconds
                StartCoroutine(quizzManager.MakeAcidRain());
            }

            //20%
            if(quizzManager.questionsRight == 2)
            {
                //enable wind turbine
            }

            //30%
            if(quizzManager.questionsRight==3)
            {
                //enable solar panel
            }

            //50%
            if(quizzManager.questionsRight==5)
            {
                //enable water plant
            }

            //70%
            if (quizzManager.questionsRight == 7)
            {
                //enable bees and animals
            }

            //90%
            if (quizzManager.questionsRight == 9)
            {
                //enable fruit trees
            }

            //100%
            if (quizzManager.questionsRight == 10)
            {
                //show victory panel
            }

            StartCoroutine(quizzManager.ShowInfoAboutQuestion(isCorrect));
           
            if (quizzManager.questionsRight == 9)
            {
                //enable rain for some seconds
                StartCoroutine(quizzManager.MakeBees());
            }

            
        }
        else
        {
            Debug.Log("Wrong Answer!");
            StartCoroutine(quizzManager.ShowInfoAboutQuestion(isCorrect));
        }
    }
}
