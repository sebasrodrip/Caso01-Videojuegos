using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    Transform[] spawningObjects;
    [SerializeField]
    Transform[] spawningPoints;
    [SerializeField]
    float timeBetweenSpawns = 3.0F;


    float _currentTime;
    float _speedMultiplier;

    void Start()
    {
        _currentTime = timeBetweenSpawns;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;

        _speedMultiplier += Time.deltaTime * 0.1F;



        if (_currentTime >= timeBetweenSpawns)
        {
            _currentTime = 0.0F;

            int spawningIndex = Random.Range(0, spawningObjects.Length);
            Transform prefab = spawningObjects[spawningIndex];

            SpawningObjectController controller = prefab.GetComponent<SpawningObjectController>();
            int[] spawningPoints = controller.GetSpawningPoints();
            spawningIndex = spawningPoints[Random.Range(0, spawningPoints.Length)];

            foreach (Transform point in this.spawningPoints)
            {
                if (point.gameObject.name.Equals("Point " + spawningIndex.ToString()))
                {
                    Instantiate(prefab, point.position, Quaternion.identity);
                    break;
                }
            }
        }
    }

    public float GetSpeedMultiplier()
    {
        return _speedMultiplier;
    }
}
