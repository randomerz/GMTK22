using UnityEngine;

[CreateAssetMenu(fileName = "VoiceClip", menuName = "Narration/VoiceClip", order = 1)]
public class VoiceClip : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private string _subtitle;
    public string Subtitle { get => _subtitle; }
    public AudioClip AudioClip { get => _audioClip; }
}
