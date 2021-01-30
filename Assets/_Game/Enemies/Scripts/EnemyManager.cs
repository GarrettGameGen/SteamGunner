using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public event System.Action OnSpawnEvent;
    public event System.Action OnBossSpawnEvent;

    [System.NonSerialized] public int g_maxEnemySpawn;

    private static EnemyManager _instance;
    public static EnemyManager Instance { get { return _instance; } }

    [SerializeField] private int _wave;
    
    [SerializeField] private int _waveTotalTime;
    [SerializeField] private float _newSpawnTimer = 5.0f;

    private float _waveTimeEnd;
    private bool isRunning = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        StartWave();
    }

    public void StartWave() {
        if(!isRunning)
        {
            _waveTimeEnd = _waveTotalTime+Time.time;
            StartCoroutine("Spawner");
        }
    }

    private IEnumerator Spawner()
	{
        isRunning = true;
        while(_waveTimeEnd > Time.time)  
        {
            yield return new WaitForSeconds(_newSpawnTimer);
            OnSpawnEvent.Invoke();
        }
        //OnBossSpawnEvent.Invoke();
        isRunning = false;
    }

}
