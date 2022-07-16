using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TextMeshProUGUI subtitleText;

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainPanel.SetActive(!mainPanel.activeInHierarchy);
        }
    }

    public static void SetSubtitle(string text, Color color, float duration)
    {
        _instance.StartCoroutine(_instance.ISetSubtitleForDuration(text, color, duration));
    }

    private IEnumerator ISetSubtitleForDuration(string text, Color color, float duration)
    {
        subtitleText.text = text;
        subtitleText.color = color;

        yield return new WaitForSeconds(duration);

        subtitleText.text = "";
        subtitleText.color = Color.white;
    }
}
