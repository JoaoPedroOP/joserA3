using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            IncreaseEnvConsc(0.1f);

            //if the first question is correct
            if (quizzManager.questionsRight == 1)
            {
                EnableAcidRain(0);
            }

            //20%
            if(quizzManager.questionsRight == 2)
            {
                EnableWindTurbine();
            }

            //30%
            if(quizzManager.questionsRight==3)
            {
                EnableAcidRain(75);
                EnableSolarPanel();                
            }

            //50%
            if(quizzManager.questionsRight==5)
            {
                UnlockWaterPlant();
            }

            //60%
            if (quizzManager.questionsRight == 6)
            {
                changeScenario();
            }

            //60%
            if (quizzManager.questionsRight == 7)
            {
                EnableRain();
            }

            //80%
            if (quizzManager.questionsRight == 8)
            {
                EnableAnimals();
            }

            //90%
            if (quizzManager.questionsRight == 9)
            {
                EnableFruitTreesAndBees();
            }

            //100%
            if (quizzManager.questionsRight == 10)
            {
                showVictoryStatus();
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

    private void IncreaseEnvConsc(float value)
    {
        EnvConsciousnessManager.Instance.AddConsciousness(value);
    }

    private void EnableSolarPanel()
    {
       GameController.unlockSolarPanel = true;
    }

    private void EnableAcidRain(int value)
    {
        if (value != 0)
        {
            StartCoroutine(quizzManager.MakeAcidRain(value));
        }
        else
        {
            StartCoroutine(quizzManager.MakeAcidRain());
        }
    }

    private void EnableWindTurbine()
    {
        GameController.unlockWindTurbine = true;
    }

    private void UnlockWaterPlant()
    {
        GameController.unlockWaterPlant = true;
        //enable water plant
        foreach (GameController controller in gameControllers)
        {
            controller.IsCleanWaterAvailable = true;
        }
    }

    private void changeScenario()
    {
        foreach (GameController controller in gameControllers)
        {
            controller.ChangeScenaryToGreen();
        }
    }

    private void EnableRain()
    {
        StartCoroutine(quizzManager.MakeRain());
    }

    private void EnableAnimals()
    {
        foreach (GameController controller in gameControllers)
        {
            controller.MakeAnimalsAppear();
            controller.unlockBees = true;
            controller.unlockAnimals = true;
        }
    }

    private void EnableFruitTreesAndBees()
    {
        StartCoroutine(quizzManager.MakeBees());
        foreach (GameController controller in gameControllers)
        {
            controller.unlockFruitTrees = true;
            //enable rain for some seconds
            StartCoroutine(quizzManager.MakeRain());
        }
    }

    private void showVictoryStatus()
    {
        StartCoroutine(InfoTabHelper.Instance.ShowInfo("You did it!"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
