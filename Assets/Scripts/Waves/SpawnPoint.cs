using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    #region Daten

    [Header("Route")]
    [SerializeField] private Transform[] wegpunkte;

    public Transform[] Wegpunkte => wegpunkte;

    #endregion
}