# Tu primer mod

Esta guía explica la estructura básica de un mod de Sephiria y te acompaña mientras creas tu primer mod y lo compruebas dentro del juego.

## Qué necesitas

- Un buen editor de texto (como [Visual Studio Code](https://code.visualstudio.com/))

## Encontrar la carpeta de instalación del juego

Abre la carpeta de instalación de Sephiria.

- Desde tu biblioteca de Steam
  - `Biblioteca de Steam → clic derecho en Sephiria → Administrar → Ver archivos locales`
- Windows (ruta de instalación por defecto)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (ruta de instalación por defecto)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (la carpeta que contiene `Sephiria.app`)

## Crear la carpeta WorkshopLocal y la carpeta de tu mod

1. Crea una carpeta `WorkshopLocal/` en la carpeta de instalación del juego.
2. Dentro, crea una carpeta para tu propio mod.
   - Sephiria trata cada carpeta como un mod.
   - Los mods creados aquí se pueden probar en el juego y subir al Workshop de Steam.

En esta guía crearás `my-first-mod`, un mod que cambia el texto del botón "Iniciar Juego" de la pantalla de título por "Hello World!". La estructura final queda así:

```
<carpeta de instalación del juego>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← un mod
    │   ├── metadata.json      ← el archivo principal que define el mod
    │   ├── thumbnail.png      ← miniatura de vista previa del mod (necesaria para subirlo al Workshop)
    │   └── Localization/      ← carpeta que lee el módulo de localización
    │       └── en-US.json     ← el texto que se va a sustituir
    └── another-mod/           ← otro mod
        └── ...
```

## El archivo metadata.json

- Crea `metadata.json` en la carpeta de tu mod y pega el contenido de abajo.
- `metadata.json`: el archivo clave que define tu mod.
- `yourname`: sustitúyelo por tu propio apodo.
- Guarda el archivo como **UTF-8**. En el Bloc de notas, elige UTF-8 como codificación en "Guardar como".
- Consulta la [Referencia de metadata.json](metadata.md) para saber qué significa cada campo.

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

## Crear el archivo de idioma

- Crea una carpeta `Localization/` dentro de la carpeta de tu mod.
- Dentro de `Localization/`, crea un archivo `en-US.json` con el contenido de abajo.
- También puedes renombrar el archivo con el código de tu propio idioma para probarlo.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Comprobarlo

Llegados a este punto, ya puedes comprobar si el mod funciona.

1. Inicia Sephiria.
2. Con el idioma del juego en inglés, mira la pantalla de título: el botón "Start Game" ahora dice "Hello World!". Los mods de `WorkshopLocal/` están activados por defecto, así que no hace falta que los actives.
3. Pulsa el botón **Taller de Steam** en la pantalla de título para abrir el panel del Workshop. "My First Mod" debería aparecer en la sección "Mis mods".

### ¡Mi mod no se aplica!

- Comprueba que la sintaxis JSON sea correcta.
- El `path` de `metadata.json` debe coincidir exactamente con el nombre real de la carpeta, incluidas mayúsculas y minúsculas.
- Comprueba que `metadata.json` esté codificado en UTF-8.
- Comprueba que el idioma del juego coincida con el idioma del nombre del archivo.

## Siguientes pasos

1. Aprende cómo se definen los mods en la [Referencia de metadata.json](metadata.md).
2. Los [mods de localización](localization-mods.md) y los [mods de trajes](costume-mods.md) son relativamente fáciles de hacer, así que empieza por ahí.
3. Los [mods DLL](dll-mods.md) son recomendables si tienes conocimientos básicos de programación.
4. Publica tu mod en el Workshop con [Publicar en el Workshop](publishing.md).
