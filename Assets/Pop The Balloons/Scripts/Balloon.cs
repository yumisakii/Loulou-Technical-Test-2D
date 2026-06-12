using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour
{
    [SerializeField] private ParticleSystem popEffect;
    [SerializeField] private AudioClip popSound;

    private string balloonName;
    private float speed = 1;
    private bool isPopping = false;
    private bool isCorrectTarget;

    public void Initialize(BalloonData balloonData, LevelData levelData, bool isCorrect)
    {
        balloonName = balloonData.balloonName;
        GetComponent<SpriteRenderer>().sprite = balloonData.balloonSprite;
        speed = Random.Range(levelData.balloonMinSpeed, levelData.balloonMaxSpeed);
        isCorrectTarget = isCorrect;
    }

    public void Update()
    {
        if (!isPopping)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        if (isPopping) return;
        PopBalloon();
    }

    private void PopBalloon()
    {
        isPopping = true;
        GetComponent<Collider2D>().enabled = false;

        GameManager.Instance.RegisterPop(isCorrectTarget);

        AudioSource.PlayClipAtPoint(popSound, transform.position);
        ParticleSystem fxInstance = Instantiate(popEffect, transform.position, Quaternion.identity);
        fxInstance.Play();

        Destroy(fxInstance.gameObject, 1f);

        transform.DOScale(transform.localScale * 1.5f, 0.1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => Destroy(gameObject));
    }
}