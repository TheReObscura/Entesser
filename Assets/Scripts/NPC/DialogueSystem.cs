using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public GameObject panel;
    public TextMeshProUGUI text;

    private string[] lines;
    private int index;
    public bool entered = false;

    private void Awake()
    {
        Instance = this;
        Debug.Log("DialogueSystem ready");
    }

    public void StartDialogue(string[] dialogueLines)
    {
        entered = true;
        lines = dialogueLines;
        index = 0;

        panel.SetActive(true);
        ShowLine();
    }

    public void Next()
    {
        index++;

        if (index >= lines.Length)
        {
            entered=false;
            panel.SetActive(false);
            return;
        }

        ShowLine();
    }

    void ShowLine()
    {
        text.text = lines[index];
    }
}