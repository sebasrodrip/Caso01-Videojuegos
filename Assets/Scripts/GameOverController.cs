using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI puntos;

    [SerializeField]
    TextMeshProUGUI logros;



    SessionManager _sessionManager;

    void Awake()
    {
        _sessionManager = SessionManager.Instance;
    }

    void Start()
    {
        int intentoActual = _sessionManager.Player.Puntos;
        int logrosActual = PlayerPrefs.GetInt("Logros", 0);

        if (intentoActual > logrosActual)
        {
            PlayerPrefs.SetInt("Logros", intentoActual);
        }
        puntos.text = intentoActual.ToString();
        logros.text = logrosActual.ToString();
    }
}
