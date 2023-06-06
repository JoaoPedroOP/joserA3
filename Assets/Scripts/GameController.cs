using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class GameController : MonoBehaviour
{
    public GameObject bankOfSeeds;
    public GameObject plantations;
    public GameObject spawnedImagePrefab;

    public Image infoTab;
    public Text infoText;
    private Image bankImage;
    public CanvasGroup seedResource;
    public CanvasGroup mineralsResource;
    public GameObject quizz;
    public ParticleSystem Rain;
    public CanvasGroup acidWaterResource;

    public Vector2 scaleRange = new Vector2(1f, 100f);

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

    private void Update()
    {
        var plantBtn = plantations.GetComponentInChildren<Button>();
        var plantBtnImage = plantBtn.GetComponentInChildren<UnityEngine.UI.Image>();

        if (!hasPlantResources())
        {
            plantBtn.enabled = false;
            Color currentColor = plantBtnImage.color;
            currentColor.a = 0.5f;
            plantBtnImage.color = currentColor;
        }
        else
        {
            plantBtn.enabled = true;
            Color currentColor = plantBtnImage.color;
            currentColor.a = 1f;
            plantBtnImage.color = currentColor;
        }
    }

    private bool hasPlantResources()
    {
        int seedValue = 1;
        int acidWaterValue = 5;

        var currentAcidWater = ResourceManager.Instance.
            GetResourceByType(ResourceType.AcidWater);
        var currentSeeds = ResourceManager.Instance.
            GetResourceByType(ResourceType.Seeds);

        if(currentAcidWater.Quantity >= acidWaterValue && currentSeeds.Quantity >= seedValue)
        {
            return true;
        }

        return false;
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

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));
    }

    public void clickPlantButton()
    {
        //consume 1 seed and 5 waters
        int seedValue = -1;
        int waterValue = -5;

        //message appears indicating that some resources were spent
        string text = "You Spent " + Math.Abs(seedValue) + " Seeds!" + "\n"
            + "And "+ Math.Abs(waterValue) +" Waters!";

        // updating the seed quantity on the manager
        var newSeedQuantity = ResourceManager.Instance.UpdateByName(ResourceType.Seeds, seedValue);
        if(newSeedQuantity == 0)
        {
            seedResource.alpha = 0.5f;
        }
        var seedQuantityText = seedResource.GetComponentsInChildren<TMP_Text>();

        seedQuantityText[1].text = $"{newSeedQuantity}";

        var newWaterQuantity = ResourceManager.Instance.UpdateByName(ResourceType.AcidWater, waterValue);
        if (newWaterQuantity == 0)
        {
            acidWaterResource.alpha = 0.5f;
        }
        var waterQuantityText = acidWaterResource.GetComponentsInChildren<TMP_Text>();

        waterQuantityText[1].text = $"{newWaterQuantity}";

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));

        /////spawn the plant image randomly in the map
        Vector2 randomPosition = GetRandomPosition();
        Instantiate(spawnedImagePrefab, randomPosition, Quaternion.identity,transform.parent);
        
    }

    private Vector2 GetRandomPosition()
    {
        float x = UnityEngine.Random.Range(-10f, 870f); 
        float y = UnityEngine.Random.Range(0f, 470f);
        return new Vector2(x, y);
    }
}
