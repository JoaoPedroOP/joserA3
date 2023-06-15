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
            Quantity = 50,
        },
        new ResourceModel
        {
            Id = 1,
            Type = ResourceType.AcidWater,
        },
        new ResourceModel
        {
            Id = 2,
            Type = ResourceType.Seeds
        },
        new ResourceModel
        {
            Id = 3,
            Type = ResourceType.Wood
        },
        new ResourceModel
        {
            Id = 4,
            Type = ResourceType.Energy,
            Quantity = 10
        },
        new ResourceModel
        {
            Id = 5,
            Type = ResourceType.CleanWater
        },
    };

    private void Awake()
    {
        Instance = this;
    }


    public int UpdateByName(ResourceType type, int quantity)
    {
        var existingResource = this.GetResourceByType(type);

        existingResource.Quantity += quantity;
        SetActiveResource(existingResource);

        return existingResource.Quantity;
    }

    public void SetActiveResource(ResourceModel resource)
    {
        resource.IsActive = true;
    }

    public ResourceModel GetResourceByType(ResourceType type)
    {
        return Resources.FirstOrDefault(r => r.Type == type);
    }
}
