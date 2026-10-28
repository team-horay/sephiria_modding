# metadata.json

O arquivo `metadata.json` na raiz da pasta do seu mod é o arquivo central que descreve tanto **o que** é o seu mod quanto **como** ele é executado.


## Estrutura geral

```json
{
  "info": {
    "title": "New Artifact Pack",
    "description": "Adds 12 artifacts.",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ],
    "libraries": [
      "bin/Helper.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
    ]
  },
  "tags": ["Item"]
}
```

O nível superior define três coisas.

| Chave | Função | Obrigatório |
|---|---|---|
| `info` | Título, descrição e pré-visualização exibidos na Oficina Steam e no painel dentro do jogo | Obrigatório |
| `tags` | Tags da Oficina | Opcional |
| `mod` | Como o mod é executado | Obrigatório |

## info

### title

- O nome do mod. Se estiver vazio, o `modId` é usado como nome.
- Você pode usar um mapa de idiomas para definir o nome do mod em cada idioma.
	- Além do seu próprio idioma, recomendamos adicionar inglês.
- Códigos de idioma suportados: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- A descrição do mod.
- Assim como em `title`, você pode definir a descrição por idioma.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- Uma imagem de pré-visualização é obrigatória para enviar à Oficina. (PNG ou JPG, com menos de 1 MB)
- Informe o caminho relativo da imagem de pré-visualização.
- Se omitido, o jogo procura `thumbnail.png`, `preview.png`, `thumbnail.jpg` e `preview.jpg` na pasta raiz, nessa ordem.


## mod

### schemaVersion

- A versão atual é 1.
- Observe que esse número pode aumentar no futuro se houver mudanças grandes na forma como os mods são executados.

### modId

- Uma string única que identifica o seu mod. Use letras minúsculas do alfabeto inglês, dígitos, pontos, hifens e sublinhados. No máximo 64 caracteres.
- **Escolha um nome único.** Se o seu `modId` colidir com o de outro mod, ele não funcionará corretamente. Usar o seu nome de criador como prefixo ajuda bastante a evitar colisões.
(ex.: `horay.artifact-pack`)
- **Não mude o `modId` depois de publicar.** Outras pessoas podem criar mods filhos a partir do seu usando `dependencies`, e elas identificam o mod pai pelo `modId`.


### version

- Uma string que define a versão do seu mod.
- É o valor comparado quando outro mod exige uma versão exata por meio de `dependencies.version`.
- **Aumente a versão sempre que atualizar o mod.** Ao entrar em uma sala multijogador, o jogo verifica se o anfitrião e os participantes têm as mesmas versões dos mods. Se você mudar o conteúdo mas mantiver a mesma versão, jogadores com conteúdos de mod diferentes podem acabar na mesma sala, o que pode fazer a sincronização de conteúdo falhar ou travar o progresso do jogo.
- Recomendamos o mesmo formato 1.0.0 usado pelo jogo.

### author

Diga a todos quem é você!

### multiplayer / installation

Define se os jogadores de uma partida multijogador precisam compartilhar este mod. Valores permitidos:

| Valor | Descrição |
|---|---|
| `local_only` | Afeta apenas a sua própria tela e interface (mods de localização e mods simples de UI entram aqui) |
| `everyone` | Todos na sala precisam ter o mesmo mod ativado (qualquer mod que adicione ou altere conteúdo entra aqui) |
| `host_only` | Só o anfitrião precisa tê-lo ativado; funciona sem alterações do lado dos outros participantes (mods multijogador que afetam apenas cálculos do lado do servidor entram aqui) |

Se você declarar `installation` como `local_only` ou `host_only`, mas o seu mod mexer em conteúdo que os participantes também precisam (registro de novos itens ou unidades, objetos de rede, código que também roda nos clientes dos participantes), jogadores da mesma sala podem acabar jogando partidas diferentes, ou a sincronização de conteúdo pode falhar e travar o progresso normal do jogo. Verifique com cuidado o que o seu mod realmente faz.


### modules

- Os módulos definem o que o seu mod faz, o que torna esta **a seção mais importante**.
- Os módulos atuais são `localization, dll, costume, data`.
- Como no exemplo abaixo, você pode combinar vários módulos em um único pacote.
- Se ao menos um módulo for desconhecido, o pacote inteiro é marcado como "não suportado" e o mod é ignorado durante o carregamento.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: módulo localization

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- O módulo `localization` é para mods que incluem arquivos de idioma.
- Defina `path` como a pasta que contém os arquivos de idioma.
- Coloque **arquivos JSON nomeados com os códigos de idioma**, como `ko-KR.json` e `en-US.json`, dentro da pasta `path`. Consulte [Mods de localização](localization-mods.md) para ver os códigos de idioma e o formato de arquivo que o jogo usa.

