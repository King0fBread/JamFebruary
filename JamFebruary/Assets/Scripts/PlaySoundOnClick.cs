using UnityEngine;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField] private SoundManager.Sounds _buttonSound;

    public void PlayButtonSound()
    {
        SoundManager.instance.PlaySound(_buttonSound);
    }
}
