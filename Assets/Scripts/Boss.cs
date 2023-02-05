using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Boss : MonoBehaviour
{
    public int health = 100;
    public int spikeIndicatorDuration = 2;
    public int spikeDuration = 2;
    public int spikeCooldown = 7;

    public int shootInterval;

    public GameObject[] flowers = new GameObject[3];

    IEnumerator CastEarthSpikeSpell(GameObject enemy)
    {
        while (health > 0)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) > 50f)
            {
                yield return null;
            }
            else
            {
                var enemyPosition = enemy.transform.position;
                var enemyRotation = enemy.transform.rotation;

                // calculate the position
                enemyPosition.y -= 1;

                var spikeIndicator = Instantiate(Resources.Load(ConstValue.EarthSpikeIndicator), enemyPosition, enemyRotation);
                if (spikeIndicator == null)
                {
                    Debug.Log("instantiate Pre-spike failed");
                }
                yield return new WaitForSeconds(spikeIndicatorDuration);

                // TODO: use preSpike.transform.position
                var spike = Instantiate(Resources.Load(ConstValue.EarthSpike), enemyPosition, enemyRotation);
                yield return new WaitForSeconds(spikeDuration);

                GameObject.Destroy(spikeIndicator);
                GameObject.Destroy(spike);
                yield return new WaitForSeconds(spikeCooldown - spikeIndicatorDuration - spikeDuration);
            }
        }
    }

    public static int MinIndex(params float[] values)
    {
        int num = values.Length;
        if (num == 0)
        {
            throw new ArgumentException();
        }

        float num2 = values[0];
        int res = 0;
        for (int i = 1; i < num; i++)
        {
            if (values[i] < num2)
            {
                num2 = values[i];
                res = i;
            }
        }

        return res;
    }

    IEnumerator Shoot(GameObject enemy)
    {
        while (health > 0)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) > 50f)
            {
                yield return null;
            }
            else
            {
                var enemyPosition = enemy.transform.position;
                var enemyRotation = enemy.transform.rotation;

                // calculate the index
                var index = MinIndex(Math.Abs(enemyPosition.y - flowers[0].transform.position.y),
                    Math.Abs(enemyPosition.y - flowers[1].transform.position.y),
                    Math.Abs(enemyPosition.y - flowers[2].transform.position.y));

                var spikeIndicator = Instantiate(Resources.Load(ConstValue.Vine),
                    flowers[index].transform.position,
                    flowers[index].transform.rotation);

                if (spikeIndicator == null)
                {
                    Debug.Log("instantiate Pre-spike failed");
                }
                yield return new WaitForSeconds(spikeIndicatorDuration);

                // TODO: use preSpike.transform.position
                var spike = Instantiate(Resources.Load(ConstValue.Vine),
                    flowers[index].transform.position,
                    flowers[index].transform.rotation);
                yield return new WaitForSeconds(spikeDuration);

                GameObject.Destroy(spikeIndicator);
                GameObject.Destroy(spike);
                yield return new WaitForSeconds(spikeCooldown - spikeIndicatorDuration - spikeDuration);
            }
        }
    }
    void Awake()
    { }

    void Start()
    {
        GameObject enemy = GameObject.Find(ConstValue.Player);
        if (enemy == null)
        {
            Debug.Log("Player Not Initialized");
        }
        StartCoroutine(CastEarthSpikeSpell(enemy));
        StartCoroutine(Shoot(enemy));
    }

    void Update()
    {
    }
}
