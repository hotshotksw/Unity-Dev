using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COR : MonoBehaviour
{
    [SerializeField] float time = 3;
    [SerializeField] bool go = false;

    Coroutine timerCoroutine;

    void Start()
    {
        timerCoroutine = StartCoroutine(Timer(time));
        StartCoroutine("StoryTime");
        StartCoroutine(WaitAction());
    }

    void Update()
    {
        //time -= Time.deltaTime;
        //if (time <= 0)
        //{
        //    time = 3;
        //    print("hello");
        //}
    }


    IEnumerator Timer(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            // check perception
            print("hello");
        }

        //yield return null;
    }

    IEnumerator StoryTime()
    {
        print("hello");
        yield return new WaitForSeconds(1);
        print("welcome to the new world");
        yield return new WaitForSeconds(1);
        print("time to die!");
        yield return new WaitForSeconds(1);

        StopCoroutine(timerCoroutine);

        yield return null;
    }

    IEnumerator WaitAction()
    {
        yield return new WaitUntil(() => go);
        print("go");
        yield return null;
    }
}
