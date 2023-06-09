using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;

    public static int Duration = 540;

    public int remainingDuration;

    private void Start()
    {
        //se a dificuldade for f�cil 540
        //m�dio � 480
        //dif�cil � 420
      SetDuration(Duration);
    }

    private void SetDuration(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(remainingDuration >= 0)
        {
            uiText.text = $"{remainingDuration/60:00} : {remainingDuration % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0,Duration,remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        SceneManager.LoadScene("Defeat");
    }
}
