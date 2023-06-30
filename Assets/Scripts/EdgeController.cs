using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    SessionManager _sessionManager;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            _sessionManager = SessionManager.Instance;
            _sessionManager.Player.Incrementar();
        }

    }
}
