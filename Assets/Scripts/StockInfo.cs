using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StockInfo : MonoBehaviour
{

    /* StockInfo.cs holds data for each food. 
     * All food models or gameobjects that wants to hold food data MUST have StockInfo.cs!
     
         This Script automatically changes its own model to food model based on containing data*/


    [SerializeField]
    bool isActive;

    public Food food;
    public int index;

    // Use this for initialization
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponent<StockInfo>().food.foodName != null && gameObject.GetComponent<StockInfo>().food != null)
        {
            if (SceneManager.GetActiveScene().name != "AR_Main")
            {
                //If virtual scene, change model's mesh and material based on fooddata
                gameObject.GetComponent<MeshFilter>().mesh
                    = gameObject.GetComponent<StockInfo>().food.foodPrefab.GetComponent<MeshFilter>().sharedMesh;
                gameObject.GetComponent<MeshRenderer>().materials
                    = gameObject.GetComponent<StockInfo>().food.foodPrefab.GetComponent<MeshRenderer>().sharedMaterials;
            }
            else
            {
                //If AR scene, change only image material based on food data (AR is 3D image)
                gameObject.GetComponent<MeshRenderer>().material
                    = gameObject.GetComponent<StockInfo>().food.foodARImage;
            }
        }

        if (!isActive)
        {
            //if selection model not in use and not in AR mode, Return to original position
            if(SceneManager.GetActiveScene().name != "AR_Main")
                ReduceModel();
            
        }
    }

    //Change forward position
    public void EnlargeModel()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    //Change original position
    public void ReduceModel()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    //To call from other scripts
    public void SetActiveSelected(bool active)
    {
        isActive = active;
    }
}
