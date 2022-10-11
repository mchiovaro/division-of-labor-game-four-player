// script for the ending cells

using System;
using TouchScript.Gestures;
using UnityEngine;
using Random = UnityEngine.Random;


/// <exclude />
public class DropCellScript : MonoBehaviour
{
  // not in contact with the cell
    public bool contact_on = false;
    Collider2D player_coll;

    // counter for taps to drop the pellet in the cell
    public int dropTapCounter = 0;
    public int pelletCounter = 0;
    private GameObject beeFree, beeRestricted, beeFree2, beeRestricted2, beeFreeMiddle, beeRestrictedMiddle, beeFree2Middle, beeRestricted2Middle;

    private void Start()
    {
      Debug.Log("pelletCounter start = " + pelletCounter);
      beeFree = GameObject.FindGameObjectWithTag("bee_free");
      beeRestricted = GameObject.FindGameObjectWithTag("bee_restricted");
      beeFree2 = GameObject.FindGameObjectWithTag("bee_free2");
      beeRestricted2 = GameObject.FindGameObjectWithTag("bee_restricted2");
      beeFreeMiddle = GameObject.FindGameObjectWithTag("bee_free_middle");
      beeFree2Middle = GameObject.FindGameObjectWithTag("bee_free2_middle");
      beeRestrictedMiddle = GameObject.FindGameObjectWithTag("bee_restricted_middle");
      beeRestricted2Middle = GameObject.FindGameObjectWithTag("bee_restricted2_middle");
    }

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += tappedHandler2;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= tappedHandler2;
    }

    // when player collides with the drop cell
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player_coll = collision.collider;
        contact_on = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        contact_on = true;
        player_coll = other;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        contact_on = false;
        dropTapCounter = 0;
    }

    //this happens when the pellet is tapped
    private void tappedHandler2(object sender, EventArgs eventArgs)
    {

        if (contact_on)
        {
            if (player_coll.transform.childCount != 0) // FIX: is the little cirlce a child here?
            {

                //Debug.Log(player_coll.transform.GetChild(0).tag);

                dropTapCounter++;

                Debug.Log("player colliding: " + player_coll.tag);

                if(dropTapCounter == 2)
                {
                    // set the offset to drop the pellet in the back
                    float offset = 5.4f / 10;
                    // if there is not pellet yet, drop it in the back
                    if (pelletCounter == 0)
                        offset *= -1;

                    GameObject pellet_ = player_coll.transform.GetChild(1).gameObject;
                    Debug.Log("PELLET = " + pellet_);

                    // render the pellet in the cell
                    pellet_.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 0.0f);
                    pellet_.transform.SetParent(null);
                    pellet_.transform.position = transform.position + new Vector3(offset,0,0);
                    pellet_.GetComponent<Collider2D>().enabled = true;
                    pellet_.GetComponent<Rigidbody2D>().isKinematic = true;
                    //player_coll.GetComponent<BeeTap>().grabbed_on = false;

                    pelletCounter++;

                    // turn the player back to their color and log the drop
                    if (player_coll.tag.Equals("bee_free"))
                    {
                        beeFree.GetComponent<BeeTap>().grabbed_on = false;
                        beeFreeMiddle.GetComponent<SpriteRenderer>().color = Color.red;
                        pellet_.GetComponent<PelletScript>().saveToBuffer("1_DROP_END");
                    }
                    if (player_coll.tag.Equals("bee_restricted"))
                    {
                        beeRestricted.GetComponent<BeeTap>().grabbed_on = false;
                        beeRestrictedMiddle.GetComponent<SpriteRenderer>().color = Color.blue;
                        pellet_.GetComponent<PelletScript>().saveToBuffer("2_DROP_END");
                    }
                    if (player_coll.tag.Equals("bee_free2"))
                    {
                        beeFree2.GetComponent<BeeTap>().grabbed_on = false;
                        beeFree2Middle.GetComponent<SpriteRenderer>().color = Color.red;
                        pellet_.GetComponent<PelletScript>().saveToBuffer("3_DROP_END");
                    }
                    if (player_coll.tag.Equals("bee_restricted2"))
                    {
                        beeRestricted2.GetComponent<BeeTap>().grabbed_on = false;
                        beeRestricted2Middle.GetComponent<SpriteRenderer>().color = Color.blue;
                        pellet_.GetComponent<PelletScript>().saveToBuffer("4_DROP_END");
                    }

                    // reset dropping taps to 0
                    dropTapCounter = 0;

                    // add a pellet to that cell?
                    Camera.main.GetComponent<Exit_app_script>().addPellet(pellet_);

                }


            }
        }

    }
}
