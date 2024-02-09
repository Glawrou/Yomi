using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "AndreyNosov/Dialog", order = 1)]
public class DialogData : ScriptableObject
{
    public string Answer;
    [TextArea(10, 200)] public string Text;
    public AudioClip AudioText;
    public DialogData[] Answers;
}
