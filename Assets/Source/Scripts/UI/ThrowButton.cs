using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ThrowButton : MonoBehaviour
{
    public Action OnThrowButtonClicked;

    private void Start()
    {
        gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(ThrowButtonClicked);
    }

    public void ShowButton()
    {
        gameObject.SetActive(true);
    }

    private void ThrowButtonClicked()
    {
        OnThrowButtonClicked?.Invoke();
        gameObject.SetActive(false);
    }
}
