using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class QuestionBoxScript : MonoBehaviour
{
    public float bounceHeight = 0.1f;
    public float bounceSpeed = 3f;

    public float coinHeight = 1f;
    public float coinSpeed = 2f;

    private bool canBounce = true;

    public GameObject spinningCoin;

    public Animator boxAnimator;

    // for audio
    public AudioSource boxAudio;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void QuestionBoxBounce()
    {
        if (canBounce)
        {
/*            Debug.Log("Bouncing!");*/
            canBounce = false;
            spawnCoin();
            StartCoroutine(Bounce());
            boxAnimator.SetTrigger("KenaHit");
        }
    }

    void spawnCoin()
    {
/*        Debug.Log("Spawning Coin");*/
        GameObject coin = Instantiate(spinningCoin, gameObject.transform);
        coin.transform.position = gameObject.transform.position;
        StartCoroutine(coinBounce(coin));
    }

    IEnumerator Bounce()
    {
        Vector2 originalPosition = transform.localPosition;
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y >= originalPosition.y + bounceHeight)
            {
/*                Debug.Log("MaxHeight");*/
                break;
            }
            yield return null;
        }
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y <= originalPosition.y + bounceHeight)
            {
/*                Debug.Log("OrgHeight");*/
                transform.localPosition = originalPosition;
                break;
            }
            yield return null;
        }
    }

    IEnumerator coinBounce(GameObject coin)
    {
        Vector2 originalPosition = coin.transform.localPosition;
        coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + 1);
        while (true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + coinSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y >= originalPosition.y + coinHeight+1)
            {
/*                Debug.Log("MaxCoinHeight");*/
                break;
            }
            yield return null;
        }
        while (true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y - coinSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y <= originalPosition.y)
            {
/*                Debug.Log("CoinFallDistance");*/
                boxAudio.PlayOneShot(boxAudio.clip);
                Destroy(coin.gameObject);
                break;
            }
            yield return null;
        }
    }
}
