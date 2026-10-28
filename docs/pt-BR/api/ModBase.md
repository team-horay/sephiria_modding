<!-- Arquivo gerado automaticamente. Tools/ModApiDocGen no repositório do jogo o constrói a partir dos comentários /// no código-fonte. Não o edite diretamente; corrija os comentários no código-fonte e gere novamente. -->
# Ponto de entrada e contexto

As classes base que a classe de entrada de um mod herda, além dos tipos de contexto e manifesto que o loader anexa. Mods novos devem herdar de `WorkshopModBase`.

Gerado em 2026-10-26 para a versão **1.1.0** do jogo. [Índice da referência](README.md)

| Tipo | Descrição |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | A base de entrada para mods da Oficina. |
| [`WorkshopModContext`](#workshopmodcontext) | Contexto somente leitura fornecido a um módulo DLL da Oficina. |
| [`HorayModBase`](#horaymodbase) | A base das classes de entrada de mods em DLL. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Um pacote de módulos de dados para um mod consumidor ler. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | Uma especificação de execução validada. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | Uma entrada do `modules[]` do manifesto. |
| [`WorkshopDependency`](#workshopdependency) | Uma entrada do `dependencies[]` do manifesto. |
| [`WorkshopModuleType`](#workshopmoduletype) | Um valor de `modules[].type` do manifesto. |
| [`WorkshopInstallation`](#workshopinstallation) | A declaração do autor sobre se os outros participantes precisam instalar o mod (`multiplayer.installation` do manifesto). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

A base de entrada para mods da Oficina.

### Propriedades

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Válido a partir do momento em que OnModLoaded é chamado.

### Métodos

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Pacotes de módulos de dados que indicam este mod como consumidor, na ordem de carregamento. Chame depois que a ativação terminar (depois dos eventos de banco de dados). Retorna uma lista vazia se não houver contexto (por exemplo, no caminho AddOns).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Contexto somente leitura fornecido a um módulo DLL da Oficina. O caminho de instalação pode variar, então os mods usam esses valores para encontrar seus próprios arquivos.

### Propriedades

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Caminho absoluto da raiz do pacote (pasta de instalação).


#### FileId

```csharp
public ulong FileId { get; }
```

O ID de arquivo da Oficina Steam. Pacotes locais recebem um ID único derivado do seu caminho.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

A especificação de execução validada.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

A entrada do módulo dll que criou esta instância.


#### Options

```csharp
public ModOptions Options { get; }
```

Opções por dispositivo para este modId. Compartilhadas pelos módulos DLL do pacote e pelas suas cópias local e inscrita.

### Construtores

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Ainda sem descrição.*

### Métodos

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Converte um caminho relativo à raiz do pacote em um caminho absoluto. Caminhos que escapam da raiz, ou caminhos absolutos, são rejeitados (lança exceção).


## HorayModBase

```csharp
public class HorayModBase
```

A base das classes de entrada de mods em DLL.

### Constantes e campos

#### metadata

```csharp
public Metadata metadata
```

Os metadados recebidos em `LoadMod`.

### Métodos

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Chamado pelo loader. Armazena os metadados e depois chama `OnModLoaded`.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

O ponto de inicialização do mod. É chamado antes que os bancos de dados sejam inicializados, então inscreva-se nos eventos da `HorayModAPI` aqui.


#### UnloadMod

```csharp
public void UnloadMod()
```

Chama `OnModUnloaded`.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

O ponto de descarregamento do mod. Coloque aqui a limpeza, como o cancelamento de inscrições em eventos.

### Tipos aninhados

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Informações de identificação do mod que o loader passa a `LoadMod`.

##### Constantes e campos

###### modName

```csharp
public string modName
```

Nome de exibição.


###### modVersion

```csharp
public string modVersion
```

Versão.


###### modAuthor

```csharp
public string modAuthor
```

Autor.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Um pacote de módulos de dados para um mod consumidor ler. Obtenha-o com `WorkshopModBase.GetDataPackages()`.

### Propriedades

#### ModId

```csharp
public string ModId { get; }
```

O modId do pacote.


#### Version

```csharp
public string Version { get; }
```

A versão do pacote.


#### FileId

```csharp
public ulong FileId { get; }
```

O ID de arquivo da Oficina Steam.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Nome de exibição (info.title ou, se ausente, modId).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Caminho absoluto da raiz do pacote.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Módulos de dados destinados a este consumidor, na ordem de declaração.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Caminhos absolutos das pastas dos módulos, na ordem de declaração. Igual a Modules[i].FullPath.

### Métodos

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Seleciona, a partir dos registros de ativação (na ordem de carregamento), os pacotes que têm módulos de dados para `consumerModId`. Inclui apenas os estados em que as dependências estão satisfeitas (Active, AlreadyActive, NoDllModules). Dados de pacotes que falharam ou foram aplicados parcialmente não são repassados.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

Uma especificação de execução validada. Imutável.

### Propriedades

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Caminho absoluto da raiz do pacote.


#### ModId

```csharp
public string ModId { get; }
```

O modId do manifesto.


#### Version

```csharp
public string Version { get; }
```

A versão do manifesto.


#### Author

```csharp
public string Author { get; }
```

O autor do manifesto. null se ausente.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title ou, se ausente, modId.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Requisito de instalação para multijogador.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Se o objeto multiplayer foi declarado explicitamente. Se false, Installation contém o padrão normalizado.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Lista de módulos, na ordem de declaração.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Caminhos absolutos das DLLs gerenciadas auxiliares. Elas não recebem callbacks de inicialização.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Lista de mods dos quais este mod depende.


#### HasDll

```csharp
public bool HasDll { get; }
```

Derivado de modules conter ou não um tipo dll. Não há uma flag separada.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Se modules contém um tipo localization.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Se modules contém um tipo costume.


#### HasData

```csharp
public bool HasData { get; }
```

Se modules contém um tipo data.

### Construtores

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Ainda sem descrição.*

### Métodos

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Módulos de dados que `consumerModId` recebe, na ordem de declaração. Lista vazia se não houver nenhum. Um módulo que indica um consumidor vai apenas para aquele mod; um módulo que o omite vai para este próprio pacote e para todos os mods que dependem diretamente dele.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

Uma entrada do `modules[]` do manifesto.

### Propriedades

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Tipo do módulo.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Caminho relativo à raiz do pacote (separadores normalizados para '/').


#### FullPath

```csharp
public string FullPath { get; }
```

Caminho absoluto validado.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Nome completo da classe de entrada do módulo dll. null para localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId do mod que lê o módulo de dados. Se omitido (null), é o próprio pacote mais todos os mods que dependem diretamente dele. Sempre null para módulos que não são de dados.

### Construtores

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Criado pelo validador.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Criado pelo validador.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

Uma entrada do `dependencies[]` do manifesto.

### Propriedades

#### ModId

```csharp
public string ModId { get; }
```

modId do mod do qual se depende.


#### Version

```csharp
public string Version { get; }
```

null significa qualquer versão. Se definido, apenas uma correspondência exata é aceita (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

Uma dica de onde baixar. Precisa ser verificado em relação ao modId/version reais depois da instalação; por si só, não prova que o mod baixado é o correto.

### Construtores

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Criado pelo validador.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Um valor de `modules[].type` do manifesto.

| Valor | Descrição |
|---|---|
| `Dll` | Módulo de DLL gerenciada |
| `Localization` | Módulo de pasta de localização |
| `Costume` | Pasta de pacote de traje sem código (costume.json/skin.json + PNG) |
| `Data` | Pasta de dados de uso geral lida por outro mod (o consumidor); o jogo não interpreta o seu conteúdo |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

A declaração do autor sobre se os outros participantes precisam instalar o mod (`multiplayer.installation` do manifesto).

| Valor | Descrição |
|---|---|
| `Unknown` | Não declarado (incluído na comparação de configuração da sala como Everyone) |
| `LocalOnly` | Funciona apenas no dispositivo do próprio usuário; excluído da comparação de configuração da sala |
| `Everyone` | Todos na sala precisam instalar o mesmo pacote |
| `HostOnly` | Só o anfitrião precisa instalá-lo |
