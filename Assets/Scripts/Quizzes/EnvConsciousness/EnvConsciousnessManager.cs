using UnityEngine;
using UnityEngine.UI;

public class EnvConsciousnessManager : MonoBehaviour
{
    public static EnvConsciousnessManager Instance = null;
    public float Quantity = 0;
    public Slider slider;

    private void Awake()
    {
        Instance = this;
    }

    public float AddConsciousness(float quantity)
    {
        this.Quantity += quantity;
        slider.value = this.Quantity;
        StartCoroutine(InfoTabHelper.Instance.ShowInfo("Well Done! You've gained Environmental points!"));

        return this.Quantity;
    }
}
