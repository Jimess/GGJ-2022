using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeSpin : MonoBehaviour
{
    public bool isSpinning;

    // Start is called before the first frame update
    void Start()
    {
        //transform.DOLocalRotate(new Vector3(0, 0, 240), 5f);
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Coroutine1() {
        yield return new WaitForSeconds(3f);
        print("čiulpk tu man");
        yield return new WaitForSeconds(1f);
        print("baigiuosi");
    }

    private IEnumerator Coroutine2() {
        while (isSpinning) {
            transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 1));
            yield return null;
        }

        transform.rotation = Quaternion.identity;
        print("sukasi");
    }

    private IEnumerator Timer() {
        while (!Input.GetKeyDown(KeyCode.H)) {
            print("LAUKIAM");
            transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 1));
            yield return null;
        }

        print("DONE NOW");
    }
}
