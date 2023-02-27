using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private GameObject parent;

    public void CreatePopup(string _text) {
        GameObject _popup = Instantiate(template, parent.transform.position, parent.transform.rotation);
        _popup.GetComponent<Popup>().SetText(_text);
        _popup.transform.SetParent(parent.transform);
    }
}
