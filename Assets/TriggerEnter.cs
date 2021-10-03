using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter : MonoBehaviour
{
    public UnityEvent Event;

    private void OnTriggerEnter(Collider other)
    {
        var temp = other.GetComponent<CharacterController>();
        if (temp != null)
        {
            Event?.Invoke();
            Destroy(gameObject);
        }
    }
}