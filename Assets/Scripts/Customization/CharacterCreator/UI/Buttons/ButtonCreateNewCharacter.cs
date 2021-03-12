using UnityEngine;
using UnityEngine.Events;

public class ButtonCreateNewCharacter : MonoBehaviour
{
    public UnityAction onClicked;

    public void CreateNew()
	{
        onClicked?.Invoke();
    }
}