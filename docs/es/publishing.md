# Publicar en el Workshop

Esta página explica cómo subir tu mod al Workshop de Steam, cómo funciona el sistema de revisión y cómo se sincronizan los mods entre jugadores en multijugador.


## Antes de subirlo

- Para subir un mod al Workshop se necesita una imagen de vista previa (PNG o JPG, de menos de 1 MB). Defínela con `info.preview` o pon un `thumbnail.png` en la carpeta de tu mod. Consulta la [Referencia de metadata.json](metadata.md#preview) para más detalles.
- `info.title` puede tener hasta 128 caracteres e `info.description` hasta 7999 caracteres. Si los escribiste en varios idiomas, en la página del Workshop se usa la versión `en-US`.
- Pon las categorías del Workshop en `tags`. Consulta la [Referencia de metadata.json](metadata.md) para ver las categorías disponibles.
- Todos los archivos de la carpeta se someten a revisión, así que asegúrate de que la carpeta de tu mod no contenga copias de las DLL del juego, archivos `.pdb` ni resultados intermedios de compilación.

## Pasos para publicar

1. Inicia Sephiria.
2. Abre el panel **Taller de Steam** en la pantalla de título y busca el mod que creaste.
3. Selecciona tu mod.
4. Los mods que incluyen código (DLL) pasan por una revisión de código.
5. Los mods sin código se pueden subir al Workshop de inmediato.
6. Ve a la página del Workshop que se ha subido, haz una prueba final (por ejemplo, suscribiéndote) y cambia la visibilidad a "Público".

## Revisión de código

- El sistema de revisión comprueba todos los archivos de la carpeta de tu mod. Las DLL se descompilan y se comprueba si hay comunicación de red, acceso a archivos, ofuscación y comportamiento malicioso; en los archivos de datos como imágenes, sonidos y JSON se comprueba que el formato sea válido.
- Si no se encuentra ningún problema, el mod se aprueba automáticamente; si algo parece sospechoso, un operador lo comprueba manualmente. Los mods claramente maliciosos se rechazan automáticamente.
- La revisión combina el análisis automatizado con comprobaciones de operadores, y somos conscientes de que ningún sistema de revisión puede detectar todos los riesgos. Aun así, nuestro objetivo es reducir la propagación de mods maliciosos mediante este proceso previo de revisión y aprobación. Gracias por tu colaboración.
- Que un mod contenga código o no se determina por el **contenido** de los archivos, no por su nombre. Aunque no tenga un módulo `dll`, un mod se somete a revisión si tiene `libraries` o contiene una DLL o un EXE con la extensión cambiada.
- Los mods que pasan por la revisión de código deben ocupar 20 MB o menos con la carpeta comprimida.

## Certificado de aprobación (solo mods con código)

- Los mods que contienen código reciben un certificado de aprobación.
- Certifica que los archivos del mod son idénticos a la versión aprobada.
- Los mods DLL deben pasar por esta certificación antes de que los jugadores que descarguen el mod puedan ejecutar su código.
- Si cambia aunque sea un solo archivo después de la certificación, el certificado deja de ser válido y el mod debe revisarse de nuevo.

## Visibilidad y publicación

- La primera vez que lo subes, la visibilidad queda como `No listado`.
- Antes de hacerlo público, suscríbete tú mismo, comprueba que la copia instalada funciona correctamente y luego cámbialo a "Público".
- Puedes editar la descripción de la página, las capturas de pantalla, las descripciones en otros idiomas, etc. desde la página del elemento en el Workshop de Steam.

## Actualizar tu mod

Puedes actualizar tu mod de la misma forma en que lo publicaste.

- Los mods DLL necesitan un nuevo certificado de aprobación.
- No cambies tu `modId`. El mod se trataría como un mod distinto, lo que puede romper dependencias y más cosas.
- **Sube siempre `version` en `metadata.json` con cada actualización.** Las versiones de los mods se comparan al unirse a una sala multijugador, así que si el contenido cambió pero la versión no, jugadores con contenido de mod distinto pueden acabar en la misma partida y tener problemas.

## Ticket de subida

Después de solicitar la revisión y subir el mod, se añaden a la carpeta de tu mod los archivos de abajo. Contienen la información de aprobación de tu revisión, así que **no los borres.** Si gestionas la carpeta de tu mod con git, lo más seguro es confirmar también estos tres archivos. Si cambias de ordenador o formateas, necesitarás estos archivos para seguir actualizando el mismo elemento del Workshop.

| Archivo | Contenido |
|---|---|
| `uploadticket.json` | El ticket de revisión y el ID del elemento del Workshop. Sin él, el juego trata la carpeta como un mod nuevo que nunca se ha publicado. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | El certificado de aprobación de la revisión: la lista de archivos aprobados y una firma. Editarlos invalida la aprobación. |

Consulta las [preguntas frecuentes](faq.md) para saber cómo recuperar estos archivos si los pierdes.

## Revocación de la aprobación

Si se detecta una infracción de las normas después de la publicación, un operador puede revocar la aprobación. Un mod revocado no se carga aunque esté instalado, y se muestra en el panel como "Aprobación de revisión revocada".


## Reglas de multijugador

### Configuración de la sala multijugador

Cuando el anfitrión crea una sala multijugador, **la lista de mods que el anfitrión tiene activados** se registra en la sala.

- Mods con `installation` en `host_only`: los participantes no tienen que instalarlos; solo se muestran en la lista.
- Mods con `multiplayer.installation` en `everyone` (o sin declarar): los participantes también deben instalarlos.
- Mods de trajes y mods de datos: los participantes también deben instalarlos.

### Unirse

Si la configuración de mods actual de un jugador difiere de la de la sala, pasa por estos pasos: descargar → activar → reiniciar el juego e intentar unirse de nuevo.

### Mods de prueba instalados en WorkshopLocal

El multijugador no está disponible en una partida que tenga cargados mods locales de `WorkshopLocal/`. Para probar el multijugador, sube tu mod al Workshop, vacía la carpeta local y usa la copia suscrita. Puedes suscribirte mediante el enlace incluso mientras el elemento está en `No listado`.
