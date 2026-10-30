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
3. Selecciona tu mod y pulsa **Subir al Workshop**.
4. En los mods sin código, la subida al Workshop es el último paso.
5. En los mods que incluyen código (DLL), cuando termina la subida al Workshop, el mod se envía automáticamente al **sistema de revisión** junto con el ID de su elemento del Workshop.
6. Ve a la página del Workshop que se ha subido, haz una prueba final (por ejemplo, suscribiéndote) y cambia la visibilidad a "Público".

Si la subida al Workshop terminó pero solo falló el envío a revisión (por ejemplo, por un error de red), vuelve a pulsar **Subir al Workshop** en el mismo mod. Si el contenido no ha cambiado, el mod no se vuelve a subir al Workshop; solo se repite el envío. El envío debe hacerse con la cuenta de Steam que creó el elemento del Workshop.

## Revisión de código

- El sistema de revisión descompila las DLL y comprueba si hay comunicación de red, acceso a archivos, ofuscación y comportamiento malicioso. La revisión combina el análisis automatizado con comprobaciones de operadores, y somos conscientes de que ningún sistema de revisión puede detectar todos los riesgos. Aun así, nuestro objetivo es reducir la propagación de mods maliciosos mediante este proceso de revisión. Gracias por tu colaboración.
- Todos los mods que contienen código (DLL) **también deben enviarse al sistema de revisión de mods.** Para ello, súbelos siempre con el botón **Subir al Workshop** del juego.
- Por lo general, los jugadores no pueden ejecutar los mods que se subieron al Workshop pero no se enviaron al sistema de revisión.
- Los jugadores que descargan un mod consultan su estado de revisión mediante el ID del elemento del Workshop del mod y las DLL instaladas.
  - **Enviado / Aprobado**: El mod se ejecuta. Se ejecuta incluso antes de que termine la revisión.
  - **Sin enviar / Rechazado**: El mod no se ejecuta y el motivo se muestra en el panel de mods.
- Si se detecta un problema durante la revisión, un mod puede rechazarse incluso después de publicarse. En un mod rechazado, puedes ver el motivo con **Ver motivo del rechazo** en el panel. Las DLL rechazadas no se pueden volver a publicar tal cual; corrige el problema y vuelve a subirlo.
- Aunque no tenga un módulo `dll`, un mod se somete a revisión si tiene `libraries` o contiene una DLL o un EXE con la extensión cambiada.
- Los mods que pasan por la revisión de código deben ocupar 20 MB o menos con la carpeta comprimida.

### Motivos de fallo de revisión

|Mensaje|Causa|
|---|---|
|No hay ningún registro de envío en el sistema de revisión.|El mod se subió al Workshop pero no se envió al sistema de revisión. Vuelve a enviarlo con el botón **Subir al Workshop**.|
|El sistema de revisión ha rechazado la aprobación de este mod.|El equipo de operaciones rechazó el mod por algún motivo. Consulta el motivo del rechazo.|
|No se ha podido obtener información del servidor de revisión.|Puede que la conexión a internet del suscriptor no funcione o que haya un problema con el servidor de revisión.|
|Este mod es del sistema de revisión anterior.|El servidor de revisión se actualizó y se restablecieron los datos de verificación existentes del mod. Vuelve a enviarlo.|
|Se ha detectado una DLL no autorizada en el mod.|Si el conjunto de archivos DLL del mod cambia mientras el mod se está ejecutando, se restringe la carga de ese mod. Es decir, lo siguiente puede infringir el sistema de revisión: el mod descarga una DLL del exterior mientras se ejecuta y carga el ensamblado; el mod crea y ejecuta su propia DLL mientras se ejecuta; el mod extrae una DLL empaquetada en su interior con su propio formato de compresión y la usa.|

## Visibilidad y publicación

- La primera vez que lo subes, la visibilidad queda como `No listado`.
- Antes de hacerlo público, suscríbete tú mismo, comprueba que la copia instalada funciona correctamente y luego cámbialo a "Público".
- Puedes editar la descripción de la página, las capturas de pantalla, las descripciones en otros idiomas, etc. desde la página del elemento en el Workshop de Steam.

## Actualizar tu mod

Puedes actualizar tu mod de la misma forma en que lo publicaste.

- Una actualización que cambia DLL se vuelve a enviar automáticamente tras la subida y pasa de nuevo por la revisión. Si no cambiaste ninguna DLL, el estado de revisión se mantiene.
- No cambies tu `modId`. El mod se trataría como un mod distinto, lo que puede romper dependencias y más cosas.
- **Sube siempre `version` en `metadata.json` con cada actualización.** Las versiones de los mods se comparan al unirse a una sala multijugador, así que si el contenido cambió pero la versión no, jugadores con contenido de mod distinto pueden acabar en la misma partida y tener problemas.

## Ticket de subida

Después de subir el mod, se añade `uploadticket.json` a la carpeta de tu mod. Contiene el ID del elemento del Workshop y la información del envío a revisión, así que **no lo borres.** Si gestionas la carpeta de tu mod con git, lo más seguro es confirmar también este archivo. Si cambias de ordenador o formateas, necesitarás este archivo para seguir actualizando el mismo elemento del Workshop. Sin él, el juego trata la carpeta como un mod nuevo que nunca se ha publicado.

`mod-guard-manifest.json` y `mod-guard.sig.json`, que usaba el sistema de revisión antes de la 1.1.1, ya no se usan. Aunque sigan ahí, no se suben al Workshop, y puedes borrarlos.

Consulta las [preguntas frecuentes](faq.md) para saber cómo recuperar el archivo si lo pierdes.


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
