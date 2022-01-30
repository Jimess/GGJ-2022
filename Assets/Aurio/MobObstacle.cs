using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MobObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public string dialogText = "You hit me!";
    public float dialogDuration = 5f;
    public float moveDuration = 5f;

    bool isMovingRight = true;

    Tween moveTween;

    float moveXMin;
    float moveXMax;

    void OnEnable()
    {
        BoxCollider2D gameBounds = FindObjectOfType<ObstacleLoaderManager>().gameBounds;
        moveXMin = gameBounds.transform.position.x - gameBounds.size.x / 2;
        moveXMax = gameBounds.transform.position.x + gameBounds.size.x / 2;

        Move();

    }

    private void OnDisable() {
        if (moveTween != null || moveTween.IsPlaying()) {
            moveTween.Kill();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Move()
    {
        float moveToX;

        if (isMovingRight)
            moveToX = moveXMax;
        else
            moveToX = moveXMin;

        moveTween = transform.parent.DOMoveX(moveToX, moveDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            isMovingRight = !isMovingRight;

            flip();
            Move();
        });
    }

    void flip()
    {
        float newX = -transform.parent.localScale.x;
        transform.parent.localScale = new Vector3(newX, transform.parent.localScale.y, transform.parent.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CollisionManager.Instance.countCollision(collision.gameObject);
            transform.GetComponent<CapsuleCollider2D>().enabled = false;
            transform.parent.DOScale(Vector3.zero, 1).SetEase(Ease.OutQuint).OnComplete(() => Destroy(gameObject));
            saySomething();
        }
    }

    protected void saySomething()
    {
        if(dialogText != null)
            DialogManager.Instance.ShowDialog(dialogDuration, dialogText);
    }
}
