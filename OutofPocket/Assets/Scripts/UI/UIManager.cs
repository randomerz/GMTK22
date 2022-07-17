using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("References")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private CanvasGroup subtitleCanvasGroup;
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

            GameManager._instance.pauseAnnotation.enabled = false;
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
        subtitleCanvasGroup.alpha = 0;
    }

    private IEnumerator IFadeInAndMoveUpText()
    {
        subtitleCanvasGroup.alpha = 0;
        subtitleText.alpha = 1;
        while (subtitleCanvasGroup.alpha < 0.99f)
        {
            subtitleCanvasGroup.alpha = Mathf.Lerp(subtitleCanvasGroup.alpha, 1, subtitleFadeInSpeed * Time.deltaTime);
            subtitleText.rectTransform.position = new Vector2(subtitleText.rectTransform.position.x, -50 + subtitleCanvasGroup.alpha * 50);
            yield return null;
        }
    }
}
