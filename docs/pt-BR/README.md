[한국어](../../README.md) | [English](../../README.en.md) | [日本語](../ja/README.md) | [简体中文](../zh-CN/README.md) | [繁體中文](../zh-TW/README.md) | [Deutsch](../de/README.md) | [Français](../fr/README.md) | [Español](../es/README.md) | [Italiano](../it/README.md) | [Polski](../pl/README.md) | **Português (Brasil)** | [Русский](../ru/README.md) | [Svenska](../sv/README.md) | [ไทย](../th/README.md) | [Türkçe](../tr/README.md)

# Guia de modding de Sephiria

Obrigado por fazer parte da comunidade de modding de Sephiria.

Este é o repositório oficial de guias, exemplos e modelos de modding para [Sephiria](https://store.steampowered.com/app/2436940). Os mods podem ser compartilhados pela [Oficina Steam](https://steamcommunity.com/app/2436940/workshop/).



## Regras

Para manter o jogo seguro para todos, siga as regras abaixo. Violações podem resultar em restrições à publicação de mods ou ao próprio acesso ao jogo.

1. **Nenhum comportamento malicioso**: malware, backdoors, keyloggers, mineradores de criptomoedas e qualquer outro recurso que comprometa o dispositivo, a conta ou os dados do usuário são proibidos.
2. **Uso não comercial**: todos os mods devem ser criados e distribuídos sem fins comerciais.
3. **Sem violação de direitos nem conteúdo nocivo**: é proibido conteúdo que viole direitos autorais, privacidade ou direito de imagem de terceiros, bem como conteúdo sexual ou que promova ódio, assédio ou violência no mundo real.
4. **Não atrapalhe o jogo dos outros**: não faça o jogo funcionar mal de propósito nem interfira no jogo normal de outros jogadores.
5. **Sem coleta de dados**: não colete informações dos usuários nem as envie para destinos externos.
6. **Sem burlar verificações de acesso**: não burle de forma ilegítima as verificações de compra, posse ou acesso do jogo ou da plataforma de distribuição.
7. **Revisão de mods**: mods que contêm código (DLLs) passam por uma revisão, incluindo análise e descompilação, para verificar violações das regras, e podem ser rejeitados mesmo depois de publicados se algum problema for encontrado. Não falsifique resultados de revisão nem selos de aprovação. Os arquivos devem ser fornecidos em um estado que permita a revisão; ofuscação é proibida.
8. **Uso dos recursos do jogo**: descompilar e analisar o código, a arte, o som e outros recursos do jogo é permitido na medida necessária para criar mods. No entanto, não redistribua os recursos extraídos do jogo em si.
9. **Sem falsificação de identidade nem adulteração**: não se passe por outras pessoas nem modifique e redistribua mods de terceiros sem o consentimento deles.



## Antes de começar

- Mods que contêm código (DLLs) são **enviados automaticamente ao sistema de revisão** quando você os envia à Oficina. A revisão verifica principalmente se o código tem comportamento malicioso, e mods que não foram enviados ou que foram rejeitados não são executados nos jogos de outros jogadores. Mods sem código, como traduções e trajes, podem ser enviados imediatamente, sem revisão. Observe que mods que o sistema de revisão não consegue ler serão rejeitados (arquivos ofuscados, DLLs compiladas em C/C++ etc.).
- No multijogador, todos os participantes da sala precisam ter o mesmo conjunto de mods ativado. (Mods pessoais de interface, como traduções, estão isentos.)
- Depois de ativar mods, é preciso reiniciar o jogo para mudar a ordem deles ou desativá-los novamente.
- Mods inscritos vêm desativados por padrão. Você pode ativá-los na lista.



## Documentação

| Documento | Conteúdo |
|---|---|
| [Seu primeiro mod](getting-started.md) | Configurar uma pasta de desenvolvimento, testar um mod localmente, ler os logs |
| [Referência do metadata.json](metadata.md) | O que cada campo significa, regras de dependências e versões, declaração de módulos |
| [Mods de localização](localization-mods.md) | Estrutura dos arquivos de idioma, como encontrar chaves, regras para preservar tags e marcadores |
| [Mods de traje](costume-mods.md) | Criando seu próprio traje |
| [Mods em DLL](dll-mods.md) | Configuração do projeto, ponto de entrada, eventos da `HorayModAPI`, depuração |
| [Publicando na Oficina](publishing.md) | Envio à Oficina, revisão de mods com código, regras de multijogador |
| [Referência da API](api/README.md) | Tipos e membros públicos como `HorayModAPI`, `WorkshopModBase`, opções de mod e UI. Gerada automaticamente a partir do código-fonte do jogo |
| [Apêndice e FAQ](faq.md) |  |


## Licença

O código deste repositório (exemplos e modelos) está licenciado sob a [Licença MIT](../../LICENSE).
A documentação pode ser citada, traduzida e redistribuída livremente; por favor, credite a fonte.
O jogo Sephiria em si e seus recursos são obras da TEAM HORAY e não estão cobertos por esta licença.
