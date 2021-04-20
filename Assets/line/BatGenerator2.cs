﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatGenerator2 : MonoBehaviour
{

    public GameObject target;
    public GameObject bat;
    public GameObject ebat;

    GameObject b;
    GameObject c;

    int count = 0;
    Vector3 sPosition;

    ChangePartsSystemBehavior changePartsSystemBehavior;

    // Start is called before the first frame update
    void Start()
    {
        changePartsSystemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

        if (changePartsSystemBehavior.changeMode)
        {
            GameObject[] checks = GameObject.FindGameObjectsWithTag("checker");

            foreach (GameObject check in checks)
            {

                Destroy(check);
            }
        }




        //if (Input.GetKeyDown(KeyCode.Q) && count < 2)
        //{
        //    count++;

        //    if (count == 1)
        //    {
        //        b = Instantiate(bat) as GameObject;
        //        b.transform.rotation = target.transform.rotation;
        //        b.transform.position = target.transform.position;

        //        sPosition = b.transform.position;
        //    }

        //    if (count >= 2)
        //    {

        //        c = Instantiate(ebat) as GameObject;
        //        c.transform.rotation = target.transform.rotation;
        //        c.transform.position = target.transform.position;

        //        Coroutine cor = StartCoroutine("DelayMethod", 10);

        //    }

        //}

    }

    private IEnumerator DelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("box");
        GameObject[] ends = GameObject.FindGameObjectsWithTag("endBox");

        GameObject[] checks = GameObject.FindGameObjectsWithTag("checker");

        foreach (GameObject check in checks)
        {

            Destroy(check);
        }

        foreach (GameObject cube in cubes)
        {

            Destroy(cube);
        }

        foreach (GameObject end in ends)
        {

            Destroy(end);
        }

        count = 0;

    }

}