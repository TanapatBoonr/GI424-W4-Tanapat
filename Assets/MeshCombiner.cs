using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        CombineMeshes();
    }

    void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i].transform == transform) continue;

            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false); // ซ่อนของเดิม
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = combinedMesh;

        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        mr.sharedMaterial = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;
    }
}