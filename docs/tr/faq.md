# SSS


## Günlükleri nasıl görüntülerim?

Oyun günlükleri aşağıdaki dosyaya yazılır. Oyunu her başlattığınızda yeniden yazılır; bir önceki çalıştırma `Player-prev.log` dosyasında saklanır.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

Modla ilgili satırlar köşeli parantez içinde bir etiketle başlar; bu yüzden o etiketi arayın.

| Etiket | İçerik |
|---|---|
| `[WorkshopModManager]` | Abonelik listesi sorgulama, indirmeler, yerel klasör tarama, yükleme planı özeti |
| `[WorkshopModRuntime]` | DLL etkinleştirme ve yerelleştirme birleştirme sonuçları |
| `[WorkshopUploader]` | Atölye'ye yükleme ve inceleme gönderimi |
| `[ModGuardClient]` | İnceleme sunucusuyla iletişim |
| `[WorkshopLobbyMods]` | Çok oyunculu odalarda mod setlerinin karşılaştırılması |
| `모드 내 언어 파일 발견` (Korece, "modda dil dosyası bulundu") | Her yerelleştirme dosyasından okunan anahtar sayısı |


## Steam Atölyesi panelindeki "Yerel mod (AddOns klasörü)" nedir?

Panelde "Yerel mod (AddOns klasörü)" adlı bir girdi görebilirsiniz. Bu eski bir mod özelliğidir; yeni modlar için kullanmayın.
Eski modlar diğer oyuncularla eşitlenemez; bu yüzden kaldırmanızı öneririz.


## WorkshopLocal klasörünü Sephiria kurulum klasörü dışında bir yere taşımak istiyorum.

Modlarınızı başka bir sürücüde veya bir çalışma klasöründe tutmak istiyorsanız, yolu başlatma argümanı olarak geçirebilirsiniz. Steam kütüphanenizde Sephiria → Özellikler → Genel → Başlatma Seçenekleri'ne gidin ve şunu girin:

```
-workshop_local=D:\mods\my-first-mod
```

Birden fazla yolu noktalı virgülle ayırın; bir yol boşluk içeriyorsa tamamını tırnak içine alın.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

Argüman olarak geçirilen klasörler, tam olarak `WorkshopLocal/` alt klasörleri gibi ele alınır.


## uploadticket.json dosyasını kaybettim.

`mod-guard-manifest.json` ve `mod-guard.sig.json` 1.1.1 sürümünden itibaren artık kullanılmıyor; bu yüzden kaybetmeniz sorun değil. İhtiyacınız olan tek dosya `uploadticket.json`.

`uploadticket.json` dosyasını kaybettiyseniz (örneğin biçimlendirmeden sonra), oyun klasörü yeni bir mod olarak ele alır; bu haliyle yüklemek mevcut öğeyi güncellemek yerine yeni bir Atölye öğesi oluşturur. Mevcut öğeyi güncellemeye devam etmek için şu sırayla kurtarın:

1. Atölye'de kendi modunuza abone olun.
2. Steam'in indirdiği klasörün tamamını (`<Steam kütüphanesi>/steamapps/workshop/content/2436940/<Atölye öğe kimliği>/`) `WorkshopLocal/` içine kopyalayın. Klasör adı önemli değildir.
3. Kopyalanan klasörde, aşağıdaki içerikle `uploadticket.json` oluşturun. Sayı için Atölye sayfası URL'sinde `id=` ifadesinden sonra gelen rakamları kullanın.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. Oyunu başlatın, panelde modu seçin ve **Atölye'ye yükle** düğmesine basın. **Mevcut öğe güncellenir**; kod içeren bir modsa aynı öğe kimliğiyle yeniden incelemeye gönderilir. (Not: Mevcut bir mod klasörünü kopyalayıp farklı bir mod yaptıysanız, `uploadticket.json` dosyasını **mutlaka silmelisiniz**. Bırakırsanız yeni mod, özgün modun Atölye öğesinin üzerine yazar.)
