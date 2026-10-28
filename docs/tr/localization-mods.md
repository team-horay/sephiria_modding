# Yerelleştirme Modları

[İlk Modunuz](getting-started.md) mod yapmanın temellerini ele aldı; bu sayfa ise yerelleştirme modları için geçerli ortak kuralları kapsar.
[`examples/translation-sample`](../../examples/translation-sample) içinde, doğrudan `WorkshopLocal/` klasörüne kopyalayıp deneyebileceğiniz eksiksiz bir örnek bulunur.



## Nasıl Çalışır

- Oyunda her dil için bir **anahtar → dize** tablosu vardır. Bir mod yüklendiğinde moddaki değerler özgün tablonun üzerine yazılır.
- Dosyanızda yalnızca değiştirmek istediğiniz anahtarların bulunması yeterlidir.
- Dosya adı dili belirler. `ko-KR.json` Korece tablonun, `en-US.json` İngilizce tablonun üzerine yazar.
- Birden fazla yerelleştirme modu aynı anahtarı ayarlarsa, sonra yüklenen mod kazanır.
- Yalnızca metni değiştiren bir mod diğer katılımcıları etkilemez; bu yüzden `metadata.json` içindeki `multiplayer.installation` değerini `local_only` olarak ayarlamanızı öneririz.
- Hem anahtarlar hem değerler dizedir. İç içe nesnelere ve dizilere izin verilmez.
- Dosyaları **UTF-8** olarak kaydedin.

```
my-translation-mod/
├── metadata.json              ← modules içine { "type": "localization", "path": "Localization" } ekleyin
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## Çevrilmemesi Gerekenler

Oyun metinleri, çalışma zamanında yorumlanan ifadeler içerir.
Aşağıdaki metinleri çevirmeyin; büyük/küçük harf dahil İngilizce halini olduğu gibi koruyun.

| Tür    | Örnek                                                                     | Açıklama                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Anahtar sözcükler   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Çalışma zamanında sayılarla veya adlarla değiştirilir. Küme parantezlerinin içindeki tek bir karakteri bile değiştirmeyin.   |
| Oyun etiketleri | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Anahtar sözcük ipuçları ve simgelerle değiştirilir. Etiketlerin içindeki adlar büyük/küçük harf dahil sabittir. |
| Biçimlendirme etiketleri | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Metin biçimlendirmesi. Açılış ve kapanış etiketleri eşleşmelidir.        |
| Satır sonları   | `\n`                                                                        | JSON dizesi içindeki bir satır sonu.                        |




## Yeni Bir Dil Ekleme

- Oyunun desteklemediği bir dili de ekleyebilirsiniz. Dosya adı yeni bir dil koduysa oyun onu yeni bir dil olarak kaydeder ve dil ayarları listesinde gösterir.
- Özgün dil dosyaları `<oyun kurulum klasörü>/Sephiria_Data/StreamingAssets/Localization/` altında, her dil için bir tane olacak şekilde bulunur; çeviri yaparken bunları referans alın.
- Oyunun halihazırda desteklediği diller: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH` ve `tr-TR`.
- **Her anahtarı çevirin.** Mod dosyanızda eksik olan anahtarlar boş gösterilmez; **İngilizce**ye (İngilizce de yoksa Koreceye) geri düşer. Başlamanın en kolay yolu özgün `en-US.json` dosyasını kopyalamaktır.
- **`#` ile başlayan ayar anahtarlarını doldurun.** `#LanguageName`, dil ayarları listesinde gösterilen addır; `#FontName` ise kullanılacak yazı tipidir. `#Colon`, `#Bullet` ve `#ParenthesisOpen` gibi noktalama anahtarlarını o dilin kurallarına göre ayarlayın.
- `#FontName` için oyunda yerleşik yazı tiplerinden birini seçebilirsiniz. Oyun aşağıdaki yazı sistemleri için yazı tipleri içerir:

| Yazı sistemi | `#FontName` |
|---|---|
| Latin, Kiril, Hangıl | `GalCyrillic` |
| Japonca | `JF-Dot-MPlus10` |
| Basitleştirilmiş Çince | `Fusion_Pixel_10_Simple` |
| Geleneksel Çince | `BoutiqueBitmap9x9` |
| Tayca | `NotoSansThai` |



## Sorun Giderme

| Belirti | Kontrol edilecekler |
|---|---|
| Mod etkin ama metin değişmiyor | Anahtar adının özgün adla her karakterine kadar eşleşip eşleşmediği. Dosya adının geçerli oyun diliyle eşleşip eşleşmediği (`ko-KR.json` yalnızca Korecede uygulanır). |
| Yalnızca bazı anahtarlar değişmiyor | Başka bir yerelleştirme modunun aynı anahtarın üzerine daha sonra yazıp yazmadığı. Paneldeki sırayı değiştirmeyi deneyin. |
| `{VALUE}` metinde olduğu gibi görünüyor | Anahtar sözcük özgün halinden farklı yazılmış. Doğrudan özgün metinden kopyalayın. |
| `<tag=…>` metinde olduğu gibi görünüyor | Etiket adının yazımı veya büyük/küçük harfi farklı. |
| Korece (veya İngilizce dışı başka) metin bozuk görünüyor | Dosya UTF-8 değil. UTF-8 olarak yeniden kaydedin. |
| Karakterler kutu olarak görünüyor | Bu karakterler oyunun yerleşik yazı tiplerinde yer almıyor. |
| Dosyanın tamamı yok sayılıyor | Dosyada bir JSON söz dizimi hatası var. `Player.log` hatayı dosya adıyla birlikte kaydeder. |

Günlük dosyası `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log` konumundadır. Hangi dosyaların okunduğunu ve her birinde kaç anahtar olduğunu görmek için "모드 내 언어 파일 발견" (modda dil dosyası bulundu) ifadesini arayın.
