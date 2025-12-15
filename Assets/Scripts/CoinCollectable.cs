using UnityEngine;

public class CoinCollectable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        GameManager.Instance.CollectCoin(gameObject);
        MusicManager.Instance.PlayCoinSound();

        Destroy(gameObject);
    }
}
