using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public int initialEnemiesPerWave = 3;
    public int currentEnemiesPerWave;

    public float spawnDelay = 0.5f;

    private int currentWave = 0;
    public float waveCooldown = 5.0f;

    public bool inCooldown;
    public float cooldownCounter = 0;

    public List<Enemy> currentEnemiesAlive;

    public GameObject enemyPrefab;

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI waveNumberUI;
    public TextMeshProUGUI cooldownUI;

    private void Start()
    {
        currentEnemiesPerWave = initialEnemiesPerWave;

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentEnemiesAlive.Clear();

        currentWave++;

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i =0; i < currentEnemiesPerWave; i++)
        {
            Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-1f,1f), 0f, UnityEngine.Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            Enemy enemyScript = enemy.GetComponent<Enemy>();

            currentEnemiesAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        List<Enemy> enemiesToRemove = new List<Enemy>();
        foreach (Enemy enemy in currentEnemiesAlive)
        {
            if (enemy.isDead)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (Enemy enemy in enemiesToRemove)
        {
            currentEnemiesAlive.Remove(enemy);
        }

        enemiesToRemove.Clear();

        if (currentEnemiesAlive.Count == 0 && inCooldown == false) 
        {
            StartCoroutine(WaveCooldown());
        }

        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }
        waveNumberUI.text = $"Wave Number: {currentWave}";
        cooldownUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        //waveOverUI.text = $"Wave is over";
        waveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        //waveOverUI.text = $"";
        waveOverUI.gameObject.SetActive(false);

        currentEnemiesPerWave *= 2;

        StartNextWave();
    }
}
