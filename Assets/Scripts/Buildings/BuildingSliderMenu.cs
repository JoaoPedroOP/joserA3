using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSliderMenu : MonoBehaviour
{
    public GameObject PanelBuildingMenu;

    public void showHideBuildingMenu()
    {
        Animator animator = PanelBuildingMenu.GetComponent<Animator>();
        if(PanelBuildingMenu != null)
        {
            bool isOpen = animator.GetBool("showBMenu");
            animator.SetBool("showBMenu", !isOpen);
        }
    }
}
