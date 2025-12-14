using UnityEngine;

public class CoinCollectable : MonoBehaviour, ICollectable
{
    public int coinValue = 1;

    public void Collect()
    {
        Debug.Log("Coin collected: +" + coinValue);

        // Buraya:
        GameManager.Instance.AddCoin(coinValue);
        // Ses efekti
        // Partikül

        Destroy(gameObject);
    }
}
