using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject bankOfSeeds;
    public Image infoTab;
    public Text infoText;
    private Image bankImage;
    public CanvasGroup seedResource;
    public CanvasGroup mineralsResource;
    public Button seedBtnResource;

    // Start is called before the first frame update
    void Start()
    {
        //Info Tab is hidden
        this.infoTab.enabled = false;
        this.infoText.enabled = false;
        this.mineralsResource.alpha = 1.0f;

        //renderer of the bank
        bankImage = this.bankOfSeeds.GetComponent<Image>();
    }

    public void clickBankOfResources()
    {
        int seedValue = 5;

        //message appears indicating that some resources were gained
        string text = "You gained " + seedValue + " Seeds!" + "\n" + "Good Luck!";
        seedBtnResource.interactable = true;

        seedResource.alpha = 1f;

        // updating the seed quantity and enabling the resource on the manager
        var newQuantity = ResourceManager.Instance.UpdateByName(ResourceType.Seeds, seedValue);
        var seedQuantityText = seedResource.GetComponentsInChildren<TMP_Text>();
        seedQuantityText[1].text = $"{newQuantity}";

        // Disable the renderer to make the object invisible
        bankImage.enabled = false;

        StartCoroutine(ShowInfo(text));
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
