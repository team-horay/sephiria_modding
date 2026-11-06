# Mods em DLL

Os módulos DLL permitem executar código escrito em C# dentro do jogo. Inscreva-se nos eventos da `HorayModAPI` para se conectar a jogadores, itens, interface e muito mais.
Este documento cobre tudo, desde a criação do projeto até a execução da sua primeira DLL no jogo. Para a lista completa de eventos e funções de registro, consulte a [Referência da API](api/README.md).


## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) 8 ou superior
- Um editor de C#


## Criando um projeto

Consulte o exemplo [`examples/dll-sample`](../../examples/dll-sample). Este documento o usa como referência.

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
    <!-- Pasta de instalação do jogo. Altere conforme a sua configuração ou passe com dotnet build -p:GameDir=... -->
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
    <!-- Adicione Mirror, da mesma forma, se for trabalhar com objetos de rede como PlayerAvatar; UnityEngine.UI se for criar UI; e 0Harmony se for aplicar patches -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Use `netstandard2.1` como alvo** e defina a versão da linguagem como C# 9.
2. **Referencie as DLLs do jogo, mas não as copie.** Aponte as referências para os arquivos em `<pasta de instalação do jogo>/Sephiria_Data/Managed/` e defina todas como `Private=false`. Se as DLLs do jogo forem copiadas para a pasta do seu mod, o mesmo assembly é carregado duas vezes, o que causa erros obscuros, além de adicionar arquivos desnecessários à revisão.
3. `Assembly-CSharp.dll` contém a maior parte do código que executa o jogo.


## Estrutura do código


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Chamado uma vez por processo. Inscreva-se nos eventos aqui, e apenas uma vez.
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

        // Chamado apenas no anfitrião (servidor). Altere o estado do jogo aqui.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Chamado em todos os participantes. O valor adicionado pelo servidor chega pela sincronização.
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

Sua classe herda o seguinte da classe base.

| Membro | Descrição |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Preenchidos com os valores do `metadata.json`. |
| `Context` | `PackageRoot` (caminho absoluto da pasta do mod), `FileId` (ID do item na Oficina Steam; para pacotes locais, um valor separado que nunca colide com um ID da Steam), `Definition` (o manifesto validado), `ResolvePath(relativePath)`. Use-o para encontrar arquivos dentro da pasta do seu mod. O caminho de instalação é diferente para cada usuário, então nunca o escreva fixo no código. |
| `GetDataPackages()` | A lista de pacotes de módulos `data` que indicam este mod como `consumer`. Chame-o depois que a ativação estiver concluída (dentro de um handler `OnLoad*Database` ou depois de `OnAllDatabasesReady`). |

`Context` é válido a partir do momento em que `OnModLoaded` é chamado. No construtor, ele ainda é null.


## Ciclo de vida

Os mods em DLL são ativados **quando você inicia uma partida e entra no mundo**. Nenhum código roda ainda na tela de título. A ordem de inicialização é a seguinte.

1. A cena do mundo é carregada e o `GameDataLoader` começa.
2. Os módulos DLL do plano de carregamento são carregados em ordem, cada classe de entrada é instanciada e `OnModLoaded` é chamado.
3. Conforme os bancos de dados são inicializados, eventos como `OnLoadItemDatabase` e `OnLoadUnitDatabase` são disparados um após o outro.
4. Quando tudo termina, `OnAllDatabasesReady` é chamado.

## Inscrevendo-se em eventos

A classe `HorayModAPI` oferece eventos predefinidos que permitem se conectar a muitas situações.

- Inscreva-se nos eventos **apenas uma vez**, em `OnModLoaded`. Os eventos de banco de dados disparam novamente a cada carregamento, então faça as coisas que "precisam acontecer toda vez" (registrar itens etc.) dentro dos handlers.
- Gerencie as inscrições e os cancelamentos com cuidado. Cancele a inscrição (-=) dos eventos nos quais você se inscreveu em `OnModUnloaded`.

A lista completa de eventos está na [Referência da API](api/README.md). A tabela abaixo lista o evento recomendado para cada coisa que você pode querer fazer.

