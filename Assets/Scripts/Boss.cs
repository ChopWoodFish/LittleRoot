using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Boss : MonoBehaviour
{
    int health = 100;
    int spikeIndicatorDuration = 2;
    int spikeDuration = 2;
    int spikeCooldown = 7;

    IEnumerator CastEarthSpikeSpell(GameObject enemy)
    {
        while (health > 0)
        {
            var position = enemy.transform.position;
            var rotation = enemy.transform.rotation;

            // calculate the position
            position.y -= 1;

            var spikeIndicator = Object.Instantiate(Resources.Load(ConstValue.EarthSpikeIndicator), position, rotation);
            if (spikeIndicator == null)
            {
                Debug.Log("instantiate Pre-spike failed");
            }
            yield return new WaitForSeconds(spikeIndicatorDuration);

            // TODO: use preSpike.transform.position
            var spike = Object.Instantiate(Resources.Load(ConstValue.EarthSpike), position, rotation);
            GameObject.Destroy(spikeIndicator);
            yield return new WaitForSeconds(spikeDuration);

            GameObject.Destroy(spike);
            yield return new WaitForSeconds(spikeCooldown - spikeIndicatorDuration - spikeDuration);
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
    }

    void Update()
    {
    }
}
