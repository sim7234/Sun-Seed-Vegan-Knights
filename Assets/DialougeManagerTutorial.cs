using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeManagerTutorial : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool PlayerNotJoined = true;

    [SerializeField] private TextMeshProUGUI npcDialougeTextJoin;
    [SerializeField] private TextMeshProUGUI npcDialougeTextAttack;
    [SerializeField] private string[] npcDialougeSentences;

    private int npcIndex;

    private void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        npcDialougeTextJoin.text = "";
        npcDialougeTextAttack.text = "";

        if (PlayerNotJoined)
        {
            StartCoroutine(TypeNPCDialogueJoin());
        }
        else
        {
            StartCoroutine(TypeNPCDialogueAttack());
        }
    }

    private IEnumerator TypeNPCDialogueJoin()
    {
        foreach (char letter in npcDialougeSentences[npcIndex].ToCharArray())
        {
            npcDialougeTextJoin.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator TypeNPCDialogueAttack()
    {
        foreach (char letter in npcDialougeSentences[npcIndex].ToCharArray())
        {
            npcDialougeTextAttack.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}