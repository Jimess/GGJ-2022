using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementController : MonoBehaviour, IJumpable {

    [Header("Player movement params")]
    [Range(5f, 25f)]
    public float speed;
    [Range(150f, 250f)]
    public float jumpForce;

    private Rigidbody2D _rb2d;

    //public delegate void OnJump();
    //public static OnJump onJump;

    private void Awake() {
        print("AWAKE");
        //subscribint delegatams
    }

    private void OnDestroy() {
        //unsubscribint delegatams
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        print("UZKROVE");
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        //transform.position.y = 25f;
        //transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
    }

    private void FixedUpdate() {
    }

    private void HandleMovement() {
        //debug
        if (Input.GetKeyDown(KeyCode.G)) {
            print(transform.position);
            //print("child: " + child.transform.position);
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

        if (Input.GetKey(KeyCode.D)) {
            _rb2d.AddForce(new Vector3(speed, 0, 0));
        } else if (Input.GetKey(KeyCode.A)) {
            //GetComponent<Rigidbody2D>().MovePosition(transform.position + new Vector3(-speed, 0, 0));
            _rb2d.AddForce(new Vector3(-speed, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            Jump();
            SoundSystem.Instance.Play();
        }


        if (Input.GetKeyDown(KeyCode.F)) {
            MoveAToBAndBack();
        }
        //GetComponent<IJumpable>().Jump();
    }

    public void Jump() {
        //throw new System.NotImplementedException();
        _rb2d.AddForce(Vector2.up * jumpForce);
    }

    public void MoveAToB() {
        transform.DOMoveX(transform.position.x + 2f, 1f).SetEase(Ease.InOutBounce);
    }

    public void MoveAToBAndBack() {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveX(transform.position.x + 2f, 1f).SetEase(Ease.InOutBounce));
        seq.AppendInterval(1f);`
        seq.Append(transform.DOMoveX(transform.position.x - 2f, 1f).SetEase(Ease.OutCirc));
        seq.Join(GetComponent<SpriteRenderer>().DOColor(Color.black, 1f));
        seq.AppendCallback(() => {
            print("BYBYS");
        });
    }
}


//public class KitaKlase : MonoBehaviour {
//    private void Awake() {
//        MovementController.onJump += OnJumpDoSomething;
//    }

//    private void OnDestroy() {
//        MovementController.onJump -= OnJumpDoSomething;
//    }

//    public void OnJumpDoSomething() {
//        print("AHA AS PASOKAU");
//    }
//}