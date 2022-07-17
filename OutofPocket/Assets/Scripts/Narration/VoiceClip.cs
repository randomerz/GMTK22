using UnityEngine;

[CreateAssetMenu(fileName = "VoiceClip", menuName = "Narration/VoiceClip", order = 1)]
public class VoiceClip : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] [TextArea(1, 4)] private string _subtitle;
    [SerializeField] private Color _color;
    public string Subtitle { get => _subtitle; }
    public AudioClip AudioClip { get => _audioClip; }
    public Color Color { get => _color; }
}
