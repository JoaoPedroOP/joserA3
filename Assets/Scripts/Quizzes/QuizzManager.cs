using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizzManager : MonoBehaviour
{
    public CanvasGroup quizz;
    public List<QuestionAndAnswer> QuestionAndAnswers;
    public GameObject questionTab;
    public GameObject[] options;
    public GameObject responseInformation;
    public int currentQuestionIndex;
    public int index = 0;
    public int questionsRight = 0;

    public Text QuestionText;

    public ParticleSystem Rain;
    public ParticleSystem Bees;
    public int acidRainValue = 50;
    public CanvasGroup acidWaterResource;
    public Sprite acidRainBg;

    private void Start()
    {
        this.Rain.Stop();
        this.Bees.Stop();
        responseInformation.SetActive(false);
        //control to define when to generate the questions
        generateQuestion();
    }

    public void generateQuestion()
    {
        //show the quizz
        showQuizzComponents(true);

        //only generate questions if there's one available
        if (QuestionAndAnswers.Count > 0)
        {
            //as the current question is used and it's erased, no need to update index
            currentQuestionIndex = index;

            QuestionText.text = QuestionAndAnswers[currentQuestionIndex].Question;

            //put the answers of the questions on the btns
            SetAnswers();

            //show question tab and answer buttons
            ShowquestionTabAndAnswerBtns(true);
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
    }

    public IEnumerator ShowInfoAboutQuestion(bool isCorrect)
    {
        //hide question tab and answer buttons
        ShowquestionTabAndAnswerBtns(false);

        if (isCorrect)
        {
            //show the information associated to the answer;
            responseInformation.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = QuestionAndAnswers[index].AnswerInformation;

            this.Correct();
        }

        //hide the quizz gameObject
        showQuizzComponents(false);

        // Wait for 1 minute to get the next(if got right) or same(if got wrong) question
        yield return new WaitForSeconds(30f);

        this.generateQuestion();
    }

    private void showQuizzComponents(bool isEnabled)
    {
        if (isEnabled)
        {
            quizz.alpha = 1f;
        }

        var components = gameObject.GetComponents<Image>();

        foreach (var component in components)
        {
            component.enabled = isEnabled;
        }
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

    public IEnumerator MakeAcidRain(bool isArid = true)
    {   
        //give acid Rain Resources
        // updating the acid rain quantity and enabling the resource on the manager
        acidWaterResource.alpha = 1f;
        var newQuantity = ResourceManager.Instance.UpdateByName(ResourceType.AcidWater, acidRainValue);
        var acidWaterQuantityText = acidWaterResource.GetComponentsInChildren<TMP_Text>();
        var button = acidWaterResource.GetComponentInChildren<Button>();
        button.interactable = true;
        acidWaterQuantityText[1].text = $"{newQuantity}";
        if(isArid)
        {
            GameObject.Find("Panel").GetComponent<Image>().sprite = acidRainBg;
        }

        this.Rain.Play();
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        this.Rain.Stop();
    }

    internal IEnumerator MakeBees()
    {
        this.Bees.Play();
        // Wait for 15 seconds
        yield return new WaitForSeconds(15f);
        this.Bees.Stop();
    }
}
