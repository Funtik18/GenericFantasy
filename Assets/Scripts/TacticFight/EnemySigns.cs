using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySigns : MonoBehaviour
{
    [HideInInspector]
    public TacticMove enemy;
    Button myButton;

    public GameObject Selection;

    // Start is called before the first frame update
    void Start()
    {
        Selection.SetActive(false);
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => UIController.Instance.DisableSelections());
        myButton.onClick.AddListener(() => Clicked());
        
    }

    void Clicked()
    {
        UIController.Instance.ShowAttackInfo(this);
    }
}
