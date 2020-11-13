﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public static int enemyCount;
    public int waveIndex = 0;
    public float enemyGrowthRate = 1.5f;
    public Text enemyUI;
    public Text waveUI;

    // Update is called once per frame
    void Start()
    {
        waveIndex = 0;
        enemyCount = 0;
    }
    void Update()
    {
        //Check if wave is over
        //Debug.Log(enemyCount);
        if (enemyCount <= 0) {
            //Start new wave
            var pos = RandomCircle(Vector3.zero, 30f);
            var rot = Quaternion.FromToRotation(Vector3.forward, Vector3.zero);
            updateWaveUI();
            //start coroutine spawning enemies
            enemyCount = 0;

            if (waveIndex < waves.Length) {
                Debug.Log("Starting wave " + waveIndex);
                for (var i = 0; i < waves[waveIndex].spawns.Length; i++) {
                    //This part looks complex but it's just looping through the num in our group structures
                    for (var j = 0; j < waves[waveIndex].spawns[i].num; j++) {
                        pos.x += Random.Range(-2f, 2f);
                        pos.z += Random.Range(-2f, 2f);
                        //If we spawn a hostile, up the enemy count
                        if (Instantiate(waves[waveIndex].spawns[i].hostile, pos, rot)) enemyCount++;
                        updateEnemyUI();
                    }

                }
                waveIndex++;
            }
        }
    }

    public void updateEnemyUI()
    {
        enemyUI.text = "Enemies: " + enemyCount;
    }

    public void updateWaveUI()
    {
        waveUI.text = "Wave: " + waveIndex;
    }

    public Vector3 RandomCircle(Vector3 center, float radius) {
        //Create random angle between 0 to 360 degrees
        var ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}
