using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject[] _otherCanvases;
    [SerializeField] private Slider _slider;

    private void Update()
    {
        AudioListener.volume = _slider.value;
    }

    public void Unhide()
    {
        foreach (GameObject canvas in _otherCanvases)
            canvas.SetActive(false);

        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _otherCanvases[0].SetActive(true);
        Time.timeScale = 1;
    }
}
