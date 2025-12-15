using UnityEngine;

public class CoinCollectable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        GameManager.Instance.CollectCoin(gameObject);
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlayCoinSound();

        Destroy(gameObject);
    }
}
