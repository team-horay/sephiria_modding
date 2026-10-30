# DLL Modları

DLL modülleri, C# ile yazılmış kodu oyunun içinde çalıştırmanızı sağlar. Oyunculara, eşyalara, arayüze ve daha fazlasına bağlanmak için `HorayModAPI` olaylarına abone olun.
Bu belge, bir proje oluşturmaktan ilk DLL'inizi oyunda çalıştırmaya kadar her şeyi kapsar. Olayların ve kayıt işlevlerinin tam listesi için [API Referansı](api/README.md) sayfasına bakın.


## Gereksinimler

- [.NET SDK](https://dotnet.microsoft.com/download) 8 veya üzeri
- Bir C# düzenleyicisi


## Proje Oluşturma

[`examples/dll-sample`](../../examples/dll-sample) örneğine bakın. Bu belge onu referans olarak kullanır.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>9.0</LangVersion>
    <AssemblyName>PhysicalDamagePlus</AssemblyName>
    <RootNamespace>PhysicalDamagePlus</RootNamespace>
    <Nullable>disable</Nullable>
    <ImplicitUsings>disable</ImplicitUsings>
    <GenerateDependencyFile>false</GenerateDependencyFile>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <!-- Oyun kurulum klasörü. Kendi kurulumunuza göre değiştirin veya dotnet build -p:GameDir=... ile geçirin -->
    <GameDir Condition="'$(GameDir)' == ''">C:\Program Files (x86)\Steam\steamapps\common\Sephiria</GameDir>
    <OutDir>$(GameDir)\WorkshopLocal\physical-damage-plus\bin\</OutDir>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include="Assembly-CSharp">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Assembly-CSharp.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <Reference Include="UnityEngine.CoreModule">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\UnityEngine.CoreModule.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <!-- PlayerAvatar gibi ağ nesneleriyle çalışıyorsanız Mirror'ı, arayüz yapıyorsanız UnityEngine.UI'ı, yama yapıyorsanız 0Harmony'yi aynı şekilde ekleyin -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **`netstandard2.1` hedefleyin** ve dil sürümünü C# 9 olarak ayarlayın.
2. **Oyun DLL'lerine başvurun, ancak onları kopyalamayın.** Başvuruları `<oyun kurulum klasörü>/Sephiria_Data/Managed/` içindeki dosyalara yönlendirin ve hepsini `Private=false` olarak ayarlayın. Oyun DLL'leri mod klasörünüze kopyalanırsa aynı derleme iki kez yüklenir; bu da anlaşılmaz hatalara yol açar ve incelemeye gereksiz dosyalar ekler.
3. `Assembly-CSharp.dll`, oyunu çalıştıran kodun çoğunu içerir.


## Kod Yapısı


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // İşlem başına bir kez çağrılır. Olaylara burada ve yalnızca bir kez abone olun.
        protected override void OnModLoaded()
        {
            Debug.Log($"[PhysicalDamagePlus] Loaded v{metadata.modVersion} | folder={Context.PackageRoot}");

            HorayModAPI.OnPlayerAvatarSpawnedServerside += HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside += HandlePlayerSpawnedClient;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnPlayerAvatarSpawnedServerside -= HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside -= HandlePlayerSpawnedClient;
        }

        // Yalnızca sunucu sahibinde (sunucu) çağrılır. Oyun durumunu burada değiştirin.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Her katılımcıda çağrılır. Sunucunun eklediği değer eşitleme yoluyla gelir.
        private static void HandlePlayerSpawnedClient(PlayerAvatar player)
        {
            if (player == null)
                return;
            Debug.Log($"[PhysicalDamagePlus] PhysicalDamage={player.GetCustomStat(ECustomStat.PhysicalDamage)}");
        }
    }
}
```

```json
"modules": [
  { "type": "dll", "path": "bin/PhysicalDamagePlus.dll", "entryClass": "PhysicalDamagePlus.Entry" }
]
```

### WorkshopModBase

Sınıfınız temel sınıftan şunları devralır.

| Üye | Açıklama |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. `metadata.json` içindeki değerlerle doldurulur. |
| `Context` | `PackageRoot` (mod klasörünün mutlak yolu), `FileId` (Steam Atölyesi öğe kimliği; yerel paketler için bir Steam kimliğiyle asla çakışmayan ayrı bir değer), `Definition` (doğrulanmış bildirim), `ResolvePath(relativePath)`. Mod klasörünüzdeki dosyaları bulmak için kullanın. Kurulum yolu her kullanıcıda farklıdır; bu yüzden asla sabit kodlamayın. |
| `GetDataPackages()` | `consumer` olarak bu modu belirten `data` modülü paketlerinin listesi. Etkinleştirme tamamlandıktan sonra çağırın (bir `OnLoad*Database` işleyicisinin içinde veya `OnAllDatabasesReady` sonrasında). |

`Context`, `OnModLoaded` çağrıldığı andan itibaren geçerlidir. Yapıcıda henüz null'dır.


## Yaşam Döngüsü

DLL modları **bir oyun başlatıp dünyaya girdiğinizde** etkinleştirilir. Başlık ekranında henüz hiçbir kod çalışmaz. Başlangıç sırası şöyledir.

1. Dünya sahnesi yüklenir ve `GameDataLoader` başlar.
2. Yükleme planındaki DLL modülleri sırayla yüklenir, her giriş sınıfının örneği oluşturulur ve `OnModLoaded` çağrılır.
3. Veritabanları başlatılırken `OnLoadItemDatabase` ve `OnLoadUnitDatabase` gibi olaylar art arda tetiklenir.
4. Her şey bittiğinde `OnAllDatabasesReady` çağrılır.

## Abone Olma

`HorayModAPI` sınıfı, birçok duruma bağlanmanızı sağlayan önceden tanımlı olaylar sunar.

- Olaylara **yalnızca bir kez**, `OnModLoaded` içinde abone olun. Veritabanı olayları her yüklemede yeniden tetiklenir; bu yüzden "her seferinde olması gereken" işleri (eşya kaydetme vb.) işleyicilerin içinde yapın.
- Abone olmayı ve abonelikten çıkmayı dikkatle yönetin. Abone olduğunuz olaylardan `OnModUnloaded` içinde çıkın (-=).

Olayların tam listesi [API Referansı](api/README.md) sayfasındadır. Aşağıdaki tablo, yapmak isteyebileceğiniz her şey için önerilen olayı listeler.

| Yapmak istediğiniz | Olay |
|---|---|
| Eşya veya eser ekleme | `OnLoadItemDatabase` |
| Silah, mucize, pasif, kostüm veya prop ekleme | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Düşman veya NPC birimi ekleme | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Gezgin (köy NPC'si) ekleme | `OnLoadSocialIDDatabase` |
| Nitelik gösterimi ekleme | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Koddan yerelleştirilmiş metin kaydetme | `OnLocalizationReady` (`AddText`) |
| Başlık menüsüne düğme ekleme | `OnTitleMenuReady` |
| Oyun veya oturum başlangıcı | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| Bir kat oluşturulduğunda | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Oyuncu veya birim doğuşu | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Savaş denetimlerine bağlanma | `OnPlayerEvade`, `OnGetDebuff` |
| Envanter düzenleme izinleri | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Çakışmalardan Kaçınma

Modunuzun eklediği varlıklar `<modId>:<id>` biçiminde adlandırılır, örn. `yourname.artifact-pack:flame-ring`. Bu kural diğer modlarla çakışmaları önler ve mod içeriğinin oyun geçmişinde ve istatistiklerde birbirinden ayırt edilmesini sağlar.


## Sunucu Tarafı, İstemci Tarafı

- Sephiria çok oyunculu etrafında inşa edilmiştir; bu yüzden birçok olayın ayrı sunucu tarafı ve istemci tarafı sürümleri vardır.
- Sunucu sahibi her ikisini de tetikler; diğer katılımcılar yalnızca Clientside olanı tetikler.
- Kural olarak, oyun durumunu değiştiren işleri (doğurma, düşürme, hasar) yalnızca Serverside'da, yalnızca gösterimi değiştiren işleri ise Clientside'da yapın.


## Harmony Yamaları

Olayların ulaşamadığı bir şeyi değiştirmeniz gerektiğinde Harmony kullanabilirsiniz.
1.1.0 yamasından itibaren **Harmony 2.4.3 oyuna yerleşiktir**; bu yüzden yalnızca ona bir başvuru ekleyin.

```xml
<Reference Include="0Harmony">
  <HintPath>$(GameDir)\Sephiria_Data\Managed\0Harmony.dll</HintPath>
  <Private>false</Private>