| O que você quer fazer | Evento |
|---|---|
| Adicionar itens ou artefatos | `OnLoadItemDatabase` |
| Adicionar armas, milagres, passivas, trajes ou props | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Adicionar unidades inimigas ou NPCs | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Adicionar viajantes (NPCs da vila) | `OnLoadSocialIDDatabase` |
| Adicionar uma exibição de atributo | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Registrar textos localizados pelo código | `OnLocalizationReady` (`AddText`) |
| Adicionar um botão ao menu de título | `OnTitleMenuReady` |
| Início do jogo ou da sessão | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| Quando um andar é gerado | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Surgimento de jogadores ou unidades | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Interceptar verificações de combate | `OnPlayerEvade`, `OnGetDebuff` |
| Permissões de manipulação do inventário | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Evitando conflitos

As entidades que o seu mod adiciona são nomeadas no formato `<modId>:<id>`, por exemplo `yourname.artifact-pack:flame-ring`. Essa regra evita conflitos com outros mods e é como o conteúdo de mods é diferenciado no histórico de partidas e nas estatísticas.


## Lado do servidor, lado do cliente

- Sephiria foi construído em torno do multijogador, então muitos eventos vêm em versões separadas para o lado do servidor e o lado do cliente.
- O anfitrião dispara ambas; os outros participantes disparam apenas a versão Clientside.
- Como regra, faça as coisas que alteram o estado do jogo (surgimento, drops, dano) apenas em Serverside, e as coisas que só mudam o que é exibido em Clientside.


## Patches com Harmony

Quando precisar alterar algo que os eventos não alcançam, você pode usar o Harmony.
Desde o patch 1.1.0, o **Harmony 2.4.3 vem embutido** no jogo, então basta adicionar uma referência a ele.

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

