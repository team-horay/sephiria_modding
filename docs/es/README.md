[한국어](../../README.md) | [English](../../README.en.md) | [日本語](../ja/README.md) | [简体中文](../zh-CN/README.md) | [繁體中文](../zh-TW/README.md) | [Deutsch](../de/README.md) | [Français](../fr/README.md) | **Español** | [Italiano](../it/README.md) | [Polski](../pl/README.md) | [Português (Brasil)](../pt-BR/README.md) | [Русский](../ru/README.md) | [Svenska](../sv/README.md) | [ไทย](../th/README.md) | [Türkçe](../tr/README.md)

# Guía de modding de Sephiria

Gracias por unirte a la comunidad de modding de Sephiria.

Este es el repositorio oficial de guías, ejemplos y plantillas de modding para [Sephiria](https://store.steampowered.com/app/2436940). Los mods se pueden compartir a través del [Workshop de Steam](https://steamcommunity.com/app/2436940/workshop/).



## Normas

Para que el juego siga siendo seguro para todo el mundo, respeta las normas siguientes. Las infracciones pueden conllevar restricciones a la hora de publicar mods o de jugar.

1. **Sin comportamiento malicioso**: Están prohibidos el malware, las puertas traseras, los keyloggers, los mineros de criptomonedas y cualquier otra función que comprometa el dispositivo, la cuenta o los datos de un usuario.
2. **Uso no comercial**: Todos los mods deben crearse y distribuirse sin fines comerciales.
3. **Sin vulneración de derechos ni contenido dañino**: Está prohibido el contenido que vulnere los derechos de autor, la privacidad o el derecho a la propia imagen de terceros, así como el contenido sexual o el que fomente el odio, el acoso o la violencia en el mundo real.
4. **Sin interferir en el juego**: No hagas que el juego funcione mal de forma intencionada ni interfieras en el juego normal de otros jugadores.
5. **Sin recopilación de datos**: No recopiles información de los usuarios ni la envíes a destinos externos.
6. **Sin eludir comprobaciones de acceso**: No eludas de forma ilegítima las comprobaciones de compra, propiedad o acceso del juego ni de la plataforma de distribución.
7. **Revisión de mods**: Los mods que contienen código (DLL) pasan por una revisión antes de aprobarse, que incluye análisis y descompilación, para comprobar que no infringen las normas. La aprobación puede tardar un tiempo. No falsifiques resultados de revisión ni marcas de aprobación. Los archivos deben entregarse en un estado que permita revisarlos; la ofuscación está prohibida.
8. **Uso de los recursos del juego**: Se permite descompilar y analizar el código, el arte, el sonido y demás recursos del juego en la medida necesaria para crear mods. Sin embargo, no redistribuyas los recursos extraídos del juego como tales.
9. **Sin suplantación ni manipulación**: No te hagas pasar por otras personas ni modifiques y redistribuyas mods de otros sin su consentimiento.



## Antes de empezar

- Los mods que contienen código (DLL) se **revisan** antes de poder subirlos al Workshop. La revisión comprueba principalmente que el código no tenga comportamiento malicioso. Los mods sin código, como traducciones y trajes, se pueden subir de inmediato sin revisión. Ten en cuenta que los mods que el sistema de revisión no puede leer serán rechazados (archivos ofuscados, DLL compiladas desde C/C++, etc.).
- En multijugador, todos los participantes de la sala deben tener activado el mismo conjunto de mods. (Los mods de UI personales, como las traducciones, están exentos.)
- Después de activar mods, tienes que reiniciar el juego para cambiar su orden o volver a desactivarlos.
- Los mods suscritos están desactivados por defecto. Puedes activarlos tú mismo desde la lista.



## Documentación

| Documento | Contenido |
|---|---|
| [Tu primer mod](getting-started.md) | Preparar una carpeta de desarrollo, comprobar un mod en local, leer los registros |
| [Referencia de metadata.json](metadata.md) | Qué significa cada campo, reglas de dependencias y versiones, declaración de módulos |
| [Mods de localización](localization-mods.md) | Estructura de los archivos de idioma, cómo encontrar claves, reglas para conservar etiquetas y marcadores |
| [Mods de trajes](costume-mods.md) | Crear tu propio traje |
| [Mods DLL](dll-mods.md) | Configuración del proyecto, punto de entrada, eventos de `HorayModAPI`, depuración |
| [Publicar en el Workshop](publishing.md) | Subir al Workshop, revisión de mods con código, reglas de multijugador |
| [Referencia de la API](api/README.md) | Tipos y miembros públicos como `HorayModAPI`, `WorkshopModBase`, opciones de mod y UI. Generada automáticamente a partir del código fuente del juego |
| [Apéndice y preguntas frecuentes](faq.md) |  |


## Licencia

El código de este repositorio (ejemplos y plantillas) se distribuye bajo la [Licencia MIT](../../LICENSE).
La documentación se puede citar, traducir y redistribuir libremente; por favor, indica la fuente.
El juego Sephiria y sus recursos son obra de TEAM HORAY y no están cubiertos por esta licencia.
