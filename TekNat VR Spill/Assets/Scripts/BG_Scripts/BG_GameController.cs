using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BG_GameController : MonoBehaviour
{
    private int BGScore;
    public GameObject placement_block;
    public int width;
	
	private Vector3 cameraPosition;
    private Vector3 canvasPosition;
	public Transform cameraRigTransform;
    public Transform canvasRigTransform;

    private BG_Button buttons;


    private int num_blocks;
    private List<int> altList;
    int alt_2;
    int alt_3;

    private float width_shift;
    private float height_shift;
    public float center_adjustment = (float) 0.1;
    public float space_adjustment = (float) 0.3;

    public Transform helperLineWidth;
    public Transform helperLineHeight;
    public Transform helperLineHeightText;
    public Transform helperLineWidthText;
    public TextMesh helperLineWidthTextMesh;
    public TextMesh helperLineHeightTextMesh;

    private GameObject parentObject;



    // Calculates the shhift in width and height needed to center the shape aswell as the position of the Camera and buttons
    private void Positioning(int height)
    {
        width_shift = (float)(((width - 1) * space_adjustment) + center_adjustment * 2) / 2;
        height_shift = (float)(((height - 1) * space_adjustment) + center_adjustment * 2) / 2;
        cameraPosition = new Vector3(0 , 0, -width_shift - (float)3.5);
        canvasPosition = new Vector3(cameraPosition.x, (float)0.01, cameraPosition.z + 2);
    }

    // Positions and adds one of the boxes in the shape
    private void InstantiateObject(int x, int y, int z)
    {
        Vector3 spawnPosition = new Vector3(x * space_adjustment + center_adjustment - width_shift, y * space_adjustment +
            center_adjustment, z * space_adjustment + center_adjustment - width_shift);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);
        GameObject childObject = Instantiate(placement_block, spawnPosition, spawnRotation);
        childObject.transform.parent = parentObject.transform;
    }

    // positions and scale the helperlines for the shape
    private void HelpModifier(int height)
    {
        // create height and width helperlines
        helperLineWidth.localScale = new Vector3((float)0.05, (float)0.05, width_shift * 2);
        helperLineWidth.position = new Vector3( 0, (float)0.05 ,-width_shift - (float)0.1);

        helperLineHeight.localScale = new Vector3((float)0.05, height_shift * 2, (float)0.05);
        helperLineHeight.position = new Vector3(width_shift + (float)0.1, height_shift, -width_shift - (float)0.1);

        helperLineHeightTextMesh.text = height.ToString();
        helperLineWidthTextMesh.text = width.ToString();

        helperLineHeightText.position = new Vector3(width_shift + (float)0.1, height_shift, -width_shift - (float)0.3);
        helperLineWidthText.position = new Vector3(0, 0, -width_shift - (float)0.35);
    }


    public List<int> Create2DSquare(int height)
    {
        //Debug.Log("Initiializing variables...");
        width = height;
        Positioning(height);

        num_blocks = width * height;
        int alt_2 = width * height/2 + Random.Range(2,5);
        if (alt_2 == num_blocks) alt_2 += Random.Range(1, 5);
        int alt_3 = width * height - Random.Range(2, 5);
        if (alt_3 == alt_2) alt_3 += Random.Range(2, 5);

        //Debug.Log("Loading Square...");

        int x = 0;

        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < width; z++)
            {
                InstantiateObject(z, y, x);
            }

        }
        HelpModifier(height);
        return new List<int> { num_blocks, alt_2, alt_3 };
    }

    public List<int> CreateSquare(int height)
    {
        //Debug.Log("Initiializing variables...");
        width = height;
        Positioning(height);

        num_blocks = width * height * width;
        alt_2 = width * height * Random.Range(2,7);
        if (alt_2 == num_blocks) alt_2 += Random.Range(1, 5);
        alt_3 = width * height * (width + Random.Range(1,5));
        if (alt_3 == alt_2) alt_3 += Random.Range(2, 5);


        //Debug.Log("Loading Square...");

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < width; z++)
                {
                    InstantiateObject(x, y, z);
                }
            }

        }
        HelpModifier(height);
        return new List<int> { num_blocks, alt_2, alt_3 };
    }

    public List<int> CreateTriangle(int height)
    {
        width = height * 2 - 1;
        Positioning(height);

        alt_2 = height * width;
        alt_3 = height * width / 2;

        int width_ = width;
        int x = 0;
        int z_ = 0;

        for (int y = 0; y < height; y++)
        {
            for (int z = z_; z < width_; z++)
            {
                num_blocks++;
                InstantiateObject(z, y, x);
            }
            z_++;
            width_--;
        }
        HelpModifier(height);

        return new List<int> { num_blocks, alt_2, alt_3};
    }

    public List<int> CreatePyramid(int height, int level)
    {
        //Debug.Log("Initiializing variables...");
        width = height * 2 - 1; // Number of cubees at the bottom level of a given height
        Positioning(height);

        num_blocks = 0;
        
        int width_ = width;
        int x_ = 0;
        int z_ = 0;

        //Debug.Log("Loading Pyramid...");
        for (int y = 0; y < height; y++)
        {
            for (int x = x_; x < width_; x++)
            {
                for (int z = z_; z < width_; z++)
                {
                    num_blocks++;
                    InstantiateObject(x, y, z);
                }
            }
            z_++;
            x_++;
            width_--;
        }
        HelpModifier(height);
        if (level < 15)
        {
            alt_2 = height * width * width;
            alt_3 = width * width;
        }
        else
        {
            if (Random.value > 0.5) alt_2 = num_blocks + Random.Range(1, 5);
            else alt_2 = num_blocks - Random.Range(1, 5);
            if (Random.value > 0.5) alt_3 = num_blocks + Random.Range(1, 5);
            else alt_3 = num_blocks - Random.Range(1, 5);
        }

        return new List<int> { num_blocks, alt_2, alt_3 };
    }

    void RelocateCamera () 
	{
		cameraRigTransform.position = cameraPosition;
        canvasRigTransform.position = canvasPosition;
	}

    void Start()
    {
        int level = GlobalVariables.BGLvl;
        if (GlobalVariables.BGLvl == null) GlobalVariables.BGLvl = 1;

        GameObject buttonControllerObject = GameObject.FindWithTag("BG_Canvas");
        GameObject pyramidObject = GameObject.FindGameObjectWithTag("BG_Pyramid");
        if (buttonControllerObject != null && pyramidObject != null)
        {
            parentObject = pyramidObject;
            buttons = buttonControllerObject.GetComponent<BG_Button>();
        }
        if (buttonControllerObject == null)
        {
            Debug.Log("Can't find Canvas");
        }
        if (pyramidObject == null)
        {
            Debug.Log("Can't find pyramid object");
        }

        switch(level) {
            case 1:
                altList = Create2DSquare(Random.Range(4, 10));
                break;
            case 2:
                altList = CreateTriangle(Random.Range(4, 5));
                break;
            case 3:
                altList = CreateSquare(Random.Range(5, 10));
                break;
            case 4:
                altList = CreatePyramid(Random.Range(2, 5),level);
                break;
            case 5:
                altList = CreateSquare(Random.Range(9, 15));
                break;
            default:
                altList = CreatePyramid(Random.Range(5,10), level);
                break;
        }

		RelocateCamera ();
        buttons.AltLoader(altList);
    }
}
