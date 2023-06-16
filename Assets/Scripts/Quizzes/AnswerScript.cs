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
            SFXPlaying.Instance.PlayCorrectAnswer();

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
                GameController.unlockWindTurbine = true;
            }

            //30%
            if(quizzManager.questionsRight==3)
            {
                //enable rain for some seconds
                StartCoroutine(quizzManager.MakeAcidRain(75));
                //enable solar panel
                
                GameController.unlockSolarPanel = true;
            }

            //50%
            if(quizzManager.questionsRight==5)
            {
                    GameController.unlockWaterPlant = true;
                //enable water plant
                foreach (GameController controller in gameControllers)
                {
                    controller.IsCleanWaterAvailable = true;
                }
            }

            //60%
            if (quizzManager.questionsRight == 6)
            {
                //change scenario
                foreach (GameController controller in gameControllers)
                {
                    controller.ChangeScenaryToGreen();
                }
            }

            //60%
            if (quizzManager.questionsRight == 7)
            {
                //enable rain for some seconds
                StartCoroutine(quizzManager.MakeRain());
            }

            //80%
            if (quizzManager.questionsRight == 8)
            {
                //enable animals
                foreach (GameController controller in gameControllers)
                {
                    controller.MakeAnimalsAppear();
                    controller.unlockBees = true;
                    controller.unlockAnimals = true;
                }
            }

            //90%
            if (quizzManager.questionsRight == 9)
            {
                //enable fruit trees and bees
                StartCoroutine(quizzManager.MakeBees());
                foreach (GameController controller in gameControllers)
                {
                    controller.unlockFruitTrees = true;
                    //enable rain for some seconds
                    StartCoroutine(quizzManager.MakeRain());
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
            SFXPlaying.Instance.PlayWrongAnswer();
            StartCoroutine(quizzManager.ShowInfoAboutQuestion(isCorrect));

           // quizzManager.questionsWrong++;
           // remove environmental points??? TODO
        }
    }
}
