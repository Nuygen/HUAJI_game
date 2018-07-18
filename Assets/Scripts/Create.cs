using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour {

    public GameObject []prefabs = new GameObject[4];
    private float gamehard = 5f;//游戏难度----控制物体bottle\bomb\half_life\life的生成数量.
    private float timegap = 2f; //物体出现的时间间隔.
    private float[] probility = new float[4] { 0.4f, 0.25f, 0.2f, 0.1f }; //初始物体出现的概率
    private float[] probility_add_minus = new float[4] {0.03f, 0.03f, -0.02f,-0.01f };//每增加游戏难度之后的增加或者减少的概率
    private float score_time = 0; //初始的计时器得分
    private float i = 1f;         //游戏难度水平计数

    private IEnumerator coroutine;

    private float timegaplimit = 0.4f;//生成物体的最小时间间隔
    private float step_level = 1f; //每个多少时间会增加游戏难度.

	// Use this for initialization
	void Start () {
        coroutine = BornObjects(gamehard, timegap);
        StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
        //每个几秒就会增加物体出现的时间间隔.时间间隔最低是0.1秒.
        score_time += Time.deltaTime;
        if (score_time > step_level * i && timegap >= timegaplimit)
        {
            ++i;
            timegap -= 0.02f;

            //增加木桶和炸弹出现的概率，减小生命值出现的概率
            change_probility(probility);
            StopCoroutine(coroutine);
            coroutine = BornObjects(gamehard, timegap);
            StartCoroutine(coroutine);
        }
	}

    //按照概率生成物体,包括时间间隔和游戏难度
    private IEnumerator BornObjects(float gamehard, float timegap)
    {
        int index;
        Vector2 wuti_posoition = new Vector2();
        while(true)
        {
            index = (int)choose(probility, gamehard);
            wuti_posoition.x = gameObject.transform.position.x;
            wuti_posoition.y = gameObject.transform.position.y + 3f * choose_height();

            GameObject.Instantiate(prefabs[index], wuti_posoition, Quaternion.identity);
            yield return new WaitForSeconds(timegap);
        }
    }

    //不同概率的障碍选择
    private float choose(float []probility, float gamehard)
    {
        float total = 0;
        foreach (float item in probility)
        {
            total += item;
        }
        float randomPoint = Random.value * total;
        for (int i = 0; i < probility.Length; i++)
        {
            if (randomPoint < probility[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probility[i];
            }
        }
        return probility.Length - 1;
    }

    //选择障碍出现的高度
    private float choose_height()
    {
        //整体的高度是-3到5
        float randomPoint = Random.value * 8;
        if (randomPoint < 3.3f)
            return 0;
        else if (randomPoint < 5.3f)
            return 1;
        else
            return 2;
    }

    private void change_probility(float[] probility)
    {
        for (int i = 0; i < probility.Length; i++)
        {
            probility[i] += probility_add_minus[i];
        }
    }
}
