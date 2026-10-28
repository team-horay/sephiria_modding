<!-- Otomatik üretilen dosya. Oyun deposundaki Tools/ModApiDocGen, kaynaktaki /// yorumlarından oluşturur. Doğrudan düzenlemeyin; kaynak yorumlarını düzeltip yeniden üretin. -->
# Oda Doğuş Ataması

`HorayModAPI.OnAssignRoomSpawnPhases` olayının geçirdiği bağlam. Kat üretecinin bir odaya canavar evrelerini atadığı ana bağlanır.

Oyun sürümü **1.1.0** için 2026-10-26 tarihinde üretildi. [Referans dizini](README.md)

| Tür | Açıklama |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | FloorGenerator rastgele tabanlı düşmanları atarken çağrılan bağlam. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Atanan odanın türü |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

FloorGenerator rastgele tabanlı düşmanları atarken çağrılan bağlam. Yalnızca sunucuda (sunucu sahibi) tetiklenir.

Kurallar
- `Phases`, sahne tablosunun derin bir kopyasıdır. Değiştirebilir veya tamamen yerine başkasını koyabilirsiniz. null bırakmak özgün kopyaya geri döner.  
- `Sources`, özgün sahne varlığıdır. Salt okunurdur (değiştirmek düzenleyici varlığını kirletir).  
Oda atamasında `Phases` (kopya) boş bir containedFactions'a sahiptir (GeneratePhaseData bunu kopyalamaz). Hizipleri Sources'tan okuyun.
- Rastgelelik için `Seed` değerinden `new System.Random(ctx.Seed)` oluşturun. UnityEngine.Random'ı veya katın RNG'sini tüketmek, yeniden üretimde veya yeniden katılmada katın farklı olmasına yol açar.  
- Bir modun spawnData'ya eklediği varlıkların, kayıt geri yükleme ve istemci tarafı doğuşun çalışması için UnitDatabase'de kayıtlı olması gerekir.

### Özellikler

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

Bu atamayı gerçekleştiren kat


#### Stage

```csharp
public StageEntity Stage { get; }
```

Katın sahnesi. Bulunamazsa null.


#### Floor

```csharp
public FloorData Floor { get; }
```

Kat verisi (tohum, zorluk, sahne adı). Sunucu verisi yoksa null.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Havuzu seçmek için kullanılan nihai zorluk (oyuncu sayısını ve DIFFICULTY_UP değerini içerir)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Atanan odanın türü


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Bu atamanın kattaki dizini (0 tabanlı). Her üreteç örneği için 0'dan başlar.


#### Seed

```csharp
public int Seed { get; }
```

Hash(kat tohumu, atama dizini). Modun RNG'si için tohum.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Özgün sahne varlıkları (salt okunur). Bir oda için bir tane, bir savaş bölgesi için phaseCount kadar.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

İlk özgün varlık. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Atanacak kopya. Değiştirilebilir.

### Yapıcılar

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Henüz açıklama yok.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Atanan odanın türü

| Değer | Açıklama |
|---|---|
| `Normal` | Normal savaş odası (pusu) |
| `Weak` | Zayıf savaş odası (zorluk ≤ 1 havuzu, pusu yok) |
| `BattleZone` | Savaş bölgesi (birden fazla evreyi birleştirir, ardından kancayı bir kez çalıştırır) |
