using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// To reload the scene
using UnityEngine.SceneManagement;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] Health _health;

    public Health Health => _health;

    private void Awake()
    {
        // J'inscrit OnDeath sur la fonction Death pour reload la scène
        _health.OnDeath += Death;
    }

    void Death()
    {
        //Reload de la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}