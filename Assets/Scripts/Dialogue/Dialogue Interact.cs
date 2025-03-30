using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueInteract : MonoBehaviour
{
    public DialogueData[] sentences;
    public string animBoolTag;
    public TMP_Text text;
    public float typingSpeed;
    public GameObject popup, playerTextBox, npcTextBox, nextIndicator;
    public Image playerSprite, npcSprite;

    int index;
    Animator anim;

#region mono

    public virtual void Start() {
        nextIndicator.SetActive(false);
        playerTextBox.SetActive(false);
        npcTextBox.SetActive(false);
        popup.SetActive(false);

        text.text = "";

        if (gameObject.GetComponent<Animator>() != null) {
            anim = gameObject.GetComponent<Animator>();
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerInput.InteractEvent += StartText;
            popup.SetActive(true);
            other.gameObject.GetComponent<PlayerInput>().interacting = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerInput.InteractEvent -= StartText;
            popup.SetActive(false);
            other.gameObject.GetComponent<PlayerInput>().interacting = false;
        }
    }

#endregion

#region typing voids

    public virtual void StartText() {
        PlayerInput.InteractEvent -= StartText;
        PlayerInput.InteractEvent += TypeSkip;
        PlayerInput.slowPlayer = true;

        StartCoroutine(Typing());

        if (anim != null) {
        anim.SetBool(animBoolTag, true);
        }
    }

    IEnumerator Typing() {
        if (sentences[index].player) {
            playerTextBox.SetActive(true);
            npcTextBox.SetActive(false);
            playerSprite.sprite = sentences[index].image;
        }
        else {
            npcTextBox.SetActive(true);
            playerTextBox.SetActive(false);
            npcSprite.sprite = sentences[index].image;
        }
        
        foreach(char letter in sentences[index].text) {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        nextIndicator.SetActive(true);
    }

    public virtual void TypeSkip() {
        nextIndicator.SetActive(true);

        if (text.text != sentences[index].text) {
            StopAllCoroutines();
            text.text = sentences[index].text;
        }
        else if (text.text == sentences[index].text) {
            if (index < sentences.Length - 1) {
                //moves to next chunk
                nextIndicator.SetActive(false);
                index++;
                text.text = "";
                StartCoroutine(Typing());
            }
            else {
                //dialogue ends here
                PlayerInput.InteractEvent += StartText;
                PlayerInput.InteractEvent -= TypeSkip;
                PlayerInput.slowPlayer = false;

                playerTextBox.SetActive(false);
                npcTextBox.SetActive(false);
                text.text = "";
                index = 0;

                if (anim != null) {
                anim.SetBool(animBoolTag, false);
                }
            }
        }
    }

#endregion

}
