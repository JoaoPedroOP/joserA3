using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject treePrefab;
    public GameObject treeWithFruitPrefab;

    //buildings prefabs
    public GameObject windTurbinePrefab;
    public GameObject solarPanelPrefab;
    public GameObject waterPlantPrefab;

    //buildings
    public CanvasGroup windTurbine;
    public CanvasGroup solarPanel;
    public CanvasGroup waterPlant;
    public AnswerScript answerScript;

    // animals
    public List<GameObject> animals = new List<GameObject>();

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
    public Sprite cleanWaterBg;
    public Sprite greenScenarioBg;

    public Vector2 scaleRange = new Vector2(1f, 100f);

    //unlockBuildings
    public static bool unlockWaterPlant = false;
    public static bool unlockSolarPanel = false;
    public static bool unlockWindTurbine = false;

    //unlockBeesAnimalsFruitTrees
    public bool unlockBees = false;
    public bool unlockAnimals = false;
    public bool unlockFruitTrees = false;

    public static int maxWindTurbines = 4;
    public static int maxSolarPanels = 7;
    public static int maxWaterPlants = 1;

    public Vector2[] windTurbinePositions = new Vector2[maxWindTurbines];
    public Vector2[] solarPanelsPositions = new Vector2[maxSolarPanels];

    public bool IsCleanWaterAvailable = false;

    private Button windTurbineBtn;
    private Button solarPanelBtn;

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

        windTurbineBtn = windTurbine.GetComponentInChildren<Button>();

        solarPanelBtn = solarPanel.GetComponentInChildren<Button>();

        var waterPlantBtn = waterPlant.GetComponentInChildren<Button>();

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
            windTurbineBtn.interactable = false;
            windTurbine.alpha = 0.5f;
        }
        else
        {
            windTurbineBtn.enabled = true;
            windTurbineBtn.interactable = true;
            windTurbine.alpha = 1f;
        }

        if (!hasSolarPanelResources())
        {
            solarPanelBtn.enabled = false;
            solarPanelBtn.interactable = false;
            solarPanel.alpha = 0.5f;
        }
        else
        {
            solarPanelBtn.enabled = true;
            solarPanelBtn.interactable = true;
            solarPanel.alpha = 1f;
        }

        if (!hasWaterPlantResources())
        {
            waterPlantBtn.enabled = false;
            waterPlant.alpha = 0.5f;
        }
        else
        {
            waterPlantBtn.enabled = true;
            waterPlant.alpha = 1f;
        }
    }

    private bool hasWaterPlantResources()
    {
        if(!unlockWaterPlant)
            return false;

        //wood, minerals, energy and acid water
        var woodValue = 1;
        var mineralsValue = 1;
        var energyValue = 1;
        var acidWaterValue = 1;

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
        var woodValue = 2;
        var mineralsValue = 3;

        return HasWoodAndMinerals(woodValue, mineralsValue);
    }

    private bool hasWindTurbineResources()
    {
        if (!unlockWindTurbine)
            return false;

        //wood and minerals
        var woodValue = 1;
        var mineralsValue = 1;

        return HasWoodAndMinerals(woodValue, mineralsValue);
    }

    private static bool HasWoodAndMinerals(int woodValue, int mineralsValue)
    {
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
        var seedValue = 1;
        var waterValue = 5;

        var currentAcidWater = ResourceManager.Instance.
            GetResourceByType(ResourceType.AcidWater);
        var currentCleanWater = ResourceManager.Instance.
            GetResourceByType(ResourceType.CleanWater);
        var currentSeeds = ResourceManager.Instance.
            GetResourceByType(ResourceType.Seeds);

        if(currentAcidWater.Quantity >= waterValue && currentSeeds.Quantity >= seedValue ||
            currentCleanWater.Quantity >= waterValue && currentSeeds.Quantity >= seedValue)
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
        var seedValue = 50;

        //message appears indicating that some resources were gained
        var text = "You gained " + seedValue + " Seeds!" + "\n" + "Good Luck!";
        SetResourceQuantity(seedValue, ResourceType.Seeds, this.seedResource);
        // Disable the renderer to make the object invisible
        bankImage.enabled = false;

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));
    }

    public void clickPlantButton()
    {
        //consume 1 seed and 5 waters
        var seedValue = -1;
        var waterValue = -5;

        //message appears indicating that some resources were spent
        var text = "You Spent " + Math.Abs(seedValue) + " Seeds!" + "\n"
            + "And "+ Math.Abs(waterValue) +" Waters!";

        // updating the seed quantity on the manager
        SetResourceQuantity(seedValue, ResourceType.Seeds, this.seedResource);

        var randomPosition = GetRandomPosition();

        if (!IsCleanWaterAvailable)
        {
            SetResourceQuantity(waterValue, ResourceType.AcidWater, this.acidWaterResource);

            Instantiate(spawnedImagePrefab, randomPosition, Quaternion.identity, transform.parent);
        }
        else
        {
            SetResourceQuantity(waterValue, ResourceType.CleanWater, this.cleanWaterResource);

            if (unlockFruitTrees)
            {
                Instantiate(treeWithFruitPrefab, randomPosition, Quaternion.identity, transform.parent);
            }
            else 
            {
                Instantiate(treePrefab, randomPosition, Quaternion.identity, transform.parent);
            }
        }

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));
    }

    public void clickWindTurbineButton()
    {
        if (maxWindTurbines == 0)
        {
            unlockWindTurbine = false;
            DisableBuilding(windTurbineBtn, windTurbine);
            StartCoroutine(InfoTabHelper.Instance.ShowInfo("MAX WIND TURBINES REACHED!"));
            return;
        }
        maxWindTurbines--;

        //consume 1 wood and 1 mineral
        var woodValue = -1;
        var mineralValue = -1;

        //message appears indicating that some resources were spent
        var text = "You Spent " + Math.Abs(woodValue) + " Wood!" + "\n"
            + "And " + Math.Abs(mineralValue) + " Minerals!";

        // updating the wood quantity on the manager
        SetResourceQuantity(woodValue, ResourceType.Wood, this.woodResource);

        //updating the mineral quantity on the manager
        SetResourceQuantity(mineralValue, ResourceType.Minerals, this.mineralsResource);

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));

        /////spawn the wind turbine animation randomly in the map
        var randomPosition = GetWindTurbinePosition(maxWindTurbines);
        Instantiate(windTurbinePrefab, randomPosition, Quaternion.identity, transform.parent);

        //ADD 3 ENERGY
        var energyValue = 3;
        text = "You Gained " + Math.Abs(energyValue) + " Energy Points!";

        // updating the energy quantity on the manager
        SetResourceQuantity(energyValue, ResourceType.Energy, this.energyResource);

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text)); //-> only this message is shown -_-
        /////////
    }

    public void clickSolarPanelButton()
    {
        if (maxSolarPanels == 0)
        {
            unlockSolarPanel = false;
            DisableBuilding(solarPanelBtn, solarPanel);
            StartCoroutine(InfoTabHelper.Instance.ShowInfo("MAX SOLAR PANELS REACHED!"));
            return;
        }
        maxSolarPanels--;

        //consume 5 wood and 1 mineral
        var woodValue = -5;
        var mineralValue = -1;

        //message appears indicating that some resources were spent
        var text = "You Spent " + Math.Abs(woodValue) + " Wood!" + "\n"
            + "And " + Math.Abs(mineralValue) + " Minerals!";

        // updating the wood quantity on the manager
        SetResourceQuantity(woodValue, ResourceType.Wood, this.woodResource);

        //updating the mineral quantity on the manager
        SetResourceQuantity(mineralValue, ResourceType.Minerals, this.mineralsResource);

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));

        //instantiate object on the map
        Vector2 randomPosition = GetSolarPanelPosition();
        Instantiate(solarPanelPrefab, randomPosition, Quaternion.identity, transform.parent);

        //ADD 5 ENERGY
        var energyValue = 5;
        text = "You Gained " + Math.Abs(energyValue) + " Energy Points!";

        // updating the wood quantity on the manager
        SetResourceQuantity(energyValue, ResourceType.Energy, this.energyResource);

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text)); //-> only this message is shown -_-
        /////////
    }

    private void DisableBuilding(Button buildingBtn, CanvasGroup buildingGroup)
    {
        buildingBtn.enabled = false;
        buildingBtn.interactable = false;
        buildingGroup.alpha = 0.5f;
    }

    public void ChangeSceneryToGreen()
    {
        GameObject.Find("Panel").GetComponent<Image>().sprite = this.greenScenarioBg;
    }

    public void clickWaterPlantButton()
    {
        if (maxWaterPlants == 0)
        {
            unlockWaterPlant = false;
            StartCoroutine(InfoTabHelper.Instance.ShowInfo("MAX WATER PLANTS REACHED!"));
            return;
        }
        maxWaterPlants--;

        SFXPlaying.Instance.PlayRunningWater();

        GameObject.Find("Panel").GetComponent<Image>().sprite = this.cleanWaterBg;

        //consume 5 wood, 1 mineral and 40 energy
        var woodValue = -5;
        var mineralValue = -1;
        var energyValue = -40;

        //message appears indicating that some resources were spent
        var text = "You Spent " + Math.Abs(woodValue) + " Wood!" + "\n"
            + "And " + Math.Abs(mineralValue) + " Minerals!"
            + "And" + Math.Abs(energyValue) + "Energy!";

        // updating the wood quantity on the manager
        SetResourceQuantity(woodValue, ResourceType.Wood, this.woodResource);

        // updating the energy quantity on the manager
        SetResourceQuantity(energyValue, ResourceType.Energy, this.energyResource);

        //updating the mineral quantity on the manager
        SetResourceQuantity(mineralValue, ResourceType.Minerals, this.mineralsResource);

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text));

        /////spawn the solar panel animation randomly in the map
        Vector2 randomPosition = GetWaterPlantPosition();
        Instantiate(waterPlantPrefab, randomPosition, Quaternion.identity, transform.parent);

        //Convert Acid Water to clean Water
        int acidWaterValue = ResourceManager.Instance.GetResourceByType(ResourceType.AcidWater).Quantity;
        text = "You converted all the acid water to clean water!";

        // updating the clean water quantity on the manager and the acid water
        var newCleanWaterQtd = SetResourceQuantity(acidWaterValue, ResourceType.CleanWater, this.cleanWaterResource);
        SetResourceQuantity(-newCleanWaterQtd, ResourceType.AcidWater, this.acidWaterResource);
        ResourceManager.Instance.UpdateByName(ResourceType.AcidWater,-newCleanWaterQtd);
        var acidWaterResourceTxt = acidWaterResource.GetComponentsInChildren<TMP_Text>();
        var acidWaterResourceBtn = acidWaterResource.GetComponentInChildren<Button>();

        acidWaterResourceBtn.interactable = false;
        acidWaterResourceTxt[1].text = "0";

        StartCoroutine(InfoTabHelper.Instance.ShowInfo(text)); //-> only this message is shown -_-
        /////////
    }

    private int SetResourceQuantity(int seedValue, ResourceType resource, CanvasGroup resourceGroup)
    {
        // updating the quantity and enabling the resource on the manager
        var newQuantity = ResourceManager.Instance.UpdateByName(resource, seedValue);
        if (newQuantity == 0)
        {
            resourceGroup.alpha = 0.5f;
        }
        else
        {
            resourceGroup.alpha = 1f;
        }

        var quantityText = resourceGroup.GetComponentsInChildren<TMP_Text>();
        var resourceBtn = resourceGroup.GetComponentInChildren<Button>();
        if (!resourceBtn.interactable)
        {
            resourceBtn.interactable = true;
        }

        quantityText[1].text = $"{newQuantity}";

        return newQuantity;
    }

    private Vector2 GetWaterPlantPosition()
    {
        var x = 600f;
        var y = 230f;
        return new Vector2(x, y);
    }

    private Vector2 GetSolarPanelPosition()
    {
        return this.solarPanelsPositions[maxSolarPanels];
    }

    private Vector2 GetWindTurbinePosition(int maxWindTurbines)
    {
        return this.windTurbinePositions[maxWindTurbines];
    }

    private Vector2 GetRandomPosition()
    {
        var x = UnityEngine.Random.Range(-10f, 870f); 
        var y = UnityEngine.Random.Range(0f, 470f);
        return new Vector2(x, y);
    }

    internal void MakeAnimalsAppear()
    {
        foreach (var animal in animals)
        {
            animal.SetActive(true);
        }
    }
}
