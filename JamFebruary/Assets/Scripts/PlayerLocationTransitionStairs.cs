using UnityEngine;
using UnityEngine.UI;

public class PlayerLocationTransitionStairs : MonoBehaviour
{
    [Header("Destinations")]
    [SerializeField] private Transform _goUpLocation;
    [SerializeField] private Transform _goDownLocation;

    [SerializeField] private GameObject _confirmationMenu;
    private Transform _buttonsStack;
    private Button _buttonUp;
    private Button _buttonDown;
    private Button _buttonCancel;

    private Transform _playerInside;

    private void Awake()
    {
        if (_confirmationMenu == null)
        {
            print("No TransitionConfirmation menu!");
            return;
        }

        _buttonsStack = _confirmationMenu.transform.Find("Buttons");

        _buttonUp = _buttonsStack.Find("ButtonUp").GetComponent<Button>();
        _buttonDown = _buttonsStack.Find("ButtonDown").GetComponent<Button>();
        _buttonCancel = _buttonsStack.Find("ButtonCancel").GetComponent<Button>();

        _confirmationMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        _playerInside = collision.transform;

        ShowMenu();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        _playerInside = null;
        _confirmationMenu.SetActive(false);
    }

    private void ShowMenu()
    {
        _confirmationMenu.SetActive(true);

        //Clear button actions
        _buttonUp.onClick.RemoveAllListeners();
        _buttonDown.onClick.RemoveAllListeners();
        _buttonCancel.onClick.RemoveAllListeners();

        //UP
        if (_goUpLocation != null)
        {
            _buttonUp.interactable = true;
            _buttonUp.onClick.AddListener(() =>
            {
                MovePlayer(_goUpLocation);
            });
        }
        else
        {
            _buttonUp.interactable = false;
        }

        //DOWN
        if (_goDownLocation != null)
        {
            _buttonDown.interactable = true;
            _buttonDown.onClick.AddListener(() =>
            {
                MovePlayer(_goDownLocation);
            });
        }
        else
        {
            _buttonDown.interactable = false;
        }

        //CANCEL
        _buttonCancel.onClick.AddListener(() =>
        {
            _confirmationMenu.SetActive(false);
        });
    }

    private void MovePlayer(Transform target)
    {
        if (_playerInside != null)
        {
            _playerInside.position = target.position;
        }

        _confirmationMenu.SetActive(false);
    }
}