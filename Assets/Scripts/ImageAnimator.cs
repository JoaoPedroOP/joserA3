using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : StateMachineBehaviour
{
    public Image backgroundImg;
    public Sprite[] backgroundSprites;

    private int currentSpriteIndex = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        backgroundImg.sprite = backgroundSprites[currentSpriteIndex];
        currentSpriteIndex = (currentSpriteIndex + 1) % backgroundSprites.Length;
    }
}
