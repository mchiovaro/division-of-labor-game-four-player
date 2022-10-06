

using System;
using TouchScript.Gestures;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;


/// <exclude />
public class workCellScript : MonoBehaviour
{

    GameObject pellet_;
    private GameObject beeFree, beeRestricted, beeFree2, beeRestricted2;
    private bool free_contact_cell = false;
    private bool restrict_contact_cell = false;
    private bool free_contact_cell2 = false;
    private bool restrict_contact_cell2 = false;
    public bool advanced_pellet;
    public bool contact_on = false;
    public int dropTapCounter = 0;

    public Dictionary<string, bool> my_colliders = new Dictionary<string, bool>() {
        { "bee_free", false }, { "bee_restricted", false }, { "bee_free2", false }, { "bee_restricted2", false }};

    private void Start()
    {
        beeFree = GameObject.FindGameObjectWithTag("bee_free");
        beeRestricted = GameObject.FindGameObjectWithTag("bee_restricted");
        beeFree2 = GameObject.FindGameObjectWithTag("bee_free2");
        beeRestricted2 = GameObject.FindGameObjectWithTag("bee_restricted2");
    }

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += tappedHandler2;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= tappedHandler2;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }


    private void OnTriggerEnter2D(Collider2D collider_)
    {

        // if the collided object is bee free, set contact true and give a debug message
        if (collider_.tag.Equals("bee_free")){
          free_contact_cell = true;
          //Debug.Log(collider_.gameObject.tag + " contacted work cell");
        }

        // if the collided object is bee free, set contact true and give a debug message
        if (collider_.tag.Equals("bee_restricted")){
          restrict_contact_cell = true;
          //Debug.Log(collider_.gameObject.tag + " contacted work cell");
        }

        // if the collided object is bee free, set contact true and give a debug message
        if (collider_.tag.Equals("bee_free2")){
          free_contact_cell2 = true;
          //Debug.Log(collider_.gameObject.tag + " contacted work cell");
        }

        // if the collided object is bee free, set contact true and give a debug message
        if (collider_.tag.Equals("bee_restricted2")){
          restrict_contact_cell2 = true;
          //Debug.Log(collider_.gameObject.tag + " contacted work cell");
        }

        // if the player has a child object (pellet)
        if (collider_.transform.childCount != 0)
        {
            // for each child in their possession
            foreach (Transform tt in collider_.transform)
                    pellet_ = tt.gameObject;

        }

    }

    private void OnTriggerExit2D(Collider2D collider_)
    {
        // if there was a pellet and we moved away, reset pellet to null
        // so it can be identified on next trigger
        if (collider_.transform.childCount != 0)
        {
            pellet_ = null;
        }

        // if the collider object that moved away was bee free, set contact false and give debug message
        if (collider_.tag.Equals("bee_free"))
        {
            free_contact_cell = false;
            //Debug.Log(collider_.gameObject.tag + " moved away from work cell");
        }

        // if the collider object that moved away was bee restricted, set contact false and give debug message
        if (collider_.tag.Equals("bee_restricted"))
        {
          restrict_contact_cell = false;
          //Debug.Log(collider_.gameObject.tag + " moved away from work cell");
        }

        // if the collider object that moved away was bee free, set contact false and give debug message
        if (collider_.tag.Equals("bee_free2"))
        {
            free_contact_cell2 = false;
            //Debug.Log(collider_.gameObject.tag + " moved away from work cell");
        }

        // if the collider object that moved away was bee restricted, set contact false and give debug message
        if (collider_.tag.Equals("bee_restricted2"))
        {
          restrict_contact_cell2 = false;
          //Debug.Log(collider_.gameObject.tag + " moved away from work cell");
        }

        dropTapCounter = 0;

    }

    //this happens when the cell is tapped
    private void tappedHandler2(object sender, EventArgs eventArgs)
    {
        //Debug.Log("TAPPED");
        dropTapCounter++;

        //if restricted player has a pellet and is in contact with the cell
        if (pellet_ != null && restrict_contact_cell == true && dropTapCounter == 2)
        {

            pellet_.GetComponent<PelletScript>().saveToBuffer("2_DROP_MID");

            // remove the player as the parent
            pellet_.transform.SetParent(null);

            // put pellet in the cell
            pellet_.transform.position = transform.position;
            pellet_.GetComponent<Collider2D>().enabled = true;

            // reset the bee to not be grabbed on in tapping script
            beeRestricted.GetComponent<BeeTap>().grabbed_on = false;
            restrict_contact_cell = false;
            // identify the pellet as semi-processed so that we can't pick it up from the right
            pellet_.GetComponent<PelletScript>().advanced_pellet = true;

            // reset the bee color once they've dropped the pellet
            beeRestricted.GetComponent<SpriteRenderer>().color = Color.blue;

            // reset dropping taps to 0
            dropTapCounter = 0;
        }

        //if free player has a pellet and is in contact with the cell
        if (pellet_ != null && free_contact_cell == true && dropTapCounter == 2)
        {

            pellet_.GetComponent<PelletScript>().saveToBuffer("1_DROP_MID");

            // remove the player as the parent
            pellet_.transform.SetParent(null);

            // put pellet in the cell
            pellet_.transform.position = transform.position;
            pellet_.GetComponent<Collider2D>().enabled = true;

            // reset the bee to not be grabbed on in tapping script
            beeFree.GetComponent<BeeTap>().grabbed_on = false;
            free_contact_cell = false;
            // identify the pellet as semi-processed so that we can't pick it up from the right
            pellet_.GetComponent<PelletScript>().advanced_pellet = true;

            // reset the bee color once they've dropped the pellet
            beeFree.GetComponent<SpriteRenderer>().color = Color.red;

            // reset dropping taps to 0
            dropTapCounter = 0;
        }

        //if restricted player has a pellet and is in contact with the cell
        if (pellet_ != null && restrict_contact_cell2 == true && dropTapCounter == 2)
        {

            pellet_.GetComponent<PelletScript>().saveToBuffer("2.2_DROP_MID");

            // remove the player as the parent
            pellet_.transform.SetParent(null);

            // put pellet in the cell
            pellet_.transform.position = transform.position;
            pellet_.GetComponent<Collider2D>().enabled = true;

            // reset the bee to not be grabbed on in tapping script
            beeRestricted2.GetComponent<BeeTap>().grabbed_on = false;
            restrict_contact_cell2 = false;
            // identify the pellet as semi-processed so that we can't pick it up from the right
            pellet_.GetComponent<PelletScript>().advanced_pellet = true;

            // reset the bee color once they've dropped the pellet
            beeRestricted2.GetComponent<SpriteRenderer>().color = Color.blue;

            // reset dropping taps to 0
            dropTapCounter = 0;
        }

        //if free player has a pellet and is in contact with the cell
        if (pellet_ != null && free_contact_cell2 == true && dropTapCounter == 2)
        {

            pellet_.GetComponent<PelletScript>().saveToBuffer("1.2_DROP_MID");

            // remove the player as the parent
            pellet_.transform.SetParent(null);

            // put pellet in the cell
            pellet_.transform.position = transform.position;
            pellet_.GetComponent<Collider2D>().enabled = true;

            // reset the bee to not be grabbed on in tapping script
            beeFree2.GetComponent<BeeTap>().grabbed_on = false;
            free_contact_cell2 = false;
            // identify the pellet as semi-processed so that we can't pick it up from the right
            pellet_.GetComponent<PelletScript>().advanced_pellet = true;

            // reset the bee color once they've dropped the pellet
            beeFree2.GetComponent<SpriteRenderer>().color = Color.red;

            // reset dropping taps to 0
            dropTapCounter = 0;
        }


    }

    // called from Exit_app_script
    public void resetParameters()
    {
        //contact_on = false;
        free_contact_cell = false;
        restrict_contact_cell = false;
        free_contact_cell2 = false;
        restrict_contact_cell2 = false;
        Debug.Log("workCellScript contact set to FALSE");
        my_colliders["bee_free"] = false;
        my_colliders["bee_restricted"] = false;
        my_colliders["bee_free2"] = false;
        my_colliders["bee_restricted2"] = false;

    }
}
