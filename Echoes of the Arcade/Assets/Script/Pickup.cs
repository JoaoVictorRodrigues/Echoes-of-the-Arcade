using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour, Interactable
{

    [SerializeField] Dialog dialog;
    public void Interact()
    {
       StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
