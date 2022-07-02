using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public Transform Spawn;
    public float CreationPeriod;
    public GameObject EnemyPrefab;
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= CreationPeriod)
        {
            _timer = 0;
            Instantiate(EnemyPrefab, Spawn.position, Spawn.rotation);
        }
    }
}
