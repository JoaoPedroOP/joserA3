using UnityEngine;

[CreateAssetMenu(fileName = "Resource Menu Item", menuName = "Resource Menu Item")]
public class Resource : ScriptableObject
{
    public int Id;
    public string Name;
    public int Quantity;
    public bool IsActive;
    public Sprite Icon;
}
