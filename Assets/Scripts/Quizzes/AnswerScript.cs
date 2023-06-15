using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizzManager quizzManager;

    public List<GameController> gameControllers;
    private void Start()
    {
        // Encontrar e armazenar todos os objetos GameController na cena
        gameControllers = new List<GameController>(FindObjectsOfType<GameController>());
    }

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
                foreach (GameController controller in gameControllers)
                {
                    //enable wind turbine
                    controller.unlockWindTurbine = true;
                }
            }

            //30%
            if(quizzManager.questionsRight==3)
            {
                //enable solar panel
                foreach (GameController controller in gameControllers)
                {
                    controller.unlockSolarPanel = true;
                    //enable rain for some seconds
                    StartCoroutine(quizzManager.MakeAcidRain());
                }
            }

            //50%
            if(quizzManager.questionsRight==5)
            {
                //enable water plant
                foreach (GameController controller in gameControllers)
                {
                    controller.unlockWaterPlant = true;
                }
            }

            //80%
            if (quizzManager.questionsRight == 8)
            {
                //enable animals
                foreach (GameController controller in gameControllers)
                {
                    // TODO
                    //StartCoroutine(quizzManager.MakeAnimals());
                    controller.unlockBees = true;
                    controller.unlockAnimals = true;
                }
            }

            //90%
            if (quizzManager.questionsRight == 9)
            {
                //enable fruit trees and bees
                foreach (GameController controller in gameControllers)
                {
                    StartCoroutine(quizzManager.MakeBees());
                    controller.unlockFruitTrees = true;
                    //enable rain for some seconds
                }
            }

            //100%
            if (quizzManager.questionsRight == 10)
            {
                //show victory panel
                //showVictoryPanel()
            }

            StartCoroutine(quizzManager.ShowInfoAboutQuestion(isCorrect));
        }
        else
        {
            Debug.Log("Wrong Answer!");
            StartCoroutine(quizzManager.ShowInfoAboutQuestion(isCorrect));
        }
    }
}
