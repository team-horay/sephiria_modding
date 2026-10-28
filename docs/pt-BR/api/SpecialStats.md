<!-- Arquivo gerado automaticamente. Tools/ModApiDocGen no repositório do jogo o constrói a partir dos comentários /// no código-fonte. Não edite diretamente; corrija os comentários no código-fonte e gere novamente. -->
# Atributos especiais

Tipos de exibição de atributos registrados com `RegisterSpecialStatCategory` e `RegisterSpecialStat` em `HorayModAPI.OnLoadSpecialStatDatabase`.

Gerado em 2026-10-26 para a versão **1.1.0** do jogo. [Índice da referência](README.md)

| Tipo | Descrição |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Informações do avatar alvo passadas às funções de leitura de valor. |
| [`SpecialStatValue`](#specialstatvalue) | O valor de uma linha de atributo especial. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Dados de uma categoria de Atributos especiais na UI de atributos. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Identifica quando uma categoria é exibida. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Dados detalhados de `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Identifica de onde o valor de exibição de um atributo especial é lido. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Cor do nome de uma linha de atributo especial. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | Um asset que lê apenas o valor de exibição, sem um StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Informações do avatar alvo passadas às funções de leitura de valor.

### Propriedades

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Unidade alvo


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

O AvatarStatsHooker do alvo. Null se não houver.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

O controlador de armas do alvo. Null se não houver.

### Construtores

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

Se hooker ou weapons for omitido, ele é obtido a partir de avatar.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

O valor de uma linha de atributo especial. Crie valores numéricos com `Number`.

### Constantes e campos

#### text

```csharp
public string text
```

String de exibição


#### hasStat

```csharp
public bool hasStat
```

Se há um valor


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Cor de aumento/redução


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Se `numericValue` é válido


#### numericValue

```csharp
public double numericValue
```

Número para exibição


#### numberFormat

```csharp
public string numberFormat
```

Formato de exibição do número


#### symbolKey

```csharp
public string symbolKey
```

Chave de localização do símbolo da unidade


#### symbol

```csharp
public string symbol
```

String do símbolo da unidade


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Valores de substituição de palavras-chave para a dica

### Métodos

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Cria um valor numérico.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Dados de uma categoria de Atributos especiais na UI de atributos.

### Constantes e campos

#### id

```csharp
public string id
```

Identificador


#### enabled

```csharp
public bool enabled
```

Se a categoria deve ser exibida


#### displayName

```csharp
public LocalizedString displayName
```

Chave de localização do título


#### fallbackName

```csharp
public string fallbackName
```

Título usado quando não há tradução


#### headerColor

```csharp
public Color headerColor
```

Cor do título


#### order

```csharp
public int order
```

Ordem de classificação


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Condição de exibição


#### requireWeapon

```csharp
public bool requireWeapon
```

Se deve ser exibida apenas quando uma arma específica estiver equipada


#### weaponType

```csharp
public EWeaponType weaponType
```

Tipo de arma exigido quando `requireWeapon` está definido

### Propriedades

#### DisplayName

```csharp
public string DisplayName { get; }
```

Título traduzido


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Identifica quando uma categoria é exibida.

| Valor | Descrição |
|---|---|
| `Always` | Sempre |
| `WhenAnyStatHasValue` | Apenas quando pelo menos uma linha tem valor |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Dados detalhados de `StatusEntity.specialStat`. Determina como este atributo é exibido na UI de atributos e na UI de histórico de partidas.

### Constantes e campos

#### enabled

```csharp
public bool enabled
```

Se deve ser exibido no painel


#### category

```csharp
public SpecialStatCategoryEntity category
```

Categoria à qual pertence (obrigatório)


#### order

```csharp
public int order
```

Ordem de classificação dentro da categoria


#### showWhenZero

```csharp
public bool showWhenZero
```

Se a linha deve ser exibida mesmo quando não há valor


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Chave de localização de um nome usado apenas no painel de Atributos especiais


#### fallbackLabel

```csharp
public string fallbackLabel
```

Nome usado quando não há tradução


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Escolha da cor do nome


#### labelColor

```csharp
public Color labelColor
```

Cor do nome quando `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Como o valor é lido


#### valueId

```csharp
public string valueId
```

ID de consulta do StatHook ou chave de atributo personalizado. Se vazio, StatusEntity.id é usado.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Asset que lê o valor quando `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

Divisor para exibição


#### displayOffset

```csharp
public float displayOffset
```

Deslocamento somado para exibição


#### numberFormat

```csharp
public string numberFormat
```

Formato de exibição do número


#### includeSign

```csharp
public bool includeSign
```

Se o sinal deve ser exibido


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Identifica de onde o valor de exibição de um atributo especial é lido.

| Valor | Descrição |
|---|---|
| `StatHook` | Resultado de `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Valor de `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | Resultado de `Read` do asset `valueProvider` |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Cor do nome de uma linha de atributo especial.

| Valor | Descrição |
|---|---|
| `Default` | Branco |
| `Keyword` | Cor da palavra-chave relacionada |
| `Override` | O valor de `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

Um asset que lê apenas o valor de exibição, sem um StatusInstance.

### Métodos

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Lê o valor de exibição atual de status.
