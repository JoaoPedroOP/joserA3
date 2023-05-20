using System.Collections;
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
    public GameObject quizz;
    public ParticleSystem Rain;

    // Start is called before the first frame update
    void Start()
    {
        this.Rain.Stop();

        //Info Tab is hidden
        this.infoTab.enabled = false;
        this.infoText.enabled = false;
        this.mineralsResource.alpha = 1.0f;

        //renderer of the bank
        bankImage = this.bankOfSeeds.GetComponent<Image>();

        // quizzes
        quizz.SetActive(false);
        StartCoroutine(ShowQuizz());
    }

    public IEnumerator ShowQuizz()
    {
        yield return new WaitForSeconds(5f);
        quizz.SetActive(true);
    }

    public void clickBankOfResources()
    {
        int seedValue = 5;

        //message appears indicating that some resources were gained
        string text = "You gained " + seedValue + " Seeds!" + "\n" + "Good Luck!";

        seedResource.alpha = 1f;

        // updating the seed quantity and enabling the resource on the manager
        var newQuantity = ResourceManager.Instance.UpdateByName(ResourceType.Seeds, seedValue);
        var seedQuantityText = seedResource.GetComponentsInChildren<TMP_Text>();
        var seedBtnResource = seedResource.GetComponentInChildren<Button>();
        seedBtnResource.interactable = true;

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
