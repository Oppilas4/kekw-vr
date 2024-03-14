using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace HairSalon
{
    public class MeshSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        private void Start()
        {
            string filePath = Application.dataPath + "/Scripts/HairSalon/HS_ApinaHaircut.txt";
            try
            {
                using StreamReader reader = new StreamReader(filePath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.StartsWith("v ")) continue;
                    string[] parts = line.Split(' ');
                    if (parts.Length < 4) continue;
                    float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                    float z = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity, transform);
                }
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}