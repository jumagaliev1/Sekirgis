using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Char : MonoBehaviour
{
    private Rigidbody2D rb;
    public float fallMultiplier = 4.5f;
    private bool Grounded = true;
    private Animator anim;
    public GameObject dustParticle;
    private bool firstJump = true;
    private float prevYpos = -1000;
    public bool isDead = false;
    public GameObject GameOverScreen;
    Color[] standColor = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan};
    int colorIndex = 0;
    int Acheivemnt = 30;
    private char lastJump = 'N';
    
    public Text scoreText;
    public int total_score;
    public int prev_score;
    public Text record;
    public Button smallbtn;
    public Button bigbtn;
    private AudioSource audio;
    private bool hideBtn;
    //private int isAd;
    
    void Start()
    {
        prev_score = PlayerPrefs.GetInt("prev_score");
        if (PlayerPrefs.GetInt("isAd") == 1)
        {
            scoreText.text = prev_score.ToString();
        }
        PlayerPrefs.SetInt("isAd", 0);
        total_score = PlayerPrefs.GetInt("total_score");
        record.text = total_score.ToString();
        audio = GetComponent<AudioSource>();
        Time.timeScale = 2f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        
    }
    


    // Update is called once per frame
    void Update()
    {
        if(transform.position.y + 5f < prevYpos)
        {
            if (!isDead)
                Death();
        }
        /*if (Input.GetKeyDown(KeyCode.LeftArrow))
            Jump(true);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Jump(false);
        if (hideBtn)
        {
            hideTObtn();
        }*/
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier * Time.deltaTime);
    
    }
    void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
    }
    public void hideTObtn()
    {
        var colors = bigbtn.colors;

        colors.normalColor = new Color(255, 255, 255, 10);

        bigbtn.colors = colors;
        smallbtn.colors = colors;
    }

        public void Jump(bool smallJump)
    {
        

        firstJump = false;
        hideBtn = true;
        if (!Grounded)
            return;
        //play animation
        anim.SetTrigger("jump");
        Grounded = false;
        if(smallJump){
            lastJump = 'S';
            rb.AddForce(new Vector2(9.8f * 12f, 9.8f * 20f));
        }
        else
        {
            lastJump = 'B';
            StartCoroutine(longJump());
             //do long jump
        }
        
    }
    IEnumerator longJump()
    {
        rb.AddForce(new Vector2(0, 9.8f * 29f));
        yield return new WaitForSeconds(0.15f);
        rb.AddForce(new Vector2(9.8f * 12f, 0));
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        rb.velocity = Vector3.zero;
        if (lastJump == 'S' && col.gameObject.tag == "smallTile")
        {

        }
        else if (lastJump == 'B' && col.gameObject.tag == "bigTile")
        {

        }
        else if (lastJump == 'N')
        {

        }
        else
        {
            print("wrong Jump    - Game Over");
            smallbtn.gameObject.SetActive(false);
            bigbtn.gameObject.SetActive(false);
            GameOverScreen.SetActive(true);
        }

        if (col.gameObject.tag.Contains("Tile"))
        {
            // play sound
            audio.Play();
            //update score
            scoreText.text = (int.Parse(scoreText.text) +5).ToString();
            // change color
            GameObject cube = col.gameObject;
            Renderer cr = cube.GetComponent<Renderer>();
            cr.material.SetColor("_Color", standColor[colorIndex]);
            
            prevYpos = transform.position.y;
            
            GameObject temp = Instantiate(dustParticle, new Vector3(transform.position.x, transform.position.y-0.5f, transform.position.z), dustParticle.transform.rotation);
            Destroy(temp, 1.5f);
            
            Grounded = true;
            
            transform.position = new Vector3(col.gameObject.transform.position.x-0.2f, transform.position.y, transform.position.z);
            if (firstJump)
                return;
            StartCoroutine(FallTile(col.gameObject));
        }
        AcheivementAcheived();
        
        
    }
    void AcheivementAcheived()
    {
        if (int.Parse(scoreText.text) == Acheivemnt)
        {
            if (colorIndex >= standColor.Length - 1)
            {
                colorIndex = 0;
            }
            else
            {
                colorIndex++;
            }
            Acheivemnt = Acheivemnt * 2;

        }
    }
    IEnumerator FallTile(GameObject col)
    {
        yield return new WaitForSeconds(2f);
        if (col.gameObject != null)
            col.AddComponent<Rigidbody2D>();
    }
    public void replayWithContr()
    {
        PlayerPrefs.SetInt("isAd", 1);
        PlayerPrefs.SetInt("prev_score", int.Parse(scoreText.text));
        GameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        


    }
    void Death()
    {
        
        if (int.Parse(scoreText.text) > total_score)
        {
            total_score = int.Parse(scoreText.text);
            PlayerPrefs.SetInt("total_score", total_score);
            

        }
        
        GameOverScreen.SetActive(true);
        
        print("Player died ......");
        smallbtn.gameObject.SetActive(false);
        bigbtn.gameObject.SetActive(false);
        isDead = true;
    }
}
