using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public CanvasGroup woodResource;
    public GameObject smallPlant;

    private void OnMouseDown()
    {
        Destroy(smallPlant);
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
}