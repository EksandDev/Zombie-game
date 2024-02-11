using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject[] _otherCanvases;

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
