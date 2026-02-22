using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySatisfactionCounters : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _deliveryCounter;
    [SerializeField] private Slider _satisfactionSlider;
    [SerializeField] private GameObject _failScreen;

    [SerializeField] private GameObject _voidBarrier;

    private int _currentDeliveredValue;
    private int _failCount;

    private const int MAX_DELIVERIES = 4;
    private const int MAX_FAILS = 3;

    private void Awake()
    {
        _satisfactionSlider.interactable = false;
        _satisfactionSlider.value = 1f;
        _currentDeliveredValue = 0;
        _failCount = 0;

        UpdateDeliveredText();
    }

    private void UpdateDeliveredText()
    {
        _deliveryCounter.text = "Packages Delivered " + _currentDeliveredValue + "/" + MAX_DELIVERIES;
    }

    public void IncreaseDeliveryCount()
    {
        _currentDeliveredValue++;
        UpdateDeliveredText();

        if(_currentDeliveredValue >= 3)
        {
            _voidBarrier.SetActive(false);
        }
    }

    public void RegisterFailedDelivery()
    {
        _failCount++;

        float decreaseAmount = 1f / MAX_FAILS;
        _satisfactionSlider.value -= decreaseAmount;

        if (_failCount >= MAX_FAILS)
        {
            TriggerFailScreen();
        }
    }

    private void TriggerFailScreen()
    {

        if (_failScreen != null)
        {
            _failScreen.SetActive(true);
            SoundManager.instance.PlaySound(SoundManager.Sounds.PlayerFailed);
        }
    }
}