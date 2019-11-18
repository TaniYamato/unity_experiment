using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Exp : MonoBehaviour {


    public GameObject obj_red;
    public GameObject obj_blue;
    public int repeat;
    public string file_name;
    private int direction = 2;
    private int[] trial;
    private Vector3 left = new Vector3(-0.7f,-0.3f,0.7f);
    private Vector3 right = new Vector3(0.3f, -0.3f, 0.7f);
    private int RED = 0;
    private int BLUE = 1;
    private float startTime;
    private float response_time;

    // Use this for initialization
    void Start () {
        trial = new int[direction * repeat];
        int count = 0;

        for (int i=0; i<direction; i++)
        {
            for (int j=0; j<repeat; j++)
            {
                trial[count++] = i;
            }
        }

        trial = trial.OrderBy(i => Guid.NewGuid()).ToArray();

        foreach (var test_log in trial)
        {
            Debug.Log(test_log);
        }

        obj_red.SetActive(false);
        obj_blue.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("START");
            StartCoroutine(Play());
        }
	}

    IEnumerator Play()
    {
        float[,] result = new float[trial.Length,3];
        for (int i=0; i<trial.Length; i++)
        {
            yield return new WaitForSeconds(2);

            if (trial[i] == RED) //0なら赤が左、1なら青が左
            {
                obj_red.transform.position = left;
                obj_blue.transform.position = right;
                result[i, 0] = RED;
            }
            else
            {
                obj_red.transform.position = right;
                obj_blue.transform.position = left;
                result[i, 0] = BLUE;
            }

            obj_red.SetActive(true);
            obj_blue.SetActive(true);
            startTime = Time.time;

            //Input.GetMouseButton(0)　左クリックを取得
            //Input.GetMouseButton(1)　右クリックを取得
            while (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                yield return null;
            }

            response_time = Time.time - startTime;
            result[i, 2] = response_time;

            if (Input.GetMouseButton(0) && trial[i] == RED)
            {
                Debug.Log("response : 左 corect");
                result[i,1] = 0;
            }
            else if (Input.GetMouseButton(0) && trial[i] == BLUE)
            {
                Debug.Log("response : 左 miss");
                result[i,1] = 0;
            }
            else if (Input.GetMouseButton(1) && trial[i] == BLUE)
            {
                Debug.Log("response : 右 corect");
                result[i,1] = 1;
            }
            else if (Input.GetMouseButton(1) && trial[i] == RED)
            {
                Debug.Log("response : 右 miss");
                result[i,1] = 1;
            }

            obj_red.SetActive(false);
            obj_blue.SetActive(false);
        }
        Debug.Log("END");
        CSV.Write(result, file_name);
    }
}
