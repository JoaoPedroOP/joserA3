using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class GameController : MonoBehaviour
{
    public GameObject bankOfSeeds;

    //plants 
    public GameObject plantations;
    public GameObject spawnedImagePrefab;

    //buildings prefabs
    public GameObject windTurbinePrefab;
    //public GameObject solarPanelPrefab;
    //public GameObject waterPlantPrefab;

    //buildings
    public GameObject windTurbine;
    public GameObject solarPanel;
    public GameObject waterPlant;
    public AnswerScript answerScript;

    public Image infoTab;
    public Text infoText;
    private Image bankImage;
    public CanvasGroup acidWaterResource;
    public CanvasGroup seedResource;
    public CanvasGroup mineralsResource;
    public CanvasGroup woodResource;
    public CanvasGroup energyResource;
    public CanvasGroup cleanWaterResource;
    public GameObject quizz;
    public ParticleSystem Rain;
    public ParticleSystem Bees;

    public Vector2 scaleRange = new Vector2(1f, 100f);

    //unlockBuildings
    public bool unlockWaterPlant = false;
    public bool unlockSolarPanel = false;
    public bool unlockWindTurbine = false;

    //unlockBeesAnimalsFruitTrees
    public bool unlockBees = false;
    public bool unlockAnimals = false;
    public bool unlockFruitTrees = false;

    // Start is called before the first frame update
    void Start()
    {
        this.Rain.Stop();
        this.Bees.Stop();

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

        var windTurbineBtn = windTurbine.GetComponentInChildren<Button>();
        var windTurbineImage = windTurbineBtn.GetComponentInChildren<UnityEngine.UI.Image>();

        var solarPanelBtn = solarPanel.GetComponentInChildren<Button>();
        var solarPanelImage = solarPanelBtn.GetComponentInChildren<UnityEngine.UI.Image>();

        var waterPlantBtn = waterPlant.GetComponentInChildren<Button>();
        var waterPlantImage = waterPlantBtn.GetComponentInChildren<UnityEngine.UI.Image>();

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

        if (!hasWindTurbineResources())
        {
            windTurbineBtn.enabled = false;
            Color currentColor = windTurbineImage.color;
            currentColor.a = 0.5f;
            windTurbineImage.color = currentColor;
        }
        else
        {
            windTurbineBtn.enabled = true;
            Color currentColor = windTurbineImage.color;
            currentColor.a = 1f;
            windTurbineImage.color = currentColor;
        }

        if (!hasSolarPanelResources())
        {
            solarPanelBtn.enabled = false;
            Color currentColor = solarPanelImage.color;
            currentColor.a = 0.5f;
            solarPanelImage.color = currentColor;
        }
        else
        {
            solarPanelBtn.enabled = true;
            Color currentColor = solarPanelImage.color;
            currentColor.a = 1f;
            solarPanelImage.color = currentColor;
        }

        if (!hasWaterPlantResources())
        {
            waterPlantBtn.enabled = false;
            Color currentColor = waterPlantImage.color;
            currentColor.a = 0.5f;
            waterPlantImage.color = currentColor;
        }
        else
        {
            waterPlantBtn.enabled = true;
            Color currentColor = waterPlantImage.color;
            currentColor.a = 1f;
            waterPlantImage.color = currentColor;
        }
    }

    private bool hasWaterPlantResources()
    {
        if(!unlockWaterPlant)
            return false;

        //wood, minerals, energy and acid water
        int woodValue = 1;
        int mineralsValue = 1;
        int energyValue = 1;
        int acidWaterValue = 1;

        var currentWoodValue = ResourceManager.Instance.
            GetResourceByType(ResourceType.Wood);
        var currentMineralValue = ResourceManager.Instance.
            GetResourceByType(ResourceType.Minerals);
        var currentEnergyValue = ResourceManager.Instance.
            GetResourceByType(ResourceType.Energy);
        var currentAcidWater = ResourceManager.Instance.
            GetResourceByType(ResourceType.AcidWater);

        if (currentWoodValue.Quantity >= woodValue
            && currentMineralValue.Quantity >= mineralsValue
            && currentEnergyValue.Quantity >= energyValue
            && currentAcidWater.Quantity >= acidWaterValue)
        {
            return true;
        }

        return false;
    }

    private bool hasSolarPanelResources()
    {
        if (!unlockSolarPanel)
            return false;

        //wood and minerals
        int woodValue = 2;
        int mineralsValue = 3;

        var currentWoodValue = ResourceManager.Instance.
            GetResourceByType(ResourceType.Wood);
        var currentMineralValue = ResourceManager.Instance.
            GetResourceByType(ResourceType.Minerals);

        if (currentWoodValue.Quantity >= woodValue && currentMineralValue.Quantity >= mineralsValue)
        {
            return true;
        }

        return false;
    }

    private bool hasWindTurbineResources()
    {
        if (!unlockWindTurbine)
            return false;

        //wood and minerals
        int woodValue = 1;
        int mineralsValue = 1;

        var currentWoodValue = ResourceManager.Instance.
            GetResourceByType(ResourceType.Wood);
        var currentMineralValue = ResourceManager.Instance.
            GetResourceByType(ResourceType.Minerals);

        if (currentWoodValue.Quantity >= woodValue && currentMineralValue.Quantity >= mineralsValue)
        {
            return true;
        }

        return false;
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

    //TODO check, not working :(
    public void ClickSmallPlant()
    {
        var woodValue = 1;

        //message appears indicating that some resources were gained
        var text = "You gained " + woodValue + " wood!" + "\n" + "Good Luck!";

        woodResource.alpha = 1f;
        var newQuantity = ResourceManager.Instance.UpdateByName(ResourceType.Wood, woodValue);
        var woodQuantityText = woodResource.GetComponentsInChildren<TMP_Text>();
        var woodBtnResource = woodResource.GetComponentInChildren<Button>();
        woodBtnResource.interactable = true;

        woodQuantityText[1].text = $"{newQuantity}";

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));
    }

    public void clickBankOfResources()
    {
        int seedValue = 50;

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

    public void clickWindTurbineButton()
    {
        //consume 1 wood and 1 mineral
        int woodValue = -1;
        int mineralValue = -1;

        //message appears indicating that some resources were spent
        string text = "You Spent " + Math.Abs(woodValue) + " Wood!" + "\n"
            + "And " + Math.Abs(mineralValue) + " Minerals!";

        // updating the wood quantity on the manager
        var newWoodQuantity = ResourceManager.Instance.UpdateByName(ResourceType.Wood, woodValue);
        if (newWoodQuantity == 0)
        {
            woodResource.alpha = 0.5f;
        }
        var woodResourceText = woodResource.GetComponentsInChildren<TMP_Text>();

        woodResourceText[1].text = $"{newWoodQuantity}";


        //updating the mineral quantity on the manager
        var newMineralValue = ResourceManager.Instance.UpdateByName(ResourceType.AcidWater, mineralValue);
        if (newMineralValue == 0)
        {
            mineralsResource.alpha = 0.5f;
        }
        var mineralQuantityText = mineralsResource.GetComponentsInChildren<TMP_Text>();

        mineralQuantityText[1].text = $"{newMineralValue}";

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));

        /////spawn the wind turbine animation randomly in the map
        Vector2 randomPosition = GetRandomPosition();
        Instantiate(windTurbinePrefab, randomPosition, Quaternion.identity, transform.parent);


        //ADD 3 ENERGY
        int energyValue = 3;
        text = "You Gained " + Math.Abs(energyValue) + " Energy Points!";

        // updating the wood quantity on the manager
        var newEnergyQtd = ResourceManager.Instance.UpdateByName(ResourceType.Energy, energyValue);
        if (newEnergyQtd == 0)
        {
            woodResource.alpha = 0.5f;
        }
        var energyResourceTxt = energyResource.GetComponentsInChildren<TMP_Text>();

        energyResourceTxt[1].text = $"{newEnergyQtd}";

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text)); //-> only this message is shown -_-
        /////////
    }

    public void clickSolarPanelButton()
    {

    }

    public void clickWaterPlantButton()
    {

    }

    private Vector2 GetRandomPosition()
    {
        float x = UnityEngine.Random.Range(-10f, 870/4f); 
        float y = UnityEngine.Random.Range(0f, 470/4f);
        return new Vector2(x, y);
    }
}
