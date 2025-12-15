using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public Image icon;
    public Sprite soundOn;
    public Sprite soundOff;

    void Start()
    {
        UpdateIcon();
    }

    public void ToggleSound()
    {
        MusicManager.Instance.ToggleMute();
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (MusicManager.Instance.IsMuted())
            icon.sprite = soundOff;
        else
            icon.sprite = soundOn;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
    }
}
