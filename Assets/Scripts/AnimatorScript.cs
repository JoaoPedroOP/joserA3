using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Animate()
    {
        // Ativa o trigger para iniciar a animação
        animator.SetTrigger("resetBackgroundAnimation");
    }
}
