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
- BepInEx ve MelonLoader gibi harici yükleyiciler desteklenmez. Harici bir yükleyici gibi çalışan bir mod (Atölye dışındaki eklenti DLL'lerini bulup çalıştıran bir mod) uyarı alır ve yalnızca kullanıcı onayladıktan sonra etkinleştirilir. Ayrıntılar için [Atölye Dışındaki Kodu Çalıştıran Modlar](#atölye-dışındaki-kodu-çalıştıran-modlar) bölümüne bakın.


## Yardımcı DLL'ler

Modunuz başka kütüphaneler kullanıyorsa (örn. Newtonsoft.Json dışındaki üçüncü taraf paketler), DLL'lerini mod klasörünüze koyun ve `metadata.json` içindeki `libraries` alanında bildirin. Bildirilmeyen DLL'ler başvuru çözümlemesinde kullanılmaz.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Oyunla birlikte gelen DLL'leri (Unity, Mirror, Newtonsoft.Json, 0Harmony vb.) dahil etmeyin. Önce oyunun kopyası yüklenir; bu yüzden modunuzun içerdiği kopya ya yok sayılır ya da çakışır.
- Yerel (C/C++) DLL'ler kullanılamaz. Yalnızca yönetilen derlemeler yüklenir.
- Aynı ada sahip bir derleme başka bir mod tarafından zaten yüklenmişse, `modules` içindeki bir DLL için oyun ikisinden birini seçmez; çalıştırmayı **askıya alır**. `libraries` içindeki bir DLL için ise oyun zaten yüklü olanı kullanır ve sizin kopyanızı yok sayar; sürümler farklıysa bu hatalara yol açabilir. Paylaşılan kütüphaneleri ayrı bir moda ayırıp `dependencies` ile bağlamak daha güvenlidir.

### Başka Bir Modun Kütüphanesini Kullanma

Birden fazla mod aynı kodu kullanıyorsa, bu kodu tek bir üst moda koyun ve alt modları `dependencies` ile ona bağlayın.

1. Üst mod, paylaşılan kodu `modules` veya `libraries` içinde bildirir ve `tags` alanına `Library` ekler.
2. Alt mod, `dependencies` içine üst modun `modId` değerini yazar ve üst modun DLL'ini `Private=false` ile referans alarak derlenir. **Üst modun DLL'ini alt moda kopyalamayın.**
3. Oyun önce üst modu yükler; bu yüzden alt modun referansları, zaten yüklenmiş olan üst modun derlemesine çözülür.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- Alt modun yine de kendi `dll` modülü ve `entryClass` değeri olmalıdır. Üst modun, alt modun DLL'lerini kendi başına bulup yüklediği bir yapı desteklenmez. Bunun yerine alt modun kendi `OnModLoaded` metodundan üst modun kayıt işlevini çağırmasını sağlayın.
- Üst mod bir güncellemede herkese açık türlerini veya üyelerini değiştirirse alt modlar bozulabilir. Paylaşılan bir kütüphanede mevcut herkese açık API'leri silmemeye veya değiştirmemeye dikkat edin.

### Çalışma Zamanında DLL Oluşturmayın!

Bir modun çalıştırdığı tüm kod, incelemeden geçmiş bir Atölye öğesinin içinde olmalıdır. Mod çalışırken moddaki DLL dosyaları değişirse o modun yüklenmesi kısıtlanır. Tüm DLL'leri herhangi bir sıkıştırma veya akış (streaming) olmadan, olduğu gibi Atölye öğesine dahil edin. Yani aşağıdakiler inceleme sistemini ihlal edebilir. İhtiyacınız olan tüm DLL'leri yüklerken moda koyun.

- Mod çalışırken dışarıdan bir DLL indirip derlemeyi yükler
- Mod çalışırken kendi DLL'ini oluşturup çalıştırır (Mono.Cecil gibi bir araçla başka bir DLL'i yeniden yazıp sonra çalıştırmak da dahil)
- Mod, içinde kendi sıkıştırma yöntemiyle paketlenmiş bir DLL'i çıkarıp kullanır

### Atölye Dışındaki Kodu Çalıştıran Modlar

Kullanıcının oyun kurulum klasörü veya `AppData` gibi **mod klasörünün dışına** koyduğu DLL'leri bulup çalıştıran bir mod (örneğin başka bir yükleyicinin eklentileriyle uyumluluk sağlayan bir mod) reddedilmez, ancak inceleme sistemi ona bir **uyarı** ekler.

