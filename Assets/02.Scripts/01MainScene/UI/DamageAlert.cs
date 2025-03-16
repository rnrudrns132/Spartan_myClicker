using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageAlert : MonoBehaviour
{
    public TextMeshPro myText;
    public Rigidbody2D myRigid;
    public Material normalMaterial;
    public Material criticalMaterial;

    public void ShowNum(int damage, bool isCritical)
    {
        if (isCritical)
        {
            myText.fontMaterial = criticalMaterial;
            myText.fontSize = 5f;
        }
        else
        {
            myText.fontMaterial = normalMaterial;
            myText.fontSize = 3.5f;
        }

        myText.text = damage.ToString("N0");

        float xRand = Random.Range(-1f,1f);
        transform.position = new Vector2(xRand, Random.Range(-1f, 1f));

        gameObject.SetActive(true);

        myRigid.velocity = Vector2.zero;
        myRigid.AddForce(new Vector2(xRand, 5), ForceMode2D.Impulse);

        nowShowTime = 0;
    }

    float nowShowTime;
    private void Update()
    {
        nowShowTime += Time.deltaTime;
        if (nowShowTime > 2)
        {
            gameObject.SetActive(false);
        }
    }
}
