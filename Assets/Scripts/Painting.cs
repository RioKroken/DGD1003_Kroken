using UnityEngine;

public class Painting : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        GameManager.Instance.OpenPanel();
    }
}
