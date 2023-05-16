using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public List<Resource> ActiveResources = new List<Resource>();

    public Transform ResourceContent;
    public GameObject ResourceObject;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Resource resource)
    {
        SetActiveResource(resource);
        this.ActiveResources.Add(resource);
    }

    public void SetActiveResource(Resource resource)
    {
        resource.IsActive = true;
    }
}
