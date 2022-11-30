using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public List<TimelineRecord> timeLineRecords;
    public List<TimelineRecordWave> waves;
    public List<Zombie> prefabs;
    
    public float curWaveTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Zombie zombie in prefabs)
        {
            zombie.gameObject.SetActive(false);
        }

        timeLineRecords = new List<TimelineRecord>();
        waves = new List<TimelineRecordWave>();

        TimelineRecordWave testWave = new TimelineRecordWave();
        testWave.time = 0.1f;
        testWave.rate = new List<float>{ 0.3f, 0.7f };
        testWave.zombiePrefabIds = new List<int> { 0, 1 };
        testWave.zombieNumber = 10;
        waves.Add(testWave);

        TimelineRecord testRecord1 = new TimelineRecord();
        testRecord1.time = 1f;
        testRecord1.zombiePrefabId = 0;

        TimelineRecord testRecord2 = new TimelineRecord();
        testRecord2.time = 2f;
        testRecord2.zombiePrefabId = 1;

        TimelineRecord testRecord3 = new TimelineRecord();
        testRecord3.time = 4f;
        testRecord3.zombiePrefabId = 0;

        TimelineRecord testRecord4 = new TimelineRecord();
        testRecord4.time = 4.5f;
        testRecord4.zombiePrefabId = 0;

        TimelineRecord testRecord5 = new TimelineRecord();
        testRecord5.time = 6f;
        testRecord5.zombiePrefabId = 1;

        TimelineRecord testRecord6 = new TimelineRecord();
        testRecord6.time = 6f;
        testRecord6.zombiePrefabId = 1;

        //timeLineRecords.Add(testRecord1);
        //timeLineRecords.Add(testRecord2);
        //timeLineRecords.Add(testRecord3);
        //timeLineRecords.Add(testRecord4);
        //timeLineRecords.Add(testRecord5);
        //timeLineRecords.Add(testRecord6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Zombie> GetZombies(float curTime, float dt)
    {
        List<Zombie> ret = new List<Zombie>();
        List<int> ids = new List<int>();
        foreach (var timeline in timeLineRecords)
        {
            if (timeline.IsJustPassed(curTime, dt))
            {
                ids.Add(timeline.GetZombie());
            }
        }
        
        foreach (var wave in waves)
        {
            if (wave.IsJustPassed(curTime, dt))
            {
                wave.activate();
            }

            if (wave.active)
            {
                ids.AddRange(wave.GetZombieAndUpdate(dt));
            }
        }
        

        foreach (int id in ids)
        {
            if (id >= 0 && id < prefabs.Count)
            {
                ret.Add(prefabs[id]);
            }
        }

        return ret;
    }
}

public class TimelineRecord
{
    public float time;
    public int zombiePrefabId;

    public bool IsJustPassed(float curTime, float dt)
    {
        return time <= curTime && time > curTime - dt;
    }

    public int GetZombie()
    {
        return zombiePrefabId;
    }
}

public class TimelineRecordWave
{
    public int zombieNumber;
    public List<int> zombiePrefabIds;
    public List<float> rate;
    public bool active = false;

    public float time;
    public float waveDuration = 4.0f;
    public float curWaveTime;
    
    public bool IsJustPassed(float curTime, float dt)
    {
        return time <= curTime && time > curTime - dt;
    }

    public void activate()
    {
        active = true;
        curWaveTime = 0f;
    }

    public List<int> GetZombieAndUpdate(float dt)
    {
        List<int> ret = new List<int>();
        if (!active)
        {
            return ret;
        }

        float timePerZom = waveDuration / zombieNumber;
        int zomLastTick = Mathf.FloorToInt(curWaveTime / timePerZom);

        curWaveTime += dt;
        int zomThisTick = Mathf.FloorToInt(curWaveTime / timePerZom);
        zomThisTick = Mathf.Min(zomThisTick, zombieNumber);
        if (zomThisTick == zombieNumber)
        {
            active = false;
        }

        for (int i = 0; i < zomThisTick - zomLastTick; i++)
        {
            ret.Add(GetZombie());
        }

        return ret;
    }

    public int GetZombie()
    {
        if (zombiePrefabIds.Count == 0)
        {
            return -1;
        }
        float rand = Random.value;
        int i = 0;
        while (rate[i] < rand)
        {
            rand -= rate[i];
            i++;
        }

        return zombiePrefabIds[i];
    }
}