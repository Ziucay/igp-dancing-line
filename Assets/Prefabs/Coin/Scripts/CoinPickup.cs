using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public delegate void CoinPickupAction();
    public static event CoinPickupAction OnPicked;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnPicked != null)
                OnPicked();
            Destroy(gameObject);
        }
    }
}
