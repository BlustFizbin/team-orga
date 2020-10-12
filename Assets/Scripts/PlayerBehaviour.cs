using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speedX = 2;
    [SerializeField] private float speedY = 1;

    [SerializeField] private GameObject flowerObject;
    [SerializeField] private Transform parent;
    private Rigidbody2D rigid;
    private Transform spawnPoint;

    public Sprite[] smallSprites;
    public Sprite[] bigSprites;

    public float timeBonusPerSprite = 0.1f;

    public Slider slider, slider2;
    
    [TextArea(3,8)]
    public string textInput = "test";
    public ParticleSystem plopParticle;
    
    private string[] lines;
    public Transform camTransform;

    public float timeHealthDecrease = 1f;

    public float hitPoints = 50f;
    public float playerFlowers = 5f;
    
    public Text textElement;

    private bool cooldown;
    private bool canDeliverFlowers = false;
    public void SetFlowerActive(bool active)
    {
        canDeliverFlowers = active;
    }

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = hitPoints;
        slider2.maxValue = 16f;
        lines = textInput.Split('\n');
        cooldown = true;
        rigid = GetComponent<Rigidbody2D>();
        spawnPoint = transform.GetChild(1);
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (inputX != 0)
        {
            if (inputX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            } else transform.localScale = new Vector3(1, 1, 1);

            rigid.AddForce(new Vector2(inputX * speedX * Time.deltaTime, 0), ForceMode2D.Impulse);
        }

        float inputY = Input.GetAxis("Vertical");

        if (inputY != 0)
        {
            rigid.AddForce(new Vector2(0, inputY * speedY * Time.deltaTime), ForceMode2D.Impulse);
        }

    }

    private void Update()
    {
        if (cooldown && canDeliverFlowers && playerFlowers > 0f) { 
            if (rigid.velocity.x > 1 || rigid.velocity.x < -1 || rigid.velocity.y > 1 || rigid.velocity.y < -1)
            {
                cooldown = false;
                int random = Random.Range(0, smallSprites.Length);
                GameObject currentSpawnObject = Instantiate(flowerObject, parent);
                currentSpawnObject.GetComponent<SpriteRenderer>().sprite = smallSprites[random];
                currentSpawnObject.transform.position = spawnPoint.position;
                StartCoroutine(Cooldown());
                SpawnText();
                playerFlowers -= 1f;
                hitPoints += timeBonusPerSprite;
                StartCoroutine(FadeFlowerIn(currentSpawnObject, bigSprites[random]));
            }
        }

        if (!canDeliverFlowers)
        {
            hitPoints -= Time.deltaTime * timeHealthDecrease;
            playerFlowers += Time.deltaTime;
        }

        slider.value = hitPoints;
        slider2.value = Mathf.Min(slider2.maxValue, playerFlowers);
        
        if (hitPoints < 0f)
        {
            // exit
            enabled = false;
            SceneManager.LoadScene(0);
        }
    }

    void SpawnText()
    {
        textElement.text = lines[Random.Range(0, lines.Length)];
    }

    IEnumerator FadeFlowerIn(GameObject flower, Sprite grownImage)
    {
        SpriteRenderer rnd = flower.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(5f);
        
        plopParticle.transform.position = rnd.transform.position;
        plopParticle.Emit(8);
        rnd.sprite = grownImage;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.4f);
        cooldown = true;
    }

    IEnumerator CameraShake()
    {
        for (int i = 0; i < 16; i++)
        {
            camTransform.position += Random.onUnitSphere * Time.deltaTime;
        }
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(CameraShake());
        hitPoints -= 5f;
    }
}
