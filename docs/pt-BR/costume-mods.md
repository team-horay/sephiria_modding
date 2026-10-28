# Mods de traje

Vamos adicionar um traje usando apenas arquivos PNG e JSON, sem nenhum código. Esse tipo de mod pode adicionar as duas coisas a seguir:

- **Novos trajes**: crie um traje totalmente novo. Você pode definir a aparência, os atributos, os itens iniciais e muito mais.
- **Aparências para trajes existentes**: adicione uma aparência (skin) a um traje já existente, como o Coelhinho Rosa.

Esta página se baseia no exemplo [`examples/costume-sample`](../../examples/costume-sample). Baixe-o para ter uma noção da estrutura do mod.

## Estrutura do mod

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← pasta de trajes
│   └── WhiteCat/              ← uma pasta = um traje
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← PNGs dos outros movimentos
└── Localization/              ← textos de nome e descrição do traje
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

Ao criar o seu próprio mod a partir do exemplo, altere `modId` e `author` no `metadata.json`. (Consulte [`metadata.json`](metadata.md) para mais detalhes.)

```json
{
  "info": {
    "title": { "ko-KR": "하얀 고양이 코스튬", "en-US": "White Cat Costume" },
    "description": { "ko-KR": "하얀 고양이 코스튬을 추가합니다.", "en-US": "Adds the White Cat costume." }
  },
  "tags": ["Costume"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.costume-whitecat",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "costume", "path": "Costumes" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```


## Animações de sprite

- Um traje precisa de um bom número de animações se você incluir todas.
- Se este é o seu primeiro traje, tente fazer um simples, apenas com `idle, move, airborne, fall`.

| Movimento | Descrição | Obrigatório |
|---|---|---|
| `idle` | Parado | Obrigatório |
| `move` | Movendo-se | Obrigatório |
| `airborne` | No ar | Opcional |
| `fall` | Caído | Opcional |
| `attack` | Ataque | Opcional |
| `greatsword_lower` | Golpe giratório | Opcional |
| `greatsword_sweep` | Giro | Opcional |
| `hitfeedback` | Recebendo dano | Opcional |

### Animações de costas

- Para um traje com aparência mais natural, você pode desenhar sprites de costas. Isso é opcional.
- Se decidir adicionar sprites de costas, você precisa incluir os movimentos `idle_back, move_back`.
- As animações de costas são definidas da seguinte forma.

| Movimento | Descrição |
|---|---|
| `idle_back` | Parado (de costas) |
| `move_back` | Movendo-se (de costas) |
| `attack_back` | Ataque (de costas) |
| `greatsword_upper` | Golpe giratório (de costas) |

## Preparando a arte

- Para cada movimento, crie uma folha de sprites em PNG com os quadros daquele movimento dispostos na horizontal.
- A altura da imagem é igual à altura do quadro, e a largura é largura do quadro × número de quadros. Não deixe espaços entre os quadros.
- Recomendamos que todas as imagens individuais tenham o mesmo tamanho.
- No exemplo do Gato Branco, em que cada sprite tem 20×16, os tamanhos das imagens são os seguintes.

| Arquivo | Tamanho | Quadros |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Imagem de exemplo*


### Escrevendo as informações do novo traje

Defina o seu próprio traje escrevendo um arquivo `costume.json`.

Exemplo de `Costumes/WhiteCat/costume.json`:

```json
{
  "schemaVersion": 1,
  "id": "whitecat",
  "name": "Costume_WhiteCat_Name",
  "flavorText": "Costume_WhiteCat_FlavorText",
  "icon": "icon.png",

  "frameSize": [20, 16],
  "pivot": [10, 2],
  "pixelsPerUnit": 16,

  "animations": {
    "idle":             { "file": "idle.png", "fps": 12 },
    "idle_back":        { "file": "idle_back.png", "fps": 12 },
    "move":             { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "move_back":        { "file": "move_back.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":           { "file": "attack.png", "fps": 20, "loop": false },
    "attack_back":      { "file": "attack_back.png", "fps": 20, "loop": false },
    "greatsword_lower": { "file": "greatsword_lower.png", "fps": 20, "loop": false },
    "greatsword_upper": { "file": "greatsword_upper.png", "fps": 20, "loop": false },
    "greatsword_sweep": { "file": "greatsword_sweep.png", "fps": 30, "loop": false },
    "airborne":         { "file": "airborne.png", "fps": 12 },
    "fall":             { "file": "fall.png", "fps": 1 },
    "hitfeedback":      { "file": "hitfeedback.png", "fps": 20, "loop": false }
  },

  "stats": ["DEFENSE/-10", "EVASION/1200"],
  "startingItems": [],
  "onlyDefaultWeapon": false
}
```

| Campo | Obrigatório | Descrição |
|---|---|---|
| `schemaVersion` | Sim | Identificador do formato. Informe o inteiro `1`. |
| `id` | Sim | Use letras minúsculas do alfabeto inglês, dígitos, sublinhados e hifens, de 1 a 31 caracteres. Para evitar colisões, ele é registrado no jogo como `<modId>:<id>`. |
| `name` | Sim | A **chave de localização** do nome do traje. Consulte [Preparando os arquivos de localização](#preparando-os-arquivos-de-localização) abaixo. |
| `flavorText` | Não | A **chave de localização** da descrição do traje. Consulte [Preparando os arquivos de localização](#preparando-os-arquivos-de-localização) abaixo. |
| `icon` | Sim | Caminho do PNG do ícone de pré-visualização do traje. |
| `face` | Não | Caminho do PNG do rosto para o HUD do multijogador. |
| `pixelsPerUnit` | Não | Altere para usar uma arte mais detalhada. Valores maiores renderizam a arte com mais detalhes. O PPU padrão é 16. |
| `frameSize` | Não | A `[largura, altura]` de cada sprite. Padrão: `[16, 16]`. |
| `pivot` | Não | O ponto de pivô de cada sprite. O canto inferior esquerdo é `[0, 0]`. Padrão: `[largura/2, 0]`. |
| `animations` | Sim | Arte de cada movimento. Consulte [animations](#animations). |
| `stats` | Não | Lista de atributos a definir. Consulte a [Lista de atributos](stats.md). |
| `startingItems` | Não | Array com os IDs dos itens iniciais. Consulte a [Lista de itens](item-table.md). |
| `body` | Não | Define a caixa de colisão. Consulte [body](#body). |
| `skins` | Não | Aparências adicionais para o mesmo traje. Consulte [skins](#skins). |

### animations

```json
  "animations": {
    "idle":      { "file": "idle.png", "fps": 12 },
    "move":      { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":    { "file": "attack.png", "fps": 20, "loop": false },
    "fall":      { "file": "fall.png", "fps": 1 },
    "hitfeedback": { "file": "hitfeedback.png", "fps": 20, "loop": false, "frameSize": [24, 16], "pivot": [12, 2] }
  }
```

- Defina as animações que você preparou acima com quatro propriedades: `{ "file", "fps", "loop", "footstepFrames" }`.
- Você também pode definir `frameSize` e `pivot` separadamente para cada movimento.
- Se desenhou apenas um movimento em um tamanho diferente, como na última linha, defina `frameSize` e `pivot` nesse movimento.

#### Propriedades

| Campo | Obrigatório | Descrição |
|---|---|---|
| `file` | Sim | O nome do arquivo a ser reproduzido. |
| `fps` | Não | Velocidade de reprodução da animação. (Padrão: 12) |
| `loop` | Não | Se a animação se repete em loop. (Padrão: true; false para animações de ataque) |
| `footstepFrames` | Não | Números de quadro começando em zero. Um som de passos é reproduzido quando esses quadros são exibidos. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Define as áreas de colisão de movimento e de acerto do personagem.
- Os trajes padrão do jogo usam esses valores como padrão.
- A menos que você tenha um motivo específico, recomendamos omitir isso.

#### Propriedades

| Campo | Obrigatório | Descrição |
|---|---|---|
| `hitboxRadius` | Não | Raio do círculo da caixa de colisão, em pixels, de 0,5 a 64. Padrão `4`. |
| `hitboxOffset` | Não | Centro do círculo da caixa de colisão, `[x, y]`, em pixels relativos ao pivô; x positivo é para a direita e y positivo é para cima, ±64. Padrão `[0, 4]`. |
| `movementRadius` | Não | Raio do círculo de colisão de movimento que colide com paredes e obstáculos, em unidades (16 pixels = 1), de 0,05 a 2. Padrão `0.25`. |
| `bodyScale` | Não | Escala da posição do ombro onde a arma é segurada, de 0,1 a 4. Padrão `1`. Aumente quando a arma afundar no corpo de um traje grande. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Adiciona aparências extras ao mesmo traje.
- As animações seguem o mesmo formato acima.
- Se você omitir `frameSize` ou `pivot`, os valores do próprio traje são usados.




## Preparando os arquivos de localização

`Localization/ko-KR.json`:

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json`:

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- Em `name` e `flavorText`, escreva uma **chave de localização** em vez do texto, e coloque o texto na pasta `Localization/` do mesmo mod, um arquivo por idioma.
- Use letras do alfabeto inglês, dígitos, sublinhados e hifens nas chaves, sem espaços. Incluir o nome do seu mod ajuda a evitar colisões com outros mods.
- Se uma chave **não estiver em nenhum arquivo de idioma**, o traje não será carregado. Você precisa fornecer pelo menos um idioma.
- Recomendamos incluir também o inglês (`en-US`) para que mais pessoas possam aproveitar o seu traje.
- Não se esqueça de declarar também um módulo `localization` em `modules` no `metadata.json`.



## Teste

1. Coloque a pasta do seu mod em `WorkshopLocal/` e inicie o jogo.
2. Comece uma partida, abra a seleção de trajes na vila (**Trocar traje**) e verifique se o seu novo traje foi adicionado.
3. Se alterar o conteúdo do traje, você precisa reiniciar o jogo.



## Extra: adicionando apenas uma skin a um traje existente

Você pode criar uma aparência para um traje existente criando um `skin.json` em vez de `costume.json`.

```
Costumes/
└── PinkRabbit_Pajama/
    ├── skin.json
    ├── icon.png
    ├── idle.png
    └── move.png
```

```json
{
  "schemaVersion": 1,
  "costume": "PinkRabbit",
  "id": "pajama",
  "name": "Skin_PinkRabbit_Pajama_Name",
  "icon": "icon.png",
  "animations": { "idle": "idle.png", "move": "move.png" }
}
```

- `costume` é o id do traje de destino. Use o id de um traje padrão do jogo (como `PinkRabbit`) ou, para o traje de outro mod, o id de tempo de execução dele, incluindo o nome do mod. (No exemplo do Gato Branco, `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins` e `flavorText` vêm do traje existente como estão, então você não pode defini-los.
- Substituir a aparência padrão de um traje padrão do jogo não é suportado.
