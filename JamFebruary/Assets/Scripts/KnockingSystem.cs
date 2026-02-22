using TMPro;
using UnityEngine;

public class KnockingSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _knockingText;
    private KnockableDoor _currentKnockable;

    private static KnockingSystem _instance;
    public static KnockingSystem Instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetCurrentKnockalbe(KnockableDoor knockable)
    {
        _currentKnockable = knockable;
        _knockingText.gameObject.SetActive(true);
        _knockingText.text = "E to Knock";
    }
    public void ClearCurrentKnockable()
    {
        print("cleared");
        _currentKnockable = null;
        _knockingText.gameObject.SetActive(false);

        //SoundManager.instance.PlaySound(SoundManager.Sounds.EnvironmentDoorClosing);
    }

    public void TryToggleKnockableResident()
    {
        if(_currentKnockable != null)
        {
            _currentKnockable.ToggleResident(true);
            _knockingText.gameObject.SetActive(false);

            SoundManager.instance.PlaySound(SoundManager.Sounds.PlayerKnocking);
        }
    }

}
