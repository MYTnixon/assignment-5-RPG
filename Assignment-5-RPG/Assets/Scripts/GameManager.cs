using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerController playerController;
    private HUD hud;

    public int coins;
    public int hp;
    public int enemyCount;

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
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    public void TakeDamage(int healthToDeduct)
    {
        hp -= healthToDeduct;
    }

    public void GainHealth(int healthToAdd)
    {
        hp += healthToAdd;
    }

    private void Update()
    {
        if(hp <= 0)
        {
            SceneManager.LoadScene(0);
            hp = 5;
        }
    }
}
