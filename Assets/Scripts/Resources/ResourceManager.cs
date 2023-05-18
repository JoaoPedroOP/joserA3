using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance = null;

    public HashSet<ResourceModel> Resources = new HashSet<ResourceModel>
    {
        new ResourceModel
        {
            Id = 0,
            Type = ResourceType.Minerals,
            Quantity = 10,
        },
        new ResourceModel
        {
            Id = 0,
            Type = ResourceType.AcidWater,
        },
        new ResourceModel
        {
            Id = 0,
            Type = ResourceType.Seeds
        },
        new ResourceModel
        {
            Id = 0,
            Type = ResourceType.Wood
        },
    };

    private void Awake()
    {
        Instance = this;
    }


    public int UpdateByName(ResourceType type, int quantity)
    {
        var existingResource = Resources.FirstOrDefault(r => r.Type == type);

        existingResource.Quantity += quantity;
        SetActiveResource(existingResource);

        return existingResource.Quantity;
    }

    public void SetActiveResource(ResourceModel resource)
    {
        resource.IsActive = true;
    }
}
