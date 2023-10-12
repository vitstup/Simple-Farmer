using TMPro;
using UnityEngine;

public class StackUIPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private const string mainTextInfo = "Objects in stack : ";

    public virtual void Present(int objectsInStack, int maxInStack)
    {
        SetText(objectsInStack);
    }

    private void SetText(int objectsInStack)
    {
        text.text = mainTextInfo + objectsInStack;
    }
}