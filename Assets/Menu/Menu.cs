using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private string _firstLevel;
    [SerializeField] private Button _play;

    private void Awake()
    {
        _play.onClick.AddListener(() => SceneManager.LoadScene(_firstLevel));
    }
}
