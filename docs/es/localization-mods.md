# Mods de localización

[Tu primer mod](getting-started.md) cubrió lo básico para crear un mod, así que esta página trata las reglas comunes de los mods de localización.
[`examples/translation-sample`](../../examples/translation-sample) contiene un ejemplo completo que puedes copiar directamente en `WorkshopLocal/` para probarlo.



## Cómo funciona

- El juego tiene una tabla **clave → cadena** por idioma. Cuando se carga un mod, los valores del mod sobrescriben la tabla original.
- Tu archivo solo necesita las claves que quieras cambiar.
- El nombre del archivo es el idioma. `ko-KR.json` sobrescribe la tabla de coreano, y `en-US.json` sobrescribe la tabla de inglés.
- Si varios mods de localización definen la misma clave, gana el mod que se carga más tarde.
- Un mod que solo cambia texto no afecta a los demás participantes, así que te recomendamos poner `multiplayer.installation` en `metadata.json` como `local_only`.
- Tanto las claves como los valores son cadenas. No se permiten objetos anidados ni arrays.
- Guarda los archivos como **UTF-8**.

```
my-translation-mod/
├── metadata.json              ← añade { "type": "localization", "path": "Localization" } a modules
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## Qué no traducir

El texto del juego contiene expresiones que se interpretan en tiempo de ejecución.
No traduzcas el texto de abajo; déjalo en inglés exactamente como está (distingue mayúsculas y minúsculas).

| Tipo    | Ejemplo                                                                     | Descripción                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Palabras clave   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Se sustituyen por números o nombres en tiempo de ejecución. No cambies ni un solo carácter dentro de las llaves.   |
| Etiquetas del juego | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Se sustituyen por descripciones emergentes de palabras clave e iconos. Los nombres dentro de las etiquetas son fijos, incluidas mayúsculas y minúsculas. |
| Etiquetas de formato | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Formato de texto. Las etiquetas de apertura y cierre deben ir emparejadas.        |
| Saltos de línea   | `\n`                                                                        | Un salto de línea dentro de una cadena JSON.                        |




## Añadir un idioma nuevo

- También puedes añadir un idioma que el juego no admite. Si el nombre del archivo es un código de idioma nuevo, el juego lo registra como idioma nuevo y lo muestra en la lista de ajustes de idioma.
- Los archivos de idioma originales están en `<carpeta de instalación del juego>/Sephiria_Data/StreamingAssets/Localization/`, uno por idioma; úsalos como referencia al traducir.
- Los idiomas que el juego ya admite son `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH` y `tr-TR`.
- **Traduce todas las claves.** Las claves que faltan en tu archivo de mod no se muestran en blanco; recurren al **inglés** (o al coreano si también falta el inglés). La forma más fácil de empezar es copiar el `en-US.json` original.
- **Rellena las claves de configuración que empiezan por `#`.** `#LanguageName` es el nombre que se muestra en la lista de ajustes de idioma, y `#FontName` es la fuente que se usará. Ajusta las claves de puntuación como `#Colon`, `#Bullet` y `#ParenthesisOpen` a las convenciones de ese idioma.
- Para `#FontName`, puedes elegir entre las fuentes integradas en el juego. El juego incluye fuentes para las siguientes escrituras:

| Escritura | `#FontName` |
|---|---|
| Latina, cirílica, hangul | `GalCyrillic` |
| Japonés | `JF-Dot-MPlus10` |
| Chino simplificado | `Fusion_Pixel_10_Simple` |
| Chino tradicional | `BoutiqueBitmap9x9` |
| Tailandés | `NotoSansThai` |



## Solución de problemas

| Síntoma | Qué comprobar |
|---|---|
| El mod está activado pero el texto no cambia | Si el nombre de la clave coincide exactamente con el original, carácter por carácter. Si el nombre del archivo coincide con el idioma actual del juego (`ko-KR.json` solo se aplica en coreano). |
| Solo algunas claves no cambian | Si otro mod de localización está sobrescribiendo la misma clave más tarde. Prueba a cambiar el orden en el panel. |
| `{VALUE}` aparece tal cual en el texto | La palabra clave está escrita de forma distinta al original. Cópiala directamente del texto original. |
| `<tag=…>` aparece tal cual en el texto | La ortografía o las mayúsculas del nombre de la etiqueta son distintas. |
| El texto en coreano (u otro idioma no inglés) aparece ilegible | El archivo no está en UTF-8. Guárdalo de nuevo como UTF-8. |
| Los caracteres aparecen como cuadrados | Esos caracteres no están incluidos en las fuentes integradas del juego. |
| Se ignora un archivo entero | Ese archivo tiene un error de sintaxis JSON. `Player.log` registra el error junto con el nombre del archivo. |

El archivo de registro está en `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`. Busca "모드 내 언어 파일 발견" (archivo de idioma encontrado en el mod) para ver qué archivos se leyeron y cuántas claves tenía cada uno.
