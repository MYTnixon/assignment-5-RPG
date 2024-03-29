﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerController playerController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    /*public void TakeDamage(int healthToDeduct)
    {
        hp -= healthToDeduct;
    }

    public void GainHealth(int healthToAdd)
    {
        hp += healthToAdd;
    }*/

    private void Update()
    {
        if (playerController.hp <= 0)
        {
            SceneManager.LoadScene(2);
            playerController.hp = 5;
        }
    }
}