</Reference>
```

```csharp
protected override void OnModLoaded()
{
    var harmony = new HarmonyLib.Harmony("yourname.first-mod");
    harmony.PatchAll();
}
```

- **`0Harmony.dll` dosyasını modunuzla birlikte paketlemeyin.** Paketlenmiş kopya yok sayılır; farklı bir sürüm eklemiş olsanız bile modunuz o sürümde çalışmaz.
- Harmony kimliği olarak `modId` değerinizi olduğu gibi kullanmak başkalarıyla çakışmasını önler.
- Yamalar, oyun güncellemelerinde resmi API'den çok daha kolay bozulur. Yapabildiklerinizi olaylarla yapın ve yamaları son çare olarak saklayın.
- BepInEx ve MelonLoader gibi harici yükleyiciler desteklenmez.


## Yardımcı DLL'ler

Modunuz başka kütüphaneler kullanıyorsa (örn. Newtonsoft.Json dışındaki üçüncü taraf paketler), DLL'lerini mod klasörünüze koyun ve `metadata.json` içindeki `libraries` alanında bildirin. Bildirilmeyen DLL'ler başvuru çözümlemesinde kullanılmaz.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Oyunla birlikte gelen DLL'leri (Unity, Mirror, Newtonsoft.Json, 0Harmony vb.) dahil etmeyin. Önce oyunun kopyası yüklenir; bu yüzden modunuzun içerdiği kopya ya yok sayılır ya da çakışır.
- Yerel (C/C++) DLL'ler kullanılamaz. Yalnızca yönetilen derlemeler yüklenir.
- Aynı ada sahip bir derleme başka bir mod tarafından zaten yüklenmişse, oyun ikisinden birini seçmez; çalıştırmayı **askıya alır**. Paylaşılan kütüphaneleri ayrı bir moda ayırıp `dependencies` ile bağlamak daha güvenlidir.

### Çalışma Zamanında DLL Oluşturmayın!

Mod çalışırken moddaki DLL dosyaları değişirse o modun yüklenmesi kısıtlanır. Tüm DLL'leri herhangi bir sıkıştırma veya akış (streaming) olmadan, olduğu gibi Atölye öğesine dahil edin. Yani aşağıdakiler inceleme sistemini ihlal edebilir. İhtiyacınız olan tüm DLL'leri yüklerken moda koyun.

- Mod çalışırken dışarıdan bir DLL indirip derlemeyi yükler
- Mod çalışırken kendi DLL'ini oluşturup çalıştırır
- Mod, içinde kendi sıkıştırma yöntemiyle paketlenmiş bir DLL'i çıkarıp kullanır


## Derleme ve Test Etme

```bash
dotnet build -c Release
```

- Derleme başarılı olup DLL `WorkshopLocal/<mod>/bin/` içinde göründüğünde oyunu başlatın, başlık ekranındaki Steam Atölyesi panelinde "Modlarım" altında modunuzun göründüğünü kontrol edin, ardından bir oyun başlatın.
- `.pdb` dosyasını derleme çıktısının yanında tutarsanız günlükteki yığın izleri satır numaralarını içerir. Yayınlarken bunu dışarıda bırakın.


## Oyun Güncellemeleri ve Uyumluluk

Oyun güncellendiğinde `Assembly-CSharp.dll` değişir. Yalnızca resmi olayları kullanan modlar genellikle çalışmaya devam eder, ancak iç sınıflara doğrudan dokunan veya Harmony ile yama yapan modlar bozulabilir.

- Güncellemeden sonra yeni `Managed/` klasörüne karşı yeniden derleyin ve test edin.
- Atölye sayfanızın açıklamasında **desteklenen oyun sürümünü** belirtirseniz kullanıcılar sorunun nedenini hızla bulabilir. Yayınlarken oyun sürümü Atölye öğesinin meta verilerine otomatik olarak da kaydedilebilir.
- Kodunuzu değiştirdiğinizde inceleme sistemine yeniden gönderilmesi gerekir. [Atölye'de Yayınlama](publishing.md#kod-i̇ncelemesi) sayfasına bakın.


## Çok Oyunculu Hususlar

- Çoğu kod modunda `multiplayer.installation` değeri `everyone` olacaktır.
- Modunuz oyun durumunu yalnızca `Serverside` olaylarında değiştiriyorsa ve değiştirdiği değerler zaten eşitlenen oyun durumuysa (örn. `customStats`), yani katılımcıların koda veya kayda ihtiyacı yoksa, `host_only` olarak bildirebilirsiniz. Katılımcılar o zaman modu indirmez. Yeni eşya kaydeden, `RegisterNetworkPrefab` kullanan veya `Clientside` olaylarında herhangi bir şeyi değiştiren bir mod `host_only` olamaz.
- Ağ üzerinden doğan nesnelerin (birimler, mermiler vb.) katılımcıların tarafında da oluşturulabilmesi için `RegisterNetworkPrefab` ile kaydedilmesi gerekir. Bir `stableKey` geçirirseniz mod sürümünüz değişse bile aynı kimlik korunur.
- Sunucu sahibi ve katılımcıların mod **yükleme sırası** bile aynı olmalıdır. Sonuçları bu sıraya bağlı olan kodlardan kaçının.


## Sorun Giderme

| Belirti | Kontrol edilecekler |
|---|---|
| Durum özeti giriş sınıfının bulunamadığını söylüyor | `entryClass` içindeki ad alanı veya sınıf adı kodunuzla eşleşmiyor. İç içe sınıflar `Outer+Inner` olarak yazılır. |
| Durum özeti giriş sınıfı tür hatası gösteriyor | Giriş sınıfı `WorkshopModBase` (veya `HorayModBase`) sınıfından türemiyor, public değil, abstract ya da public parametresiz yapıcısı yok. |
| Durum özeti modun derleme adı çakışması nedeniyle askıya alındığını söylüyor | Başka bir mod (AddOns dahil) aynı `AssemblyName` değerini kullanıyor. Sizinkini yeniden adlandırın. |
| Durum özeti kütüphane yükleme hatası gösteriyor | `libraries` içinde listelenen bir DLL eksik ya da yönetilen bir derleme değil. |
| Günlükte `FileNotFoundException` | Başvurulan bir DLL mod klasörünüzde yok ya da `libraries` içinde bildirilmemiş. Ya da bir oyun DLL'i `Private=true` ile kopyalanmış ve ikisi çakışıyor. |
| Dünyaya girdikten hemen sonra istisna | Bir veritabanı olay işleyicisinde istisna. Günlüğün yığın izinde modunuzun ad alanını arayın. |
| Başlığa dönüp yeniden girdikten sonra etkiler üst üste biniyor | Bir işleyicinin içinde yeniden abone oluyorsunuz. Yalnızca bir kez, `OnModLoaded` içinde abone olun. |


## Örnek

[`examples/dll-sample`](../../examples/dll-sample), oyuncu doğduğunda Fiziksel Hasar'ı 5 artıran minimal bir moddur. `package/` klasörü önceden derlenmiş bir DLL içerir; bu yüzden o klasörü `WorkshopLocal/` içine kopyalamak derleme yapmadan hemen çalışmasını sağlar. Kendi modunuza dönüştürmek için yalnızca `AssemblyName`, `modId`, `entryClass` ve çıktı yolunu değiştirmeniz yeterlidir. Bir oyun başlatın ve nitelik penceresini açarak Fiziksel Hasar +5 olarak çalıştığını görün.
