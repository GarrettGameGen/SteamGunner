using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemy;
    // Start is called before the first frame update
    private void Start() {
         EnemyManager.Instance.OnSpawnEvent += OnSpawn;
    }

    // Update is called once per frame
    void OnSpawn()
    {
        GameObject e = Instantiate(_enemy,transform.position,transform.rotation);
    }
}
