using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGear : MonoBehaviour
{
    private float time;

    public void EnableGear()
    {
        gameObject.SetActive(true);
    }

    public void DisableGear()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.06)
        {
            time = 0;
            transform.Rotate(new Vector3(0, 0, -30f));
        }
    }
}
