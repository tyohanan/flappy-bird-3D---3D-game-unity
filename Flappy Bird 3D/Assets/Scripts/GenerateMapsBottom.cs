using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapsBottom : MonoBehaviour
{
    [SerializeField]
    private GameObject bird;
    [SerializeField]
    private int DistanceValue = 10;
    [SerializeField]
    private int safeDistanceValue = 8;

    public GameObject[] PlatformTop = new GameObject[3];

    private Transform birdPosition;
    private int minDistance;
    private int maxDistance;
    private int height = -1;
    private int fixRotation = -90;
    private int[] rotationDegree = {0};
    public List<GameObject> platformList = new List<GameObject>();
    
    private void Awake() {

    }
    void Start()
    {
        //generate awal saat player masuk
        birdPosition = bird.GetComponent<Transform>();   
        minDistance = (int)birdPosition.position.x -DistanceValue;
        maxDistance = (int)birdPosition.position.x +DistanceValue;
        // for (int x = minDistance; x <= maxDistance; x+=2){
        //     int getPlatform = Random.Range(0, PlatformTop.Length);
        //     int randomRotation = Random.Range(0,rotationDegree.Length);
        //     var allPlatformTop = Instantiate(PlatformTop[getPlatform], new Vector3(x,height,0), Quaternion.Euler(rotationDegree[randomRotation],fixRotation,0));
        //     platformList.Add(allPlatformTop);
        // }
    }

    void Update()
{
        //randomize platform and rotation
        int getPlatform = Random.Range(0, PlatformTop.Length);
        int randomRotation = Random.Range(0,rotationDegree.Length);
        //generate platform atas baru saat bird masuk ke safe distance
        int maxSafeDistance = (int) birdPosition.position.x + safeDistanceValue;
        int minSafeDistance = (int) birdPosition.position.x - safeDistanceValue;
        //check distance front
        if (maxSafeDistance >= maxDistance){
            var safePlatform = Instantiate(PlatformTop[getPlatform], new Vector3(maxDistance+2,height,0), Quaternion.Euler(rotationDegree[randomRotation],fixRotation,0));
            maxDistance = (int)birdPosition.position.x + DistanceValue;
            minDistance = (int)birdPosition.position.x - DistanceValue;
            platformList.Add(safePlatform);


            // Debug.Log(maxDistance +" "+safeDistance+" "+(int)birdPosition.position.x);
        } 
        //check distance back
        else if (minSafeDistance <= minDistance){
            var safePlatform = Instantiate(PlatformTop[getPlatform], new Vector3(minDistance-2,height,0), Quaternion.Euler(rotationDegree[randomRotation],fixRotation,0));
            maxDistance = (int)birdPosition.position.x + DistanceValue;
            minDistance = (int)birdPosition.position.x - DistanceValue;
            platformList.Add(safePlatform);
        }
        //checking if the platform out of distance then we can destroy it
        for (int x = 0; x < platformList.Count; x++){
            if (platformList[x].transform.position.x < (minDistance-4) || platformList[x].transform.position.x > (maxDistance+4)){
                Destroy(platformList[x]);
                platformList.RemoveAt(x);
            }
        }
        // Debug.Log(platformList.Count);
    }
}
