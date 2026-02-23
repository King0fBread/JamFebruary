using UnityEngine;

public class DisableUIObjectOnEnable : MonoBehaviour
{
    [SerializeField] private GameObject _menuToDisable;
    private void OnEnable()
    {
        _menuToDisable.SetActive(false);
    }
    private void OnDisable()
    {
        _menuToDisable.SetActive(true);
    }
}
