using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("References")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [Header("Properties")]
    [SerializeField] private float subtitleFadeInSpeed;

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
        _instance.StopAllCoroutines();
        _instance.StartCoroutine(_instance.ISetSubtitleForDuration(text, color, duration));
    }

    private IEnumerator ISetSubtitleForDuration(string text, Color color, float duration)
    {
        subtitleText.text = text;
        subtitleText.color = color;
        StartCoroutine(IFadeInAndMoveUpText());

        yield return new WaitForSeconds(duration);

        subtitleText.text = "";
        subtitleText.color = Color.white;
        subtitleText.alpha = 0;
    }

    private IEnumerator IFadeInAndMoveUpText()
    {
        subtitleText.alpha = 0;
        while (subtitleText.alpha < 0.99f)
        {
            subtitleText.alpha = Mathf.Lerp(subtitleText.alpha, 1, subtitleFadeInSpeed * Time.deltaTime);
            subtitleText.rectTransform.position = new Vector2(subtitleText.rectTransform.position.x, -50 + subtitleText.alpha * 100);
            yield return null;
        }
    }
}