- **Não inclua o `0Harmony.dll` junto com o seu mod.** Uma cópia incluída é ignorada, e mesmo que você inclua uma versão diferente, o seu mod não rodará nessa versão.
- Usar o seu `modId` como está para o ID do Harmony evita colisões com outros.
- Patches quebram com atualizações do jogo com muito mais facilidade do que a API oficial. Faça o que puder com eventos e deixe os patches como último recurso.
- Loaders externos como BepInEx e MelonLoader não são suportados. Um mod que funciona como um loader externo (ou seja, que encontra e executa DLLs de plugins de fora da Oficina) recebe um aviso e só é ativado depois que o usuário o aceita. Veja os detalhes em [Mods que executam código de fora da Oficina](#mods-que-executam-código-de-fora-da-oficina).


## DLLs auxiliares

Se o seu mod usa outras bibliotecas (por exemplo, pacotes de terceiros além do Newtonsoft.Json), coloque as DLLs delas na pasta do seu mod e declare-as em `libraries` no `metadata.json`. DLLs não declaradas não são usadas na resolução de referências.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Não inclua DLLs que já vêm com o jogo (Unity, Mirror, Newtonsoft.Json, 0Harmony etc.). A cópia do jogo é carregada primeiro, então a cópia incluída no seu mod é ignorada ou entra em conflito.
- DLLs nativas (C/C++) não podem ser usadas. Apenas assemblies gerenciados são carregados.
- Se um assembly com o mesmo nome já tiver sido carregado por outro mod, no caso de uma DLL em `modules` o jogo não escolhe um dos dois; ele **suspende a execução** dele. No caso de uma DLL em `libraries`, o jogo usa a que já foi carregada e ignora a sua cópia, o que pode causar problemas se as versões forem diferentes. É mais seguro separar bibliotecas compartilhadas em um mod à parte e vinculá-lo por meio de `dependencies`.

### Usando a biblioteca de outro mod

Se vários mods usam o mesmo código, coloque esse código em um mod pai e faça os mods filhos se vincularem a ele por meio de `dependencies`.

1. O mod pai declara o código compartilhado em `modules` ou `libraries` e adiciona `Library` aos seus `tags`.
2. O mod filho informa o `modId` do mod pai em `dependencies` e é compilado referenciando a DLL do mod pai com `Private=false`. **Não copie a DLL do mod pai para dentro do mod filho.**
3. O jogo carrega o mod pai primeiro, então as referências do mod filho são resolvidas para o assembly do mod pai que já foi carregado.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- O mod filho ainda precisa ter seu próprio módulo `dll` e sua `entryClass`. Não há suporte para uma estrutura em que o mod pai encontra e carrega por conta própria as DLLs do mod filho. Em vez disso, faça o mod filho chamar a função de registro do mod pai no próprio `OnModLoaded`.
- Se o mod pai alterar seus tipos ou membros públicos em uma atualização, os mods filhos podem quebrar. Evite remover ou alterar as APIs públicas existentes de uma biblioteca compartilhada.

### Não crie DLLs em tempo de execução!

Todo o código que um mod executa precisa estar dentro de um item da Oficina que passou pela revisão. Se os arquivos DLL de um mod mudarem enquanto o mod está em execução, o carregamento desse mod é restringido. Inclua todas as DLLs no item da Oficina como estão, sem nenhuma compressão ou streaming. Ou seja, o seguinte pode violar o sistema de revisão. Coloque no mod todas as DLLs de que você precisa ao enviá-lo.

- O mod baixa uma DLL de fora durante a execução e carrega o assembly
- O mod cria e executa a própria DLL durante a execução (inclusive reescrever outra DLL com Mono.Cecil ou similar e depois executá-la)
- O mod extrai e usa uma DLL empacotada dentro dele com um esquema de compressão próprio

### Mods que executam código de fora da Oficina

Um mod que encontra e executa DLLs que o usuário coloca **fora da pasta do mod**, como na pasta de instalação do jogo ou em `AppData` (por exemplo, um mod de compatibilidade com plugins de outro loader), não é rejeitado, mas o sistema de revisão adiciona um **aviso** a ele.

- Quando o usuário ativa o mod, aparece um aviso, e o mod só é ativado se ele aceitar. Quando o mod é atualizado, a pergunta é feita de novo.
- As DLLs externas executadas dessa forma não passam por revisão, e os problemas causados por elas não têm suporte.
- Um mod que baixa essas DLLs por conta própria durante a execução se enquadra nos casos proibidos acima.

Para usar o código de outro mod, não carregue DLLs diretamente da pasta desse mod; vincule-o por meio de `dependencies`, como descrito em [Usando a biblioteca de outro mod](#usando-a-biblioteca-de-outro-mod). DLLs que o jogo não carregou por conta própria pulam a ordem de carregamento, a verificação de conflito de nomes e a de dependências, então não têm suporte.

Se você precisar reescrever uma DLL (por exemplo, converter IL para outra API do Harmony), faça isso antes de enviar e inclua a DLL convertida no seu mod.


## Compilando e testando

```bash
dotnet build -c Release
```

- Quando a compilação for bem-sucedida e a DLL aparecer em `WorkshopLocal/<mod>/bin/`, inicie o jogo, verifique se o seu mod aparece em "Meus mods" no painel da Oficina da Steam na tela de título e então comece uma partida.
- Se você mantiver o `.pdb` junto com a saída da compilação, os stack traces no log incluem números de linha. Deixe-o de fora ao publicar.


## Atualizações do jogo e compatibilidade

Quando o jogo é atualizado, o `Assembly-CSharp.dll` muda. Mods que usam apenas os eventos oficiais normalmente continuam funcionando, mas mods que mexem diretamente em classes internas ou aplicam patches com Harmony podem quebrar.

- Depois de uma atualização, recompile usando a nova pasta `Managed/` e teste.
- Se você anotar a **versão do jogo suportada** na descrição da sua página na Oficina, os usuários conseguem encontrar a causa rapidamente. A versão do jogo também pode ser registrada automaticamente nos metadados do item da Oficina quando você publica.
- Quando alterar o seu código, ele precisa ser enviado ao sistema de revisão novamente. Consulte [Publicando na Oficina](publishing.md#revisão-de-código).


## Considerações sobre multijogador

- A maioria dos mods com código terá `multiplayer.installation` definido como `everyone`.
- Se o seu mod altera o estado do jogo apenas em eventos `Serverside`, e os valores que ele altera são estados do jogo que já são sincronizados (por exemplo, `customStats`), de modo que os participantes não precisam de código nem de registros, você pode declará-lo como `host_only`. Nesse caso, os participantes não baixam o mod. Um mod que registra novos itens, usa `RegisterNetworkPrefab` ou altera qualquer coisa em eventos `Clientside` não pode ser `host_only`.
- Objetos criados pela rede (unidades, projéteis etc.) precisam ser registrados com `RegisterNetworkPrefab` para serem criados também do lado dos participantes. Se você passar um `stableKey`, o mesmo ID é mantido mesmo quando a versão do seu mod muda.
- O anfitrião e os participantes precisam ter até a mesma **ordem de carregamento** dos mods. Evite código cujos resultados dependam dessa ordem.


## Sincronização de rede (SyncVar, RPC, Command)

O multijogador de Sephiria é implementado com a biblioteca de rede de código aberto "Mirror". No Mirror, você pode usar a comunicação com facilidade declarando os atributos a seguir.

- `[SyncVar]`: atributo colocado em uma variável membro de uma classe. Sincroniza o valor da variável entre o servidor e os clientes.
- `[ClientRpc]`, `[TargetRpc]`: atributos colocados em uma função. Pedem aos clientes que executem a função a partir do servidor.
- `[Command]`: atributo colocado em uma função. Pede ao servidor que execute a função a partir de um cliente.

Em um ambiente de mods, esses atributos não podem ser usados normalmente. Eles só funcionam se o Weaver do Mirror inserir, na compilação, o código de transmissão correspondente a cada atributo, mas uma DLL gerada externamente não passa pelo Weaver, então, mesmo que a compilação seja bem-sucedida, esses atributos não fazem nada. Esse tipo de bug parece normal na tela do anfitrião e só causa problema na tela dos participantes, por isso é difícil de descobrir testando sozinho. (Mesmo que você forçasse a passagem pelo Weaver, ainda poderia haver bugs como colisão de hash de funções.)


### `SyncVar<T>`, `ModRpc`

Para resolver isso, Sephiria oferece código de comunicação para mods que substitui esses atributos: `SyncVar<T>` e `ModRpc`.

| O que você quer fazer | O que usar |
|---|---|
| Manter um valor sempre sincronizado (inclusive para um participante que entra tarde) | `SyncVar<T>` |
| Chamada do servidor para todos os clientes | `ModRpc.Send` |
| Chamada do servidor para um cliente específico | `ModRpc.SendTo` |
| Chamada de um cliente para o servidor | `ModRpc.SendCommand` |
| Alterar um valor que o jogo já sincroniza | Propriedade `Network<FieldName>` |

- Este recurso está disponível a partir da versão 1.1.2 do jogo.
- Mods que usam este recurso devem declarar `multiplayer.installation` como `everyone`.


### Preparando o componente

`SyncVar<T>` e `ModRpc` só podem ser usados em um componente que herda de `NetworkBehaviour`. Esse componente deve estar anexado ao mesmo objeto, **na mesma ordem**, tanto no anfitrião quanto nos participantes. Se você o anexar com `AddComponent` a um objeto que já surgiu, ele não entra na sincronização, portanto ele já deve estar anexado antes de registrar o objeto como prefab de rede.

```csharp
private static GameObject prefab;

// Inscreva-se em HorayModAPI.OnAllDatabasesReady a partir de OnModLoaded.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Coloque-o sob um pai inativo. O Awake do prefab não é executado, e as cópias criadas com Instantiate começam ativas.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Faça o objeto surgir a partir do servidor. Se você passar o segundo argumento, esse jogador se torna o proprietário.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: sincronizando valores

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Como não há Weaver, registre-o você mesmo no construtor. A ordem dos campos deve ser sempre a mesma.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Altere o valor **no servidor**, por exemplo com `stack.Value = 3`. Um valor alterado por um participante não é transmitido ao servidor.
- Um participante que entra tarde também recebe o valor atual ao entrar.
- `Callback` é chamado no anfitrião quando um valor é atribuído e nos participantes quando um valor é recebido. Ele também é chamado se o valor recebido na entrada for diferente do valor padrão. Uma exceção no callback fica registrada no log e a sincronização continua.
- Você pode usar tipos básicos como `int`, `float`, `string` e `Vector3`. Para um struct criado por um mod, o código de serialização não é gerado automaticamente, então envie os campos separadamente.
- Para referenciar outro objeto, em vez de `SyncVar<GameObject>`, envie o `netId` dele como `SyncVar<uint>`. Pode ser que esse objeto ainda não tenha sido criado do lado que recebe.
- O número de SyncVars que podem ser registrados em um componente é 64, contando os herdados.

### `ModRpc`: chamando funções

Registre a função uma vez quando o mod for carregado. O nome usado na chamada é `modId:functionName`.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Servidor → todos os clientes que veem este objeto
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Servidor → um cliente. Se você passar null como conexão, é enviado ao proprietário do objeto.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Cliente → servidor
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Leia na ordem em que escreveu. Se a ordem ou os tipos de escrita e leitura forem diferentes, os valores ficam errados ou ocorre uma exceção.
- Por padrão, `RegisterCommand` só pode ser chamado pelo **proprietário do objeto**. Para recebê-lo em um objeto sem proprietário, passe `requiresAuthority: false`. Nesse caso, qualquer participante pode chamá-lo. Cuidado com trapaças e afins.
- Se você enviar em uma direção diferente da registrada, ou com um nome não registrado, só fica um log de erro e nada é transmitido.
- Se a função não existir no lado que recebe ou o tipo do componente for diferente, só fica um log de aviso e a chamada é ignorada. Uma exceção no handler também fica registrada no log e o jogo continua.
- O canal padrão é o transporte confiável. Para efeitos enviados com frequência, você pode passar o argumento `channelId: Channels.Unreliable`.

### O que você precisa cumprir

- Um cliente pode enviar valores arbitrários por meio de um Command. Valide no handler do servidor o número do slot, a quantidade, o alvo etc.
- Um participante que entra tarde não recebe os RPCs anteriores à sua entrada. Envie com `SyncVar<T>` o estado que precisa ser mantido, e use RPCs para coisas passageiras como sons e efeitos.
- **Um RPC pode chegar antes do SyncVar.** Se você ler `stack.Value` no handler de um RPC, pode obter o valor anterior. Se precisar do valor novo, envie-o também como argumento do RPC ou trate-o em `Callback`.

### Alterando valores do código base de Sephiria

Se você atribuir diretamente a um campo `[SyncVar]` de uma classe do jogo, o valor não é transmitido aos participantes. Use a propriedade com o prefixo `Network`. As letras maiúsculas e minúsculas são as do nome do campo, exatamente.

```csharp
unit.maxHp = 500;         // Só o valor do anfitrião muda.
unit.NetworkmaxHp = 500;  // Também é transmitido aos participantes.
```

Antes de o objeto surgir (no prefab ou logo após `Instantiate`), mesmo uma atribuição direta é transmitida junto com o valor quando ele surge.

### Testes

O anfitrião sozinho não revela os problemas acima. Antes de publicar, verifique com dois PCs as telas do anfitrião e do participante, e teste também o caso de entrar tarde depois de alterar um valor. Com o pacote local (`WorkshopLocal`) ativado, ninguém mais consegue entrar na sala, então os testes multijogador são feitos inscrevendo-se em um item que você enviou à Oficina como Unlisted. Um item enviado pela primeira vez é automaticamente Unlisted.


## Solução de problemas

| Sintoma | O que verificar |
|---|---|
| O resumo de status diz que a classe de entrada não foi encontrada | O namespace ou o nome da classe em `entryClass` não corresponde ao seu código. Classes aninhadas são escritas como `Outer+Inner`. |
| O resumo de status mostra um erro de tipo da classe de entrada | A classe de entrada não herda de `WorkshopModBase` (ou `HorayModBase`), não é pública, é abstrata ou não tem um construtor público sem parâmetros. |
| O resumo de status diz que o mod foi suspenso por conflito de nome de assembly | Outro mod (incluindo AddOns) usa o mesmo `AssemblyName`. Renomeie o seu. |
| O resumo de status mostra uma falha ao carregar biblioteca | Uma DLL listada em `libraries` está faltando ou não é um assembly gerenciado. |
| `FileNotFoundException` no log | Uma DLL referenciada não está na pasta do seu mod ou não foi declarada em `libraries`. Ou uma DLL do jogo foi copiada com `Private=true` e as duas entram em conflito. |
| Exceção logo depois de entrar no mundo | Uma exceção em um handler de evento de banco de dados. Procure o namespace do seu mod no stack trace do log. |
| Os efeitos se acumulam depois de voltar ao título e entrar de novo | Você está se inscrevendo novamente dentro de um handler. Inscreva-se apenas uma vez, em `OnModLoaded`. |


## Exemplo

[`examples/dll-sample`](../../examples/dll-sample) é um mod mínimo que aumenta o Dano físico em 5 quando o jogador surge. Sua pasta `package/` inclui uma DLL pré-compilada, então copiar essa pasta para `WorkshopLocal/` faz com que funcione imediatamente, sem compilar. Para transformá-lo no seu próprio mod, você só precisa alterar `AssemblyName`, `modId`, `entryClass` e o caminho de saída. Comece uma partida e abra a janela de atributos para vê-lo funcionando como Dano físico +5.
