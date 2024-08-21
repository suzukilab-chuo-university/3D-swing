using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static int cnt = 0;
    public GameObject[] Meshs;
    public static int[] MeshList;

    public void ChangeMesh()
    {
        switch (cnt)
        {
            case 1:
                // position
                Meshs[0].transform.localPosition = new Vector3(-1.0f, 1.2f, 0.0f);

                // scale
                Meshs[0].transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
                break;

            case 2:
                // position
                Meshs[0].transform.localPosition = new Vector3(-5.0f, 1.2f, 0.0f);
                Meshs[1].transform.localPosition = new Vector3(2.5f, 1.2f, 0.0f);

                // scale
                for (int i = 0; i < 2; i++)
                {
                    Meshs[i].transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
                }
                break;

            case 3:
                // position
                Meshs[0].transform.localPosition = new Vector3(-6.0f, 1.2f, 0.0f);
                Meshs[1].transform.localPosition = new Vector3(-1.25f, 1.2f, 0.0f);
                Meshs[2].transform.localPosition = new Vector3(3.5f, 1.2f, 0.0f);

                // scale
                for (int i = 0; i < 3; i++)
                {
                    Meshs[i].transform.localScale = new Vector3(0.55f, 0.55f, 1.0f);
                }
                break;

            case 4:
                // position
                Meshs[0].transform.localPosition = new Vector3(-5.0f, 3.0f, 0.0f);
                Meshs[1].transform.localPosition = new Vector3(2.5f, 3.0f, 0.0f);
                Meshs[2].transform.localPosition = new Vector3(-5.0f, -0.6f, 0.0f);
                Meshs[3].transform.localPosition = new Vector3(2.5f, -0.6f, 0.0f);

                // scale
                for (int i = 0; i < 4; i++)
                {
                    Meshs[i].transform.localScale = new Vector3(0.55f, 0.55f, 1.0f);
                }
                break;

            case 5:
                // position
                Meshs[0].transform.localPosition = new Vector3(-6.0f, 3.0f, 0.0f);
                Meshs[1].transform.localPosition = new Vector3(-1.25f, 3.0f, 0.0f);
                Meshs[2].transform.localPosition = new Vector3(3.5f, 3.0f, 0.0f);
                Meshs[3].transform.localPosition = new Vector3(-6.0f, -0.6f, 0.0f);
                Meshs[4].transform.localPosition = new Vector3(-1.25f, -0.6f, 0.0f);

                // scale
                for (int i = 0; i < 5; i++)
                {
                    Meshs[i].transform.localScale = new Vector3(0.55f, 0.55f, 1.0f);
                }
                break;

            case 6:
                // position
                Meshs[0].transform.localPosition = new Vector3( -6.0f,  3.0f, 0.0f);
                Meshs[1].transform.localPosition = new Vector3(-1.25f,  3.0f, 0.0f);
                Meshs[2].transform.localPosition = new Vector3(  3.5f,  3.0f, 0.0f);
                Meshs[3].transform.localPosition = new Vector3( -6.0f, -0.6f, 0.0f);
                Meshs[4].transform.localPosition = new Vector3(-1.25f, -0.6f, 0.0f);
                Meshs[5].transform.localPosition = new Vector3(  3.5f, -0.6f, 0.0f);

                // scale
                for (int i = 0; i < 6; i++)
                {
                    Meshs[i].transform.localScale = new Vector3(0.55f, 0.55f, 1.0f);
                }
                break;
        }
    }
}
