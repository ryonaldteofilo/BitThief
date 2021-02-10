using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using TMPro;

public class BustedDialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bustedDialogueText;
    [SerializeField] string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        int sizeOfSentences = sentences.Length;
        bustedDialogueText.text = sentences[Random.Range(0, sizeOfSentences)];
    }
}
