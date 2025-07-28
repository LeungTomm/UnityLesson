using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int killScore;
    ScoreBoard _scoreBoard;
    // GameObject _object;

    [SerializeField] int hitPoints;

    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        // _object = GameObject.FindGameObjectWithTag("Scoreboard");  --> more specific
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"{this.name}'s current health: {hitPoints}");
        hitPoints--;
        Instantiate(hitVFX, transform.position, Quaternion.identity);

        if (hitPoints < 1)
        {
            KillEnemy();
        }
        
    }

    private void KillEnemy()
    {
        _scoreBoard.IncreaseScore(killScore);
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