- Kullanıcı modu etkinleştirirken bir uyarı penceresi çıkar ve mod yalnızca kullanıcı onaylarsa etkinleşir. Mod güncellendiğinde kullanıcıya yeniden sorulur.
- Bu şekilde çalıştırılan harici DLL'ler incelemeden geçmez ve bunların yol açtığı sorunlar için destek verilmez.
- Bir modun bu tür DLL'leri çalışırken kendisinin indirmesi, yukarıdaki yasak durumlara girer.

Başka bir modun kodunu kullanmak için o modun klasöründen doğrudan DLL yüklemeyin; [Başka Bir Modun Kütüphanesini Kullanma](#başka-bir-modun-kütüphanesini-kullanma) bölümünde anlatıldığı gibi `dependencies` ile bağlayın. Oyunun kendisinin yüklemediği DLL'ler yükleme sırası, ad çakışması ve bağımlılık denetimlerini atladığı için desteklenmez.

Bir DLL'i yeniden yazmanız gerekiyorsa (örneğin başka bir Harmony API'sine uygun IL dönüşümü), bunu yüklemeden önce yapın ve dönüştürülmüş DLL'i modunuza ekleyin.


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


## Ağ Eşitleme (SyncVar, RPC, Command)

Sephiria'nın çok oyunculu özelliği, açık kaynaklı ağ kütüphanesi "Mirror" ile gerçekleştirilmiştir. Mirror'da aşağıdaki öznitelikleri bildirerek iletişim özelliklerini kolayca kullanabilirsiniz.

- `[SyncVar]`: Sınıf üye değişkenine eklenen özniteliktir. Bir değişkenin değerini sunucu ile istemciler arasında eşitler.
- `[ClientRpc]`, `[TargetRpc]`: İşleve eklenen özniteliklerdir. Sunucu, istemcilerden bir işlevi çalıştırmalarını ister.
- `[Command]`: İşleve eklenen özniteliktir. İstemci, sunucudan bir işlevi çalıştırmasını ister.

Mod ortamında yukarıdaki öznitelikler düzgün kullanılamaz. Bunlar ancak kod derlenirken Mirror'ın Weaver'ı her özniteliğe karşılık gelen iletim kodunu eklediğinde çalışır; dışarıda üretilen DLL'ler Weaver'dan geçmediği için derleme başarılı olsa bile bu öznitelikler hiçbir işlev görmez. Bu tür hata, sunucu sahibinin ekranında normal görünür ve yalnızca katılımcının ekranında sorun çıkarır; bu yüzden tek başınıza test ederek fark etmek zordur. (DLL'i zorla Weaver'dan geçirseniz bile işlev karması çakışması gibi hatalar olabilir.)


### `SyncVar<T>`, `ModRpc`

Bunu çözmek için Sephiria, yukarıdaki özniteliklerin yerini alan mod odaklı iletişim kodunu sunar: `SyncVar<T>` ve `ModRpc`.

| Yapmak istediğiniz | Kullanılacak |
|---|---|
| Bir değeri sürekli eşit tutmak (sonradan giren katılımcılar dahil) | `SyncVar<T>` |
| Sunucudan tüm istemcilere çağrı | `ModRpc.Send` |
| Sunucudan belirli bir istemciye çağrı | `ModRpc.SendTo` |
| İstemciden sunucuya çağrı | `ModRpc.SendCommand` |
| Oyunun zaten eşitlediği bir değeri değiştirmek | `Network<FieldName>` özelliği |

- Bu özellik oyunun 1.1.2 sürümünden itibaren kullanılabilir.
- Bu özelliği kullanan modlarda `multiplayer.installation` değerini `everyone` olarak bildirin.


### Bileşen Hazırlama

`SyncVar<T>` ve `ModRpc` yalnızca `NetworkBehaviour` sınıfından türeyen bileşenlerde kullanılabilir. Bu bileşen, hem sunucu sahibinde hem de katılımcıda aynı nesneye **aynı sırayla** eklenmiş olmalıdır. Zaten doğmuş bir nesneye `AddComponent` ile eklenen bileşen eşitleme kapsamına girmez; bu yüzden nesneyi ağ prefab'ı olarak kaydetmeden önce eklenmiş olması gerekir.

