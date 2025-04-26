using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAddBoxer : MonoBehaviour
{
    public bool hasChoice = false;
    private Button _btn;
    [SerializeField] private GameObject _imgAvatar;
    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnChoiceBoxer);
    }
    private void OnChoiceBoxer()
    {
        hasChoice = !hasChoice;
        _imgAvatar.SetActive(hasChoice);
    }
}
