<!-- Arquivo gerado automaticamente. Tools/ModApiDocGen no repositório do jogo o constrói a partir dos comentários /// no código-fonte. Não edite diretamente; corrija os comentários no código-fonte e gere novamente. -->
# Opções de mod

API para configurações por mod salvas por dispositivo, e para adicionar interruptores, caixas de seleção e controles deslizantes à tela de opções do jogo. Obtenha-a por `Context.Options` ou `HorayModAPI.GetModOptions(modId)`.

Gerado em 2026-10-26 para a versão **1.1.0** do jogo. [Índice da referência](README.md)

| Tipo | Descrição |
|---|---|
| [`ModOptions`](#modoptions) | Valores de opções que cada mod pode definir. |
| [`ModOptionsPage`](#modoptionspage) | A página de um mod na tela de opções. |
| [`ModOptionDefinition`](#modoptiondefinition) | Definição comum de um item da tela de opções. |
| [`ModOption<T>`](#modoptiont) | Um item de opção que guarda um valor. |
| [`ModToggleOption`](#modtoggleoption) | Uma opção de ligar/desligar. |
| [`ModSelectionOption`](#modselectionoption) | Uma opção de caixa de seleção. |
| [`ModSliderOption`](#modslideroption) | Uma opção de controle deslizante. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Tipo de linha na tela de opções. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Lista de páginas de mods que a tela de opções lê. |


## ModOptions

```csharp
public sealed class ModOptions
```

Valores de opções que cada mod pode definir.

### Propriedades

#### ModId

```csharp
public string ModId { get; }
```

O modId deste conjunto de opções


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Se o armazenamento está inicializado


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Nome do mod exibido na tela de opções. Para mods da Oficina Steam, o padrão é o nome de exibição do manifesto.

### Métodos

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Valor bool salvo. Retorna fallback se estiver ausente ou for de outro tipo.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Valor int salvo. Retorna fallback se estiver ausente ou for de outro tipo.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Valor long salvo. Retorna fallback se estiver ausente ou for de outro tipo.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Valor float salvo. Retorna fallback se estiver ausente ou for de outro tipo.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Valor string salvo. Retorna fallback se estiver ausente ou for de outro tipo.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Grava um valor bool na memória.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Grava um valor int na memória.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Grava um valor long na memória.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Grava um valor float na memória.


#### SetString

```csharp
public void SetString(string key, string value)
```

Grava um valor string na memória. Não aceita null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Se há um valor salvo em key


#### Remove

```csharp
public bool Remove(string key)
```

Remove o valor salvo, de modo que leituras posteriores retornem fallback.


#### Reset

```csharp
public int Reset()
```

Limpa todas as opções deste mod. Retorna o número de opções removidas.


#### Save

```csharp
public bool Save()
```

Salva o arquivo de opções (DeviceOptions). Set*/Remove/Reset só alteram a memória, então chame isto para gravá-los no arquivo. Retorna false se não houver armazenamento ou se a gravação falhar.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Adiciona um interruptor à página deste mod na tela de opções. Substitui qualquer item com a mesma chave.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Adiciona uma caixa de seleção. O valor é um índice em choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Adiciona uma caixa de seleção cujas opções são fornecidas como chaves de localização.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Adiciona um controle deslizante. Se step for 0, o valor é contínuo.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Remove todos os itens deste mod da tela de opções. Os valores salvos permanecem.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

A página de um mod na tela de opções. Guarda apenas as definições de UI; os valores salvos são mantidos separadamente.

### Propriedades

#### Options

```csharp
public ModOptions Options { get; }
```

As opções de mod desta página


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Nome do mod exibido na tela de opções


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Itens na ordem de registro


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Definição comum de um item da tela de opções. Criada por `ModOptions.Add*`.

### Propriedades

#### Options

```csharp
public ModOptions Options { get; }
```

As opções de mod às quais este item pertence


#### Key

```csharp
public string Key { get; }
```

Chave de armazenamento


#### Label

```csharp
public ModOptionText Label { get; }
```

Nome de exibição. Se vazio, a chave é exibida.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Tipo de linha

### Construtores

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Valida key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Um item de opção que guarda um valor. Mantenha o handle para ler e alterar o valor pelo código.

### Eventos

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Chamado com o novo valor depois que o valor muda.

### Propriedades

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Valor padrão informado no registro


#### Value

```csharp
public T Value { get; set; }
```

Valor atual. Defini-lo armazena o valor na memória e dispara `ValueChanged` se ele tiver mudado.

### Construtores

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged se torna o primeiro inscrito de `ValueChanged`.

### Métodos

#### Read

```csharp
protected abstract T Read()
```

Lê o valor do armazenamento.


#### Write

```csharp
protected abstract void Write(T value)
```

Grava o valor na memória do armazenamento.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Limita o valor ao intervalo permitido antes de gravar.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Uma opção de ligar/desligar.

### Propriedades

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Sempre Toggle

### Métodos

#### Read

```csharp
protected override bool Read()
```

Lê o valor bool.


#### Write

```csharp
protected override void Write(bool value)
```

Grava o valor bool.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Uma opção de caixa de seleção. O valor é um índice em `Choices`.

### Propriedades

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Sempre SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Lista de opções

### Métodos

#### Read

```csharp
protected override int Read()
```

Índice salvo. O valor padrão se estiver fora do intervalo.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Limita ao intervalo das opções.


#### Write

```csharp
protected override void Write(int value)
```

Grava o índice.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Uma opção de controle deslizante.

### Propriedades

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Sempre Slider


#### Min

```csharp
public float Min { get; }
```

Valor mínimo


#### Max

```csharp
public float Max { get; }
```

Valor máximo


#### Step

```csharp
public float Step { get; }
```

Tamanho do passo. Contínuo se 0.

### Métodos

#### Read

```csharp
protected override float Read()
```

Valor salvo, normalizado


#### Normalize

```csharp
protected override float Normalize(float value)
```

Limita ao intervalo e ajusta ao passo.


#### Write

```csharp
protected override void Write(float value)
```

Grava o valor float.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Ainda sem descrição.*

### Métodos

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Exibe a string como está.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Traduz a chave da LocalizedString no momento da exibição.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Tipo de linha na tela de opções.

| Valor | Descrição |
|---|---|
| `Toggle` | Ligar/desligar |
| `SelectionBox` | Caixa de seleção |
| `Slider` | Controle deslizante |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Lista de páginas de mods que a tela de opções lê.

### Métodos

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Retorna as páginas a serem exibidas, ordenadas por modId.
