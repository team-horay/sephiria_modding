# metadata.json

El archivo `metadata.json` de la raíz de la carpeta de tu mod es el archivo principal que describe **qué** es tu mod y **cómo** se ejecuta.


## Estructura general

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

El nivel superior define tres cosas.

| Clave | Función | Obligatorio |
|---|---|---|
| `info` | Título, descripción y vista previa que se muestran en el Workshop de Steam y en el panel del juego | Obligatorio |
| `tags` | Etiquetas del Workshop | Opcional |
| `mod` | Cómo se ejecuta el mod | Obligatorio |

## info

### title

- El nombre del mod. Si está vacío, se usa `modId` como nombre.
- Puedes usar un mapa de idiomas para definir el nombre del mod en cada idioma.
	- Además de tu propio idioma, te recomendamos añadir el inglés.
- Códigos de idioma admitidos: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- La descripción del mod.
- Igual que `title`, puedes definir la descripción por idioma.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- Para subir el mod al Workshop es necesaria una imagen de vista previa. (PNG o JPG, de menos de 1 MB)
- Introduce la ruta relativa a la imagen de vista previa.
- Si se omite, el juego busca `thumbnail.png`, `preview.png`, `thumbnail.jpg` y `preview.jpg` en la carpeta raíz, en ese orden.


## mod

### schemaVersion

- La versión actual es 1.
- Ten en cuenta que este número puede aumentar en el futuro si hay cambios importantes en la forma en que se ejecutan los mods.

### modId

- Una cadena única que identifica tu mod. Usa letras minúsculas del alfabeto inglés, dígitos, puntos, guiones y guiones bajos. Hasta 64 caracteres.
- **Elige un nombre único.** Si tu `modId` coincide con el de otro mod, no funcionará correctamente. Ponerle como prefijo tu nombre de creador ayuda mucho a evitar colisiones.
(p. ej. `horay.artifact-pack`)
- **No cambies tu `modId` una vez publicado.** Otras personas pueden crear mods hijos sobre el tuyo usando `dependencies`, e identifican el mod padre por su `modId`.


### version

- Una cadena que define la versión de tu mod.
- Es lo que se compara cuando otro mod exige una versión exacta mediante `dependencies.version`.
- **Sube la versión cada vez que actualices tu mod.** Al unirse a una sala multijugador, el juego comprueba que el anfitrión y los participantes tengan las mismas versiones de los mods. Si cambias el contenido pero mantienes la misma versión, jugadores con contenido de mod distinto pueden acabar en la misma sala, lo que puede hacer que falle la sincronización de contenido o que se bloquee el progreso de la partida.
- Te recomendamos el mismo formato 1.0.0 que usa el juego.

### author

¡Que todo el mundo sepa quién eres!

### multiplayer / installation

Define si los jugadores de una partida multijugador tienen que compartir este mod. Valores permitidos:

| Valor | Descripción |
|---|---|
| `local_only` | Solo afecta a tu propia pantalla y UI (aquí entran los mods de localización y los mods de UI sencillos) |
| `everyone` | Todos los miembros de la sala deben tener activado el mismo mod (aquí entra cualquier mod que añada o cambie contenido) |
| `host_only` | Solo el anfitrión necesita tenerlo activado; funciona sin cambios en el lado de los demás participantes (aquí entran los mods multijugador que solo afectan a cálculos del lado del servidor) |

Si declaras `installation` como `local_only` o `host_only` pero tu mod toca contenido que los participantes también necesitan (registro de nuevos objetos o unidades, objetos de red, código que también se ejecuta en los clientes de los participantes), los jugadores de la misma sala pueden acabar jugando a partidas distintas, o la sincronización de contenido puede fallar y bloquear el progreso normal de la partida. Asegúrate de comprobar qué hace realmente tu mod.


### modules

- Los módulos definen qué hace tu mod, lo que convierte esta sección en **la más importante**.
- Los módulos actuales son `localization, dll, costume, data`.
- Como en el ejemplo de abajo, puedes combinar varios módulos en un mismo paquete.
- Si hay aunque sea un módulo desconocido, todo el paquete se marca como "no compatible" y el mod se omite durante la carga.

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

- El módulo `localization` es para mods que incluyen archivos de idioma.
- Pon en `path` la carpeta que contiene los archivos de idioma.
- Coloca dentro de la carpeta `path` **archivos JSON con el nombre de los códigos de idioma**, como `ko-KR.json` y `en-US.json`. Consulta [Mods de localización](localization-mods.md) para ver los códigos de idioma y el formato de archivo que usa el juego.

