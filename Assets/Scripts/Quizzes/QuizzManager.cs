using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizzManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QuestionAndAnswers;
    public GameObject questionTab;
    public GameObject[] options;
    public GameObject responseInformation;
    public int currentQuestionIndex;
    public int index = 0;
    public int questionsRight = 0;

    public Text QuestionText;

    public ParticleSystem Rain;
    public int acidRainValue = 25;
    public CanvasGroup acidWaterResource;

    private void Start()
    {
        this.Rain.Stop();
        responseInformation.SetActive(false);
        //controll to define when to generate the questions
        generateQuestion();
    }

    void generateQuestion()
    {
        //only generate questions if there's one available
        if (QuestionAndAnswers.Count > 0)
        {
            //as the current question is used and it's erased, no need to update index
            currentQuestionIndex = index;

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
    public void Correct()
    {
        QuestionAndAnswers.RemoveAt(currentQuestionIndex);
        generateQuestion();
    }

    public IEnumerator ShowInfoAboutQuestion()
    {
        //hide question tab and answer buttons
        ShowquestionTabAndAnswerBtns(false);

        //show the information associated to the answer;
        responseInformation.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
            = QuestionAndAnswers[index].AnswerInformation;

        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        //enable the question tab and answer buttons
        ShowquestionTabAndAnswerBtns(true); 
    }

    public void ShowquestionTabAndAnswerBtns(bool state)
    {
        responseInformation.SetActive(!state);

        questionTab.GetComponent<Image>().enabled = state;
        questionTab.GetComponentInChildren<Text>().enabled = state;

        for (int i = 0; i < options.Length; i++)
        {
            this.options[i].GetComponent<Image>().enabled = state;
            this.options[i].GetComponentInChildren<TextMeshProUGUI>().enabled = state;
        }
    }

    public IEnumerator MakeRain()
    {
        this.Rain.Play();
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        this.Rain.Stop();

        //give acid Rain Resources
        // updating the acid rain quantity and enabling the resource on the manager
        acidWaterResource.alpha = 1f;
        var newQuantity = ResourceManager.Instance.UpdateByName(ResourceType.AcidWater, acidRainValue);
        var acidWaterQuantityText = acidWaterResource.GetComponentsInChildren<TMP_Text>();
        acidWaterQuantityText[1].text = $"{newQuantity}";
    }
}
