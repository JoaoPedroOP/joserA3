using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject bankOfResources;
    public GameObject infoTab;
    private Image bankImage;


    // Start is called before the first frame update
    void Start()
    {
        //Info Tab is hidden
        this.infoTab.SetActive(false);

        //renderer of the bank
        bankImage = this.bankOfResources.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void clickBankOfResources()
    {
        int seedValue = 5;
        //message appears indicating that some resources were gained
        string text = "You gained " + seedValue + " Seeds!" + "\n" + "Good Luck!";

        // Disable the renderer to make the object invisible
        bankImage.enabled = false;

        StartCoroutine(ShowInfo(text));

        //updating the resources on the inventory
        //Update(List<Pair<inventoryResource, value>>)
    }

    public IEnumerator ShowInfo(string text)
    {
        this.infoTab.SetActive(true);
        var infoText = this.infoTab.GetComponentInChildren<Text>();
        infoText.text = text;

        // Wait for 1.5 seconds
        yield return new WaitForSeconds(1.5f);
        this.infoTab.SetActive(false);
    }

}
