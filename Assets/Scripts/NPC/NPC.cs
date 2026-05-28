using UnityEngine;
using Assets.Scripts.Player;
public class NPC : MonoBehaviour
{
    [TextArea]
    public string[] dialogue;

    public void Interact()
    {
        DialogueSystem.Instance.StartDialogue(dialogue);
    }

}