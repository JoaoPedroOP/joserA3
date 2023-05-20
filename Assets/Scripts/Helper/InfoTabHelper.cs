using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfoTabHelper : MonoBehaviour
{
    public static InfoTabHelper Instance = null;
    public Image infoTab;
    public Text infoText;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowInfo(string text)
    {
        this.infoTab.enabled = true;
        this.infoText.enabled = true;
        var infoText = this.infoTab.GetComponentInChildren<Text>();
        infoText.text = text;

        // Wait for 1.5 seconds
        yield return new WaitForSeconds(1.5f);
        this.infoTab.enabled = false;
        this.infoText.enabled = false;
    }
}
