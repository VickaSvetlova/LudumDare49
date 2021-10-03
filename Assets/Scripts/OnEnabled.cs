using UnityEngine;
using UnityEngine.Events;

public class OnEnabled : MonoBehaviour
{
    public UnityEvent events;

    private void OnEnable()
    {
        events?.Invoke();
        Destroy(gameObject,0.01f);
    }
}