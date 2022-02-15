using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoints : MonoBehaviour
{
   [SerializeField]
   List<Transform> _spawnPoints = new List<Transform>();

   public Transform GetSpawnPoint()
   {
      int spawn = Random.Range(0, _spawnPoints.Count);
      return _spawnPoints[spawn];
   }

   public Vector3 GetSpawnPointPosition()
   {
      return GetSpawnPoint().position;
   }

   public Quaternion GetSpawnPointRotation()
   {
      return GetSpawnPoint().rotation;
   }
}
