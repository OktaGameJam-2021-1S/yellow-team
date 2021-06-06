﻿using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform castle;
    
    private const float overlapSphereRadius = 0.5f;

    private int enemyCount;
    private List<GameObject> enemies;
    private List<Transform> spawnPoints;
    private EnemyHandler enemyHandler;


    private void Awake()
    {
        if(enemyHandler==null)
            enemyHandler = GetComponent<EnemyHandler>();
    }

    public void InitSpawner(List<GameObject> enemies, List<Transform> spawnPoints, int enemyCount)
    {
        this.enemies = enemies;
        this.spawnPoints = spawnPoints;
        this.enemyCount = enemyCount;
    }

    public void SpawnEnemies() //Вообще хорошо бы просто в префабе сцены расставить противников (как в Арчеро) или места их возможного появления
    {
        if (enemies.Count == 0)
            throw new System.ArgumentNullException("enemies", "Лист префабов противников пуст");

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos;
            Quaternion spawnRot;
            //int loopBreaker = 0;

            // Spawn anywhere
            //do
            //{
            //    loopBreaker++;
            //    spawnPos = new Vector3(Random.Range(-4f,4f), 0.51f, Random.Range(2f, 13f));//Значения вытаскиваются из конструктора поля или из данных о префабе поля, если игровые поля будут разного размера
            //} while (CheckCollisions(spawnPos) && loopBreaker<100); //Может войти в бесконечный цикл, если не будет места для спавна
            //if (loopBreaker < 100)
            //{
            //    var newEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform);
            //    newEnemy.transform.position = spawnPos;
            //    enemyHandler.AddEnemy(newEnemy.GetComponent<Enemy>());
            //}


            spawnPos = spawnPoints[ i % spawnPoints.Count].position;
            spawnRot = spawnPoints[ i % spawnPoints.Count].rotation;
            bool isPlayer = i % 2 == 0 ? true : false;

            // TODO: Enemies should not be inside each other
            //do
            //{
            //    loopBreaker++;
            //    spawnPos = new Vector3(Random.Range(-4f, 4f), 0.51f, Random.Range(2f, 13f));//Значения вытаскиваются из конструктора поля или из данных о префабе поля, если игровые поля будут разного размера
            //} while (CheckCollisions(spawnPos) && loopBreaker < 100); //Может войти в бесконечный цикл, если не будет места для спавна
            //if (loopBreaker < 100)
            //{
            var newEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPos, spawnRot, transform);
            //newEnemy.transform.position = spawnPos;
            enemyHandler.AddEnemy(newEnemy.GetComponent<Enemy>());
            enemyHandler.Enemies[enemyHandler.Enemies.Count - 1].SetTarget(isPlayer, castle);
            //}

            // Set enemy target


        }
    }

    private bool CheckCollisions(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, overlapSphereRadius);
        return hitColliders.Length > 0;
    }
}