#### type: módulo dll

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- O módulo `dll` permite se conectar ao jogo com código C#.
- Defina `path` como o caminho do arquivo DLL.
- Defina `entryClass` como o nome completo da classe de ponto de entrada na DLL, incluindo o namespace. O jogo não procura um ponto de entrada na DLL, então você **precisa especificá-lo**. (Para classes aninhadas, use `Ns.Outer+Inner`.)
- Não use o mesmo `entryClass` em dois módulos diferentes.
- Funciona apenas em builds Mono.
- Consulte [Mods em DLL](dll-mods.md) para mais detalhes.

#### type: módulo costume

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Permite criar seus próprios trajes sem escrever nenhum código C#.
- Defina `path` como a pasta que contém os arquivos do seu traje.
- No multijogador, todos precisam tê-lo instalado.
- Consulte [Mods de traje](costume-mods.md) para mais detalhes.


#### type: módulo data

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- Um módulo de dados de uso geral para mods. Não é usado em casos comuns; serve principalmente para dependências entre mods.
- `consumer` especifica qual mod vai usar este módulo de dados, mas normalmente você o omite. Se omitido, todos os mods listados em `dependencies` podem ler esses dados.
- Se você tem vários mods pais e os dados se destinam a apenas um deles, coloque o `modId` desse mod em `consumer`. Esse valor também precisa estar em `dependencies`.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- Uma lista de assemblies auxiliares referenciados pelo seu módulo `dll`.
- O jogo resolve referências usando **somente os arquivos declarados aqui**. Apenas colocar uma DLL na pasta não basta para que ela seja encontrada.
- As entradas podem ser uma string ou um objeto `{ "path": "..." }`.
- Cada entrada precisa terminar em `.dll`, e não funcionará corretamente se coincidir com uma DLL em `modules`.
- Bibliotecas não são tratadas como pontos de entrada. Mesmo que uma delas contenha uma classe `WorkshopModBase`, ela não será chamada.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- Se um mod pai é um mod de "criação de conteúdo", um mod filho pode adicionar conteúdo novo construído sobre ele.
- Nesse caso, você pode declarar uma dependência de outro mod usando `dependencies`.
- Quando um mod pai é especificado, o seu mod não pode ser carregado antes desse mod pai.
- Se um mod pai estiver ausente, desativado ou falhar ao carregar, o carregamento de todos os seus mods filhos é cancelado.
- Uma dependência pode ter os três campos a seguir.

| Campo | Obrigatório | Regra |
|---|---|---|
| `modId` | Sim | O `modId` do mod sobre o qual você constrói |
| `version` | Não | **Use apenas quando precisar de uma string de versão exatamente igual.** Se a versão for diferente, o carregamento é ignorado, então normalmente é mais seguro omitir. |
| `workshopId` | Não | Permite que os usuários encontrem o mod necessário e se inscrevam nele. |


## tags

- Categorias usadas ao enviar à Oficina.
- Um mod pode ter várias tags. (Para um mod tudo-em-um que adiciona itens e trajes, use `"tags": ["Item", "Costume"]`.)

| Categoria | Conteúdo |
|---|---|
| `Item` | Itens (artefatos, tábuas, poções etc.), combos |
| `Costume` | Trajes |
| `Weapon` | Armas |
| `NPC` | Viajantes, comerciantes etc. |
| `Enemy` | Novos inimigos, chefes etc. |
| `Gameplay` | Mudanças na jogabilidade |
| `QoL` | Melhorias de interface, conveniência e acessibilidade |
| `Library` | Runtimes, frameworks, pacotes de dados etc. dos quais outros mods dependem |
| `Localization` | Traduções |

## Regras de caminhos
 
- Os caminhos em `modules[].path`, `libraries` e `info.preview` seguem todos as mesmas regras.
- Somente caminhos relativos à pasta do mod são permitidos. (Caminhos absolutos, letras de unidade e `..` são rejeitados.)
- Use `/` como separador.
- Caminhos que passam por links simbólicos ou junções são rejeitados.
- **As maiúsculas e minúsculas precisam corresponder exatamente ao nome real.** No Windows, escrever `localization` ainda abre a pasta, mas o jogo rejeita por ser diferente de `Localization`. (Essa regra garante o mesmo comportamento no Steam Deck (Linux).)
