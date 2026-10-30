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
- Loaders externos como BepInEx e MelonLoader não são suportados.


## DLLs auxiliares

Se o seu mod usa outras bibliotecas (por exemplo, pacotes de terceiros além do Newtonsoft.Json), coloque as DLLs delas na pasta do seu mod e declare-as em `libraries` no `metadata.json`. DLLs não declaradas não são usadas na resolução de referências.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Não inclua DLLs que já vêm com o jogo (Unity, Mirror, Newtonsoft.Json, 0Harmony etc.). A cópia do jogo é carregada primeiro, então a cópia incluída no seu mod é ignorada ou entra em conflito.
- DLLs nativas (C/C++) não podem ser usadas. Apenas assemblies gerenciados são carregados.
- Se um assembly com o mesmo nome já tiver sido carregado por outro mod, o jogo não escolhe um dos dois; ele **suspende a execução** dele. É mais seguro separar bibliotecas compartilhadas em um mod à parte e vinculá-lo por meio de `dependencies`.

### Não crie DLLs em tempo de execução!

Se os arquivos DLL de um mod mudarem enquanto o mod está em execução, o carregamento desse mod é restringido. Inclua todas as DLLs no item da Oficina como estão, sem nenhuma compressão ou streaming. Ou seja, o seguinte pode violar o sistema de revisão. Coloque no mod todas as DLLs de que você precisa ao enviá-lo.

- O mod baixa uma DLL de fora durante a execução e carrega o assembly
- O mod cria e executa a própria DLL durante a execução
- O mod extrai e usa uma DLL empacotada dentro dele com um esquema de compressão próprio


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
