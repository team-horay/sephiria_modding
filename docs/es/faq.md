# Preguntas frecuentes


## ¿Cómo veo los registros?

Los registros del juego se escriben en el archivo de abajo. Se reescribe cada vez que inicias el juego, y la ejecución anterior se conserva en `Player-prev.log`.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

Las líneas relacionadas con mods empiezan con una etiqueta entre corchetes, así que busca esa etiqueta.

| Etiqueta | Contenido |
|---|---|
| `[WorkshopModManager]` | Consulta de la lista de suscripciones, descargas, exploración de carpetas locales, resumen del plan de carga |
| `[WorkshopModRuntime]` | Activación de DLL y resultados de la fusión de localización |
| `[WorkshopUploader]` | Envío a revisión y subida al Workshop |
| `[ModGuardClient]` | Comunicación con el servidor de revisión |
| `[WorkshopLobbyMods]` | Comparación de conjuntos de mods en salas multijugador |
| `모드 내 언어 파일 발견` | Número de claves leídas de cada archivo de localización |


## ¿Qué es "Mod local (carpeta AddOns)" en el panel del Taller de Steam?

Puede que veas una entrada "Mod local (carpeta AddOns)" en el panel. Es una función de modding heredada, así que no la uses para mods nuevos.
Los mods heredados no se pueden sincronizar con otros jugadores, así que te recomendamos eliminarlos.


## Quiero mover la carpeta WorkshopLocal a otro sitio que no sea la carpeta de instalación de Sephiria.

Si quieres guardar tus mods en otra unidad o en una carpeta de trabajo, puedes pasar la ruta como argumento de inicio. En tu biblioteca de Steam, ve a Sephiria → Propiedades → General → Opciones de lanzamiento e introduce:

```
-workshop_local=D:\mods\my-first-mod
```

Separa varias rutas con punto y coma, y pon todo entre comillas si alguna ruta contiene espacios.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

Las carpetas pasadas como argumento se tratan exactamente igual que las subcarpetas de `WorkshopLocal/`.


## He perdido uploadticket.json, mod-guard-manifest.json y mod-guard.sig.json.

Si solo faltan los archivos del certificado (`mod-guard-manifest.json`, `mod-guard.sig.json`) y `uploadticket.json` sigue ahí, selecciona el mod en el panel y pulsa el botón **Obtener prueba de aprobación** para volver a descargarlos del servidor de revisión. Sin embargo, si también perdiste `uploadticket.json` (por ejemplo, tras formatear), el juego trata la carpeta como un mod nuevo, así que subirla tal cual crea un elemento nuevo en el Workshop en lugar de actualizar el existente. Para seguir actualizando el elemento existente, recupéralo en este orden:

1. Suscríbete a tu propio mod en el Workshop.
2. Copia la carpeta completa que Steam descargó (`<biblioteca de Steam>/steamapps/workshop/content/2436940/<ID del elemento del Workshop>/`) en `WorkshopLocal/`. Vale cualquier nombre de carpeta.
3. En la carpeta copiada, crea `uploadticket.json` con el contenido de abajo. Para el número, usa los dígitos que siguen a `id=` en la URL de la página del Workshop.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. Al iniciar el juego, el mod aparece en el panel como "Sin enviar". Pasa por solicitar revisión → aprobación → obtener prueba de aprobación → volver a subir al Workshop, y **el elemento existente se actualiza**. (Nota: si duplicaste la carpeta de un mod existente para hacer un mod distinto, **debes borrar** `uploadticket.json` y los archivos del certificado. Si los dejas, el mod nuevo sobrescribirá el elemento del Workshop del mod original.)
