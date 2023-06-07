using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperPage : Interactable
{
    [System.Serializable]
    public class ObjectEntry
    {
        public GameObject gameObj;
        public bool isActive;
    }

    private static string interactText = "(E) Прочесть";
    public bool doorState_isOpened = false;
    public GameObject[] doorsToChangeState;
    public ObjectEntry[] objectsToChangeState;
    override public string GetInteractText()
    {
        return interactText;
    }
    public void changeText(TMP_Text text)
    {
        TMP_Text tmpText = this.gameObject.GetComponentInChildren<TMP_Text>();
        tmpText.text = text.text;
        tmpText.font = text.font;
        tmpText.fontSharedMaterial = text.fontSharedMaterial;
        tmpText.fontSize = text.fontSize;
        tmpText.lineSpacing = text.lineSpacing;
        tmpText.lineSpacingAdjustment = text.lineSpacingAdjustment;
        tmpText.textStyle = text.textStyle;
    }

    IEnumerator WaitForExit()
    {
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Escape) == true);
        Animator animator_ui = GameObject.FindWithTag("UI").GetComponent<Animator>();
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        animator_ui.SetBool("bringPage", false);
        player.isImmobile = false;
        this.GetComponent<MeshRenderer>().enabled = true;
        TMP_Text tmp_text = this.GetComponentInChildren<TMP_Text>();
        tmp_text.enabled = true;
        tmp_text.ForceMeshUpdate();
    }
    IEnumerator WaitForExitAndDoAction()
    {
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Escape) == true);
        Animator animator_ui = GameObject.FindWithTag("UI").GetComponent<Animator>();
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        animator_ui.SetBool("bringPage", false);
        player.isImmobile = false;
        this.GetComponent<MeshRenderer>().enabled = true;
        TMP_Text tmp_text = this.GetComponentInChildren<TMP_Text>();
        tmp_text.enabled = true;
        tmp_text.ForceMeshUpdate();

        foreach (GameObject door in doorsToChangeState)
        {
            Door doorScript = door.GetComponentInChildren<Door>();
            doorScript.changeDoorState(doorState_isOpened);
        }
        foreach(ObjectEntry obj in objectsToChangeState)
        {
            obj.gameObj.SetActive(obj.isActive);
        }
    }
    override public void InteractAction()
    {
        Animator animator_ui = GameObject.FindWithTag("UI").GetComponent<Animator>();
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        PaperPage pageUI = GameObject.FindWithTag("Page_UI").GetComponent<PaperPage>();
        pageUI.changeText(this.gameObject.GetComponentInChildren<TMP_Text>());
        animator_ui.SetBool("bringPage", true);
        player.isImmobile = true;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponentInChildren<TMP_Text>().enabled = false;
        if(doorsToChangeState.Length == 0)
            StartCoroutine(WaitForExit());
        else
            StartCoroutine(WaitForExitAndDoAction());
    }
    private void Start()
    {
    }
}