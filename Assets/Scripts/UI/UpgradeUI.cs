using TMPro;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panel_Upgrade;
    [SerializeField] private GameObject panel_Bauliste;

    [Header("Aktueller Tower")]
    [SerializeField] private TextMeshProUGUI text_NameAlt;
    [SerializeField] private TextMeshProUGUI text_AngriffsgeschwindigkeitAlt;
    [SerializeField] private TextMeshProUGUI text_SchadenAlt;
    [SerializeField] private TextMeshProUGUI text_ReichweiteAlt;
    [SerializeField] private TextMeshProUGUI text_ZielprioAlt;
    [SerializeField] private TextMeshProUGUI text_StufeAlt;

    [Header("Neuer Tower")]
    [SerializeField] private TextMeshProUGUI text_NameNeu;
    [SerializeField] private TextMeshProUGUI text_AngriffsgeschwindigkeitNeu;
    [SerializeField] private TextMeshProUGUI text_SchadenNeu;
    [SerializeField] private TextMeshProUGUI text_ReichweiteNeu;
    [SerializeField] private TextMeshProUGUI text_ZielprioNeu;
    [SerializeField] private TextMeshProUGUI text_StufeNeu;

    [Header("Kosten")]
    [SerializeField] private TextMeshProUGUI text_GoldKosten;
    [SerializeField] private TextMeshProUGUI text_SeelenKosten;
    [SerializeField] private TextMeshProUGUI text_AuraKosten;

    private TowerBase aktuellerTower;

    public void ZeigeUpgrade(TowerBase tower)
    {
        aktuellerTower = tower;

        TowerData alt = tower.Data;
        TowerData neu = alt.upgradeTo;

        // Panel umschalten
        panel_Bauliste.SetActive(false);
        panel_Upgrade.SetActive(true);

        // Aktueller Tower
        text_NameAlt.text = alt.charakterName;
        text_AngriffsgeschwindigkeitAlt.text = $"{alt.angriffsgeschwindigkeit}/s";
        text_SchadenAlt.text = $"{alt.schaden}";
        text_ReichweiteAlt.text = $"{alt.reichweite}";
        text_ZielprioAlt.text = alt.zielPrioritaet.ToString();
        text_StufeAlt.text = $"Stufe {alt.stufe}";

        if (neu != null)
        {
            text_NameNeu.text = neu.charakterName;
            text_AngriffsgeschwindigkeitNeu.text = $"{neu.angriffsgeschwindigkeit}/s";
            text_SchadenNeu.text = $"{neu.schaden}";
            text_ReichweiteNeu.text = $"{neu.reichweite}";
            text_ZielprioNeu.text = neu.zielPrioritaet.ToString();
            text_StufeNeu.text = $"Stufe {neu.stufe}";
            text_GoldKosten.text = $"{neu.goldKosten} Gold";
            text_SeelenKosten.text = $"{neu.seelenKosten} Seelen";
            text_AuraKosten.text = $"{neu.auraKosten} Aura";
        }
        else
        {
            text_NameNeu.text = "Max Stufe";
            text_AngriffsgeschwindigkeitNeu.text = "-";
            text_SchadenNeu.text = "-";
            text_ReichweiteNeu.text = "-";
            text_ZielprioNeu.text = "-";
            text_StufeNeu.text = "-";
            text_GoldKosten.text = "-";
            text_SeelenKosten.text = "-";
            text_AuraKosten.text = "-";
        }
    }

    public void Upgrade()
    {
        if (aktuellerTower == null) return;
        TowerUpgrader upgrader = aktuellerTower.GetComponent<TowerUpgrader>();
        if (upgrader == null) return;

        if (upgrader.KannUpgraden())
        {
            upgrader.Upgrade();
            ZeigeUpgrade(aktuellerTower);
        }
        else
        {
            Debug.Log("[UpgradeUI] Upgrade nicht möglich");
        }
    }

    public void SchliesseUpgrade()
    {
        aktuellerTower = null;
        panel_Upgrade.SetActive(false);
        panel_Bauliste.SetActive(true);
    }

}
