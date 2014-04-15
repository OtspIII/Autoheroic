using UnityEngine;
using System.Collections;

public class LibraryFeeder : MonoBehaviour
{

    public GameObject cube;
    public Material cubeMat;
    public GameObject character;
    public GameObject bullet;
    public GameObject rocket;
    public GameObject explosion;
    public GameObject healer;
    public GameObject desert;
    public GameObject grass;
    public AudioClip scream;
    public Material cubMat;

    public AudioClip explosionSound;
    public AudioClip explosion2Sound;
    public AudioClip laserShotSound;
    public AudioClip footstepSound;
    public AudioClip snipeSound;
    public AudioClip hurtSound;

    // Use this for initialization
    void Awake()
    {
        Cub.View.Library.GetPrefabs(cube, cubeMat, character, bullet, rocket, explosion, healer, desert, grass, scream, cubMat,
            explosionSound, laserShotSound, footstepSound, snipeSound, explosion2Sound, hurtSound);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
