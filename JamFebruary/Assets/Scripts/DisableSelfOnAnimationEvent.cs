using UnityEngine;

public class DisableSelfOnAnimationEvent : MonoBehaviour
{
    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
