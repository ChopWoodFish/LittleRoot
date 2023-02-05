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

    public float firstFlowerHeight;
    public float secondFlowerHeight;
    public float thirdFlowerHeight;
    public int shootInterval;

    private Transform _transform;

    private float[] flowerHeights = new float[3];

    IEnumerator CastEarthSpikeSpell(GameObject enemy)
    {
        while (health > 0)
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
            var enemyPosition = enemy.transform.position;
            var enemyRotation = enemy.transform.rotation;

            // calculate the index
            var index = MinIndex(Math.Abs(enemyPosition.y - firstFlowerHeight), Math.Abs(enemyPosition.y - secondFlowerHeight), Math.Abs(enemyPosition.y - thirdFlowerHeight));

            var spikeIndicator = Instantiate(Resources.Load(ConstValue.Vine),
                new Vector2() { x = _transform.position.x, y = flowerHeights[index] },
                _transform.rotation);

            if (spikeIndicator == null)
            {
                Debug.Log("instantiate Pre-spike failed");
            }
            yield return new WaitForSeconds(spikeIndicatorDuration);

            // TODO: use preSpike.transform.position
            var spike = Instantiate(Resources.Load(ConstValue.Vine),
                new Vector2() { x = _transform.position.x, y = flowerHeights[index] },
                _transform.rotation);
            yield return new WaitForSeconds(spikeDuration);

            GameObject.Destroy(spikeIndicator);
            GameObject.Destroy(spike);
            yield return new WaitForSeconds(spikeCooldown - spikeIndicatorDuration - spikeDuration);
        }
    }
    void Awake()
    { }

    void Start()
    {
        flowerHeights[0] = firstFlowerHeight;
        flowerHeights[1] = secondFlowerHeight;
        flowerHeights[2] = thirdFlowerHeight;

        this._transform = GetComponent<Transform>();
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
