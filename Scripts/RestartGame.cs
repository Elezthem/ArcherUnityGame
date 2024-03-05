using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    [SerializeField]
    private Button restartButton = default;

    private void Start()
    {
        // ... (ваш существующий код)

        // ƒобавл€ем слушатель событи€ дл€ кнопки рестарта
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        // «десь вы можете выполнить любые необходимые действи€ перед рестартом
        // (если такие есть), а затем перезапустить сцену.
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

}
