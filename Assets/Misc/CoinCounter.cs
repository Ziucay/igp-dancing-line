using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int Counter = 0;

    private void OnEnable()
    {
        CoinPickup.OnPicked += IncreaseCounter;
    }

    private void OnDisable()
    {
        CoinPickup.OnPicked -= IncreaseCounter;
    }

    void IncreaseCounter()
    {
        Counter++;
        Debug.Log(Counter);
    }
}