#### type: módulo dll

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- El módulo `dll` te permite engancharte al juego con código C#.
- Pon en `path` la ruta del archivo DLL.
- Pon en `entryClass` el nombre completo de la clase de punto de entrada de la DLL, incluido su espacio de nombres. El juego no busca el punto de entrada dentro de la DLL, así que **debes indicarlo**. (Para clases anidadas, usa `Ns.Outer+Inner`.)
- No uses la misma `entryClass` en dos módulos distintos.
- Solo funciona en compilaciones Mono.
- Consulta [Mods DLL](dll-mods.md) para más detalles.

#### type: módulo costume

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Te permite crear tus propios trajes sin escribir código C#.
- Pon en `path` la carpeta que contiene los archivos de tus trajes.
- En multijugador, todos deben tenerlo instalado.
- Consulta [Mods de trajes](costume-mods.md) para más detalles.


#### type: módulo data

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- Un módulo de datos de uso general para mods. No se usa en los casos habituales; sirve principalmente para dependencias entre mods.
- `consumer` indica qué mod usará este módulo de datos, pero normalmente se omite. Si se omite, todos los mods listados en `dependencies` pueden leer estos datos.
- Si tienes varios mods padre y los datos están destinados a uno solo, pon el `modId` de ese mod en `consumer`. Ese valor también debe estar en `dependencies`.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- Lista de ensamblados auxiliares a los que hace referencia tu módulo `dll`.
- El juego resuelve las referencias usando **solo los archivos declarados aquí**. Dejar una DLL en la carpeta no basta para que se encuentre.
- Las entradas pueden ser una cadena o un objeto `{ "path": "..." }`.
- Cada entrada debe terminar en `.dll`, y no funcionará correctamente si coincide con una DLL de `modules`.
- Las bibliotecas no se tratan como puntos de entrada. Aunque una contenga una clase `WorkshopModBase`, no se llamará.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- Si un mod padre es un mod de "creación de contenido", un mod hijo puede añadir contenido nuevo construido sobre él.
- En ese caso, puedes declarar una dependencia de otro mod usando `dependencies`.
- Cuando se indica un mod padre, tu mod no puede cargarse antes que ese mod padre.
- Si falta un mod padre, está desactivado o no se carga correctamente, se cancela la carga de todos sus mods hijos.
- Una dependencia puede tener los tres campos siguientes.

| Campo | Obligatorio | Regla |
|---|---|---|
| `modId` | Sí | El `modId` del mod sobre el que construyes |
| `version` | No | **Úsalo solo cuando necesites una cadena de versión que coincida exactamente.** Si la versión difiere, se omite la carga, así que normalmente es más seguro dejarlo fuera. |
| `workshopId` | No | Permite a los usuarios encontrar el mod necesario y suscribirse a él. |


## tags

- Categorías que se usan al subir el mod al Workshop.
- Un mod puede tener varias etiquetas. (Para un mod todo en uno que añade objetos y trajes, usa `"tags": ["Item", "Costume"]`.)

| Categoría | Contenido |
|---|---|
| `Item` | Objetos (artefactos, tablas, pociones, etc.), combos |
| `Costume` | Trajes |
| `Weapon` | Armas |
| `NPC` | Viajeros, mercaderes, etc. |
| `Enemy` | Nuevos enemigos, jefes, etc. |
| `Gameplay` | Cambios de jugabilidad |
| `QoL` | Mejoras de UI, comodidad y accesibilidad |
| `Library` | Runtimes, frameworks, paquetes de datos, etc. de los que dependen otros mods |
| `Localization` | Traducciones |

## Reglas de rutas
 
- Las rutas de `modules[].path`, `libraries` e `info.preview` siguen todas las mismas reglas.
- Solo se permiten rutas relativas a la carpeta del mod. (Se rechazan las rutas absolutas, las letras de unidad y `..`.)
- Usa `/` como separador.
- Se rechazan las rutas que pasan por enlaces simbólicos o uniones (junctions).
- **Las mayúsculas y minúsculas deben coincidir exactamente con el nombre real.** En Windows, escribir `localization` sigue abriendo la carpeta, pero el juego lo rechaza por ser distinto de `Localization`. (Esta regla garantiza el mismo comportamiento en Steam Deck (Linux).)
