using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatGenerator : MonoBehaviour
{

    public GameObject target;
    public GameObject bat;
    public GameObject ebat;

    GameObject b;
    GameObject c;

    LineRenderer line;
    int count = 0;
    Vector3 sPosition;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントを取得する
        this.line = GetComponent<LineRenderer>();

        //線の幅を決める
        this.line.startWidth = 0.5f;
        this.line.endWidth = 0.5f;

        //頂点の数を決める
        // this.line.positionCount = 3;
    }

    // Update is called once per frame
    void Update()
    {

        int middlePoints = 10;
        int totalPoints = middlePoints + 2;

        if (Input.GetKeyDown(KeyCode.Q) && count < 2)
        {
            count++;

            if (count == 1)
            {
                b = Instantiate(bat) as GameObject;
                b.transform.position = target.transform.position;
                sPosition = b.transform.position;
            }

            if (count >= 2)
            {

                c = Instantiate(ebat) as GameObject;
                c.transform.position = target.transform.position;

                Coroutine cor = StartCoroutine("DelayMethod", 10);

            }

        }

        GameObject checking = GameObject.FindGameObjectWithTag("checker");

        if (sPosition.y <= c.transform.position.y)
        {
            if (checking.transform.position.y >= c.transform.position.y)
            {
                Destroy(checking);
            }
        }

        if (sPosition.y >= c.transform.position.y)
        {
            if (checking.transform.position.y <= c.transform.position.y)
            {
                Destroy(checking);
            }
        }

    }

    private IEnumerator DelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // GameObject型の配列cubesに、"box"タグのついたオブジェクトをすべて格納
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("box");
        GameObject[] ends = GameObject.FindGameObjectsWithTag("endBox");

        GameObject[] checks = GameObject.FindGameObjectsWithTag("checker");

        foreach (GameObject check in checks)
        {
            // 消す！
            Destroy(check);
        }

        foreach (GameObject cube in cubes)
        {
            // 消す！
            Destroy(cube);
        }

        foreach (GameObject end in ends)
        {
            // 消す！
            Destroy(end);
        }

        count = 0;

    }

}
