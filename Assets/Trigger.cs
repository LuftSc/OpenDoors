using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [System.Serializable]
    public class AnimEntry
    {
        public string AnimatorState;
        public int State;
        public bool isBool;
    }
    public bool doAnimation = false;
    public GameObject objectToAnimate = null;
    public AnimEntry[] animatorStateList = null;
    public float animationDelay = 0f;
    public bool doSwitchActive = false;
    public GameObject[] objectsToSwitch = null;
    public float switchActiveDelay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(doAnimation && objectToAnimate)
        {
            Animator animator = objectToAnimate.GetComponent<Animator>();
            if (animator != null)
            {
                foreach (AnimEntry anim in animatorStateList)
                {
                    if(anim.isBool)
                    {
                        animator.SetBool(anim.AnimatorState, anim.State == 1);
                    }
                    else
                    {
                        animator.SetInteger(anim.AnimatorState, anim.State);
                    }
                }
            }
        }
        if(doSwitchActive && objectsToSwitch.Length != 0)
        {
            foreach (GameObject obj in objectsToSwitch)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
