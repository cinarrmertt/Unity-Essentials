using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Zaman Ayarları")]
    [Range(0, 24)] public float gununSaati = 12f; // 0-24 arası saat
    public float gunUzunluguDakika = 2f; // Gerçek dünyada kaç dakika sürsün?

    [Header("Güneş ve Ay")]
    public Light gunesIsigi;
    public Light ayIsigi; // İsteğe bağlı ikincil bir ışık

    [Header("Atmosfer Renkleri")]
    public Gradient gunesRengi; // Turuncu -> Beyaz -> Kırmızı geçişi
    public Gradient gokyuzuRengi; // Mavi -> Lacivert geçişi

    void Update()
    {
        // Zamanı ilerlet (Gerçek zamanlı döngü)
        gununSaati += (Time.deltaTime / (gunUzunluguDakika * 60f)) * 24f;
        if (gununSaati >= 24) gununSaati = 0;

        ZamaniGuncelle();
    }

    void ZamaniGuncelle()
    {
        // Güneş rotasyonu: (Saat / 24) * 360 derece. -90 derece ofset gün doğumu içindir.
        float gunesAcisi = (gununSaati / 24f) * 360f;
        gunesIsigi.transform.localRotation = Quaternion.Euler(gunesAcisi - 90f, 170f, 0f);
        
        // Ay rotasyonu (Güneşin tam zıttı)
        if(ayIsigi != null)
            ayIsigi.transform.localRotation = Quaternion.Euler(gunesAcisi + 90f, 170f, 0f);

        // Renk ve Yoğunluk Ayarları
        float normalizeZaman = gununSaati / 24f;
        float isikSiddeti = Mathf.Clamp01(Vector3.Dot(gunesIsigi.transform.forward, Vector3.down));
        
        gunesIsigi.intensity = isikSiddeti * 1.2f; // Gün ortasında en parlak
        gunesIsigi.color = gunesRengi.Evaluate(normalizeZaman);

        // Ortam ışığını (Ambient) ve Gökyüzünü güncelle
        RenderSettings.ambientLight = gokyuzuRengi.Evaluate(normalizeZaman);
    }
}