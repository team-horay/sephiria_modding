# Mods de trajes

Vamos a añadir un traje usando únicamente archivos PNG y JSON, sin código. Este tipo de mod puede añadir las dos cosas siguientes:

- **Trajes nuevos**: Crea un traje completamente nuevo. Puedes definir su apariencia, sus estadísticas, sus objetos iniciales y más.
- **Apariencias para trajes existentes**: Añade una apariencia (skin) a un traje existente, como Conejito Rosa.

Esta página se basa en el ejemplo [`examples/costume-sample`](../../examples/costume-sample). Descárgalo para hacerte una idea de la estructura del mod.

## Estructura del mod

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← carpeta de trajes
│   └── WhiteCat/              ← una carpeta = un traje
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← PNG de otros movimientos
└── Localization/              ← texto del nombre y la descripción del traje
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

Cuando crees tu propio mod a partir del ejemplo, cambia `modId` y `author` en `metadata.json`. (Consulta [`metadata.json`](metadata.md) para más detalles.)

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


## Animaciones de sprites

- Un traje necesita bastantes animaciones si las incluyes todas.
- Si es tu primer traje, prueba a hacer uno sencillo con solo `idle, move, airborne, fall`.

| Movimiento | Descripción | Obligatorio |
|---|---|---|
| `idle` | En reposo | Obligatorio |
| `move` | Moviéndose | Obligatorio |
| `airborne` | En el aire | Opcional |
| `fall` | Desplomándose | Opcional |
| `attack` | Ataque | Opcional |
| `greatsword_lower` | Golpe giratorio | Opcional |
| `greatsword_sweep` | Giro | Opcional |
| `hitfeedback` | Recibiendo un golpe | Opcional |

### Animaciones de espaldas

- Para que el traje se vea más natural, puedes dibujar sprites de espaldas. Es opcional.
- Si decides añadir sprites de espaldas, debes incluir los movimientos `idle_back, move_back`.
- Las animaciones de espaldas se definen así.

| Movimiento | Descripción |
|---|---|
| `idle_back` | En reposo (de espaldas) |
| `move_back` | Moviéndose (de espaldas) |
| `attack_back` | Ataque (de espaldas) |
| `greatsword_upper` | Golpe giratorio (de espaldas) |

## Preparar el arte

- Para cada movimiento, crea una hoja de sprites PNG con los fotogramas de ese movimiento dispuestos en horizontal.
- La altura de la imagen es igual a la altura del fotograma, y la anchura es anchura del fotograma × número de fotogramas. No dejes huecos entre fotogramas.
- Te recomendamos hacer todas las imágenes individuales del mismo tamaño.
- En el ejemplo del Gato Blanco, donde cada sprite es de 20×16, los tamaños de imagen son los siguientes.

| Archivo | Tamaño | Fotogramas |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Imagen de ejemplo*


### Escribir la información del traje nuevo

Define tu propio traje escribiendo un archivo `costume.json`.

Ejemplo de `Costumes/WhiteCat/costume.json`:

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

| Campo | Obligatorio | Descripción |
|---|---|---|
| `schemaVersion` | Sí | Identificador del formato. Introduce el entero `1`. |
| `id` | Sí | Usa letras minúsculas del alfabeto inglés, dígitos, guiones bajos y guiones, de 1 a 31 caracteres. Para evitar colisiones, se registra en el juego como `<modId>:<id>`. |
| `name` | Sí | La **clave de localización** del nombre del traje. Consulta [Preparar los archivos de localización](#preparar-los-archivos-de-localización) más abajo. |
| `flavorText` | No | La **clave de localización** de la descripción del traje. Consulta [Preparar los archivos de localización](#preparar-los-archivos-de-localización) más abajo. |
| `icon` | Sí | Ruta al PNG del icono de vista previa del traje. |
| `face` | No | Ruta al PNG de la cara para el HUD de multijugador. |
| `pixelsPerUnit` | No | Cámbialo para usar arte más detallado. Los valores más altos renderizan el arte con más detalle. El PPU por defecto es 16. |
| `frameSize` | No | El `[ancho, alto]` de cada sprite. Por defecto: `[16, 16]`. |
| `pivot` | No | El punto de pivote de cada sprite. La esquina inferior izquierda es `[0, 0]`. Por defecto: `[ancho/2, 0]`. |
| `animations` | Sí | Arte para cada movimiento. Consulta [animations](#animations). |
| `stats` | No | Lista de estadísticas que se van a fijar. Consulta la [Lista de estadísticas](stats.md). |
| `startingItems` | No | Array de ID de objetos iniciales. Consulta la [Lista de objetos](item-table.md). |
| `body` | No | Define la hitbox. Consulta [body](#body). |
| `skins` | No | Apariencias adicionales para el mismo traje. Consulta [skins](#skins). |

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

- Define las animaciones que preparaste arriba con cuatro propiedades: `{ "file", "fps", "loop", "footstepFrames" }`.
- También puedes definir `frameSize` y `pivot` por separado para cada movimiento.
- Si dibujaste un solo movimiento con un tamaño distinto, como en la última línea, define `frameSize` y `pivot` en ese movimiento.

#### Propiedades

| Campo | Obligatorio | Descripción |
|---|---|---|
| `file` | Sí | El nombre del archivo que se reproducirá. |
| `fps` | No | Velocidad de reproducción de la animación. (Por defecto: 12) |
| `loop` | No | Si la animación se repite en bucle. (Por defecto: true; false para las animaciones de ataque) |
| `footstepFrames` | No | Números de fotograma empezando desde cero. Se reproduce un sonido de pasos cuando se muestran esos fotogramas. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Define las áreas de colisión de movimiento y de impacto del personaje.
- Los trajes integrados usan estos valores por defecto.
- A menos que tengas un motivo concreto, te recomendamos omitirlo.

#### Propiedades

| Campo | Obligatorio | Descripción |
|---|---|---|
| `hitboxRadius` | No | Radio del círculo de la hitbox, en píxeles, de 0,5 a 64. Por defecto `4`. |
| `hitboxOffset` | No | Centro del círculo de la hitbox, `[x, y]`, en píxeles relativos al pivote; x positivo es derecha e y positivo es arriba, ±64. Por defecto `[0, 4]`. |
| `movementRadius` | No | Radio del círculo de colisión de movimiento que choca con paredes y obstáculos, en unidades (16 píxeles = 1), de 0,05 a 2. Por defecto `0.25`. |
| `bodyScale` | No | Escala de la posición del hombro donde se sostiene el arma, de 0,1 a 4. Por defecto `1`. Súbelo cuando el arma se hunda en el cuerpo de un traje grande. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Añade apariencias adicionales al mismo traje.
- Las animaciones siguen el mismo formato que arriba.
- Si omites `frameSize` o `pivot`, se usan los valores del propio traje.




## Preparar los archivos de localización

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

- En `name` y `flavorText`, escribe una **clave de localización** en lugar de texto, y pon el texto en la carpeta `Localization/` del mismo mod, un archivo por idioma.
- Usa letras del alfabeto inglés, dígitos, guiones bajos y guiones para las claves, sin espacios. Incluir el nombre de tu mod ayuda a evitar colisiones con otros mods.
- Si una clave **no está en ningún archivo de idioma**, el traje no se cargará. Debes proporcionar al menos un idioma.
- Te recomendamos incluir también el inglés (`en-US`) para que más gente pueda disfrutar de tu traje.
- No olvides declarar también un módulo `localization` en `modules` dentro de `metadata.json`.



## Comprobarlo

1. Pon la carpeta de tu mod en `WorkshopLocal/` e inicia el juego.
2. Empieza una partida, abre la selección de trajes en el pueblo (**Cambiar atuendo**) y comprueba que tu nuevo traje se ha añadido.
3. Si cambias el contenido del traje, tienes que reiniciar el juego.



## Extra: añadir solo una skin a un traje existente

Puedes crear una apariencia para un traje existente creando `skin.json` en lugar de `costume.json`.

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

- `costume` es el id del traje de destino. Usa el id de un traje integrado (como `PinkRabbit`) o, para el traje de otro mod, su id en tiempo de ejecución incluyendo el nombre del mod. (Para el ejemplo del Gato Blanco, `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins` y `flavorText` se toman tal cual del traje existente, así que no puedes definirlos.
- No se admite sobrescribir la apariencia por defecto de un traje integrado.