```csharp
private static GameObject prefab;

// OnModLoaded içinde HorayModAPI.OnAllDatabasesReady olayına abone olun.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Etkin olmayan bir üst nesnenin altına koyun. Prefab'ın Awake'i çalışmaz, Instantiate ile oluşturulan kopya ise etkin olarak başlar.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Sunucuda doğurulur. İkinci argümanı verirseniz o oyuncu sahip olur.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: Değer Eşitleme

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Weaver olmadığı için yapıcıda elle kaydedin. Alanların sırası her zaman aynı olmalıdır.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Değeri **sunucuda** `stack.Value = 3` gibi değiştirin. Katılımcının değiştirdiği değer sunucuya iletilmez.
- Sonradan giren katılımcı da girişte mevcut değeri alır.
- `Callback`, sunucu sahibinde değer atandığında, katılımcıda ise değer alındığında çağrılır. Girişte alınan değer varsayılandan farklıysa o sırada da çağrılır. Geri çağırmada oluşan istisna günlüğe yazılır ve eşitleme sürer.
- `int`, `float`, `string`, `Vector3` gibi temel türleri kullanabilirsiniz. Modun oluşturduğu struct'lar için serileştirme kodu otomatik üretilmez; bu yüzden onları alanlara bölerek kullanın.
- Başka bir nesneye işaret etmek için `SyncVar<GameObject>` yerine `netId` değerini `SyncVar<uint>` ile gönderin. Alan tarafta o nesne henüz oluşturulmamış olabilir.
- Bir bileşene, devralınanlar dahil en fazla 64 SyncVar kaydedilebilir.

### `ModRpc`: İşlev Çağırma

Mod yüklenirken işlevi bir kez kaydedin. Çağırırken adı `modId:functionName` biçimindedir.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Sunucu → bu nesneyi gören tüm istemciler
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Sunucu → tek bir istemci. Bağlantı yerine null verirseniz nesnenin sahibine gönderilir.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// İstemci → sunucu
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Yazdığınız sırayla okuyun. Yazma ve okumanın sırası ya da türleri farklıysa değerler bozulur veya istisna oluşur.
- `RegisterCommand`, varsayılan olarak yalnızca **nesnenin sahibi** tarafından çağrılabilir. Sahibi olmayan bir nesnede çağrı almak için `requiresAuthority: false` verin. Bu durumda herhangi bir katılımcı çağırabilir. Hile gibi risklere dikkat edin.
- Kaydedilen yönden farklı bir yönde göndermek ya da kaydedilmemiş bir ad göndermek yalnızca hata günlüğü bırakır ve gönderim yapılmaz.
- Alan tarafta işlev yoksa ya da bileşen türü farklıysa yalnızca uyarı günlüğü bırakılır ve çağrı yok sayılır. İşleyicide oluşan istisna da günlüğe yazılır ve oyun çalışmaya devam eder.
- Varsayılan kanal güvenilir iletimdir. Sık gönderilen efektler için `channelId: Channels.Unreliable` argümanını verebilirsiniz.

### Mutlaka Uyulması Gerekenler

- İstemci, Command aracılığıyla rastgele değerler gönderebilir. Yuva numarası, miktar, hedef gibi değerleri sunucu işleyicisinde doğrulayın.
- Sonradan giren katılımcı, girişinden önce gönderilen RPC'leri almaz. Korunması gereken durumu `SyncVar<T>` ile gönderin; RPC'yi ses ve efekt gibi gelip geçen işler için kullanın.
- **RPC, SyncVar'dan önce gelebilir.** RPC işleyicisinde `stack.Value` değerini okursanız önceki değer olabilir. Yeni değere ihtiyacınız varsa onu RPC argümanıyla birlikte gönderin ya da `Callback` içinde işleyin.

### Sephiria Ana Oyun Kodundaki Değerleri Değiştirme

Bir oyun sınıfının `[SyncVar]` alanına doğrudan atama yapmak katılımcılara iletilmez. Başına `Network` eklenmiş özelliği kullanın. Büyük/küçük harf, alan adıyla aynıdır.

```csharp
unit.maxHp = 500;         // Yalnızca sunucu sahibinin değeri değişir.
unit.NetworkmaxHp = 500;  // Katılımcılara da iletilir.
```

Nesne doğmadan önce (prefab'da ya da `Instantiate` hemen sonrasında) doğrudan atama yapsanız da değer doğma sırasında birlikte iletilir.

### Test Etme

Sunucu sahibi tek başına bu sorunları ortaya çıkaramaz. Yayınlamadan önce iki bilgisayarla hem sunucu sahibinin hem de katılımcının ekranını kontrol edin ve değeri değiştirdikten sonra sonradan giren katılımcı durumunu da deneyin. Yerel paketi (`WorkshopLocal`) etkinleştirirseniz başkaları odaya giremez; bu yüzden çok oyunculu testleri Atölye'ye Unlisted olarak yüklenmiş bir öğeye abone olarak yapın. İlk yüklenen öğe otomatik olarak Unlisted olur.


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
