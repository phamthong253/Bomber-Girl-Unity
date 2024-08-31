using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject Interaction;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI interactionText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;

    void Start()
    {
        Interaction.gameObject.SetActive(false);
        dialoguePanel.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogue[index];
            }
        }
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        interactionText.transform.position = screenPos + new Vector3(0, 200, 0);
    }

    public void zeroText()
    {
        dialogueText.text = " ";
        index = 0;
        dialoguePanel.SetActive(false);
    }
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = " ";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            Interaction.gameObject.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            Interaction.gameObject.SetActive(false);
            zeroText();
        }
    }
}
