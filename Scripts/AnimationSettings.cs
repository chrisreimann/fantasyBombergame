using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimationSettings : MonoBehaviour
{

    // Animation Controllers
    public Animator animator;
    public int classnumber;


    void Start()
    {

    }

    void Update()
    {
        
    }

    public IEnumerator ChangeAnimatorState(int num)
    {
        yield return new WaitForSeconds(1f);
        animator.SetInteger("Class", num);
    }

    public void SetAnimatorState(int num)
    {
        animator.SetInteger("Class", num);
    }
}
