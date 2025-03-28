using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueInteract : MonoBehaviour
{
    public DialogueData[] sentences;
    public TMP_Text text;
    public float typingSpeed;
    public GameObject popup, textBox, nextIndicator;
    public Image sprite;

    int index;
    Animator anim;

#region mono

    void Start() {
        PlayerInput.InteractEvent += StartText;
        nextIndicator.SetActive(false);
        textBox.SetActive(false);
        popup.SetActive(false);

        text.text = "";

        if (gameObject.GetComponent<Animator>() != null) {
            anim = gameObject.GetComponent<Animator>();
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            popup.SetActive(true);
            other.gameObject.GetComponent<PlayerInput>().interacting = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            popup.SetActive(false);
            other.gameObject.GetComponent<PlayerInput>().interacting = false;
        }
    }

#endregion

#region typing voids

    void StartText() {
        PlayerInput.InteractEvent -= StartText;
        PlayerInput.InteractEvent += TypeSkip;

        textBox.SetActive(true);

        StartCoroutine(Typing());

        anim.SetBool("talk", true);
    }

    IEnumerator Typing() {
        sprite.sprite = sentences[index].image;

        foreach(char letter in sentences[index].text) {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        nextIndicator.SetActive(true);
    }

    void TypeSkip() {
        nextIndicator.SetActive(true);

        if (text.text != sentences[index].text) {
            StopAllCoroutines();
            text.text = sentences[index].text;
        }
        else if (text.text == sentences[index].text) {
            if (index < sentences.Length - 1) {
                nextIndicator.SetActive(false);
                index++;
                text.text = "";
                StartCoroutine(Typing());
            }
            else {
                PlayerInput.InteractEvent += StartText;
                PlayerInput.InteractEvent -= TypeSkip;

                textBox.SetActive(false);
                text.text = "";
                index = 0;

                anim.SetBool("talk", false);
            }
        }
    }

#endregion

}
