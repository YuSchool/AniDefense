// TowerSelectionUI.cs
// Zweck: Steuert die Anzeige der Turmauswahl im Spiel, einschließlich der verfügbaren Türme und deren Kosten.
// Sinn: Die Turmauswahl ist ein wichtiger Bestandteil der Benutzeroberfläche, da sie dem Spieler ermöglicht, die verfügbaren Türme zu sehen und auszuwählen, um sie im Spiel zu platzieren. Der TowerSelectionUI ist verantwortlich für die Aktualisierung und Verwaltung dieser Informationen, um sicherzustellen, dass der Spieler


using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSelectionUI : MonoBehaviour
{
    #region REFERENZEN

    [Header("Panels")]
    [SerializeField] private GameObject panel_Bauliste;
    [SerializeField] private GameObject panel_Upgrade;

    [Header("Bauliste")]
    [SerializeField] private GameObject towerKartePrefab;
    [SerializeField] private Transform kartenContainer;

    [Header("Tower Datas")]
    [SerializeField] private TowerData[] alleTowerDatas;

    [Header("Tower Prefabs")]
    [SerializeField] private GameObject[] alleTowerPrefabs;

    #endregion

    #region UNITY LIEFECYCLE

    private void Start()
    {
        LadeBauliste();
        ZeigeBauliste();
    }

    #endregion

    #region BAULISTE

    private void LadeBauliste()
    {
        foreach (Transform kind in kartenContainer)
            Destroy(kind.gameObject);

        for (int i = 0; i < alleTowerDatas.Length; i++)
        {
            TowerData data = alleTowerDatas[i];
            GameObject prefab = i < alleTowerPrefabs.Length ? alleTowerPrefabs[i] : null;

            GameObject karteObj = Instantiate(towerKartePrefab, kartenContainer);

            // Felder befüllen
            karteObj.transform.Find("Tower_Name").GetComponent<TextMeshProUGUI>().text
                = data.charakterName;

            karteObj.transform.Find("Tower_Type").GetComponent<TextMeshProUGUI>().text
                = data.towerTyp.ToString();

            karteObj.transform.Find("Tower_Kosten").GetComponent<TextMeshProUGUI>().text
                = $"{data.goldKosten} Gold - {data.seelenKosten} Seelen";

            karteObj.transform.Find("Tower_Seltenheitsklasse").GetComponent<TextMeshProUGUI>().text
                = data.seltenheitsKlasse.ToString();

            karteObj.transform.Find("Tower_Badge").GetComponent<TextMeshProUGUI>().text
                = data.seltenheit.ToString();

            // Sprite setzen falls vorhanden
            if (data.charakterSprite != null)
                karteObj.transform.Find("Tower_Image").GetComponent<Image>().sprite
                    = data.charakterSprite;

            // Klick-Listener
            int index = i;
            karteObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                TowerPlacer.Instance.WaehleTower(data, prefab);
                Debug.Log($"[TowerSelectionUI] Tower ausgewählt: {data.charakterName}");
            });
        }
    }

    #endregion

    #region Panel Steuerung

    public void ZeigeBauliste()
    {
        panel_Bauliste.SetActive(true);
        panel_Upgrade.SetActive(false);
    }

    public void ZeigeUpgrade()
    {
        panel_Bauliste.SetActive(false);
        panel_Upgrade.SetActive(true);
    }

    #endregion
}