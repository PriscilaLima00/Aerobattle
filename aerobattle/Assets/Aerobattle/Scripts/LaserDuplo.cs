using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDuplo : MonoBehaviour
{
    public float velocidadeDoLaser;

    public int danoParaDar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovimentarLaserDuplo();
    }

    private void MovimentarLaserDuplo()
    {
        transform.Translate(Vector3.up * velocidadeDoLaser * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Vex.09"))
        {
            colision.gameObject.GetComponent<Vex_09>().ReceberDanoVex(danoParaDar);
            Destroy(this.gameObject);
        }
    }
}
