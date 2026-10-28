[한국어](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Polski](README.pl.md) | **Português (Brasil)** | [Русский](README.ru.md) | [Svenska](README.sv.md) | [ไทย](README.th.md) | [Türkçe](README.tr.md)

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
7. **Revisão de mods**: mods que contêm código (DLLs) passam por uma revisão antes da aprovação, incluindo análise e descompilação, para verificar violações das regras. A aprovação pode levar algum tempo. Não falsifique resultados de revisão nem selos de aprovação. Os arquivos devem ser fornecidos em um estado que permita a revisão; ofuscação é proibida.
8. **Uso dos recursos do jogo**: descompilar e analisar o código, a arte, o som e outros recursos do jogo é permitido na medida necessária para criar mods. No entanto, não redistribua os recursos extraídos do jogo em si.
9. **Sem falsificação de identidade nem adulteração**: não se passe por outras pessoas nem modifique e redistribua mods de terceiros sem o consentimento deles.



## Antes de começar

- Mods que contêm código (DLLs) são **revisados** antes de poderem ser enviados à Oficina. A revisão verifica principalmente se o código tem comportamento malicioso. Mods sem código, como traduções e trajes, podem ser enviados imediatamente, sem revisão. Observe que mods que o sistema de revisão não consegue ler serão rejeitados (arquivos ofuscados, DLLs compiladas em C/C++ etc.).
- No multijogador, todos os participantes da sala precisam ter o mesmo conjunto de mods ativado. (Mods pessoais de interface, como traduções, estão isentos.)
- Depois de ativar mods, é preciso reiniciar o jogo para mudar a ordem deles ou desativá-los novamente.
- Mods inscritos vêm desativados por padrão. Você pode ativá-los na lista.



## Documentação

| Documento | Conteúdo |
|---|---|
| [Seu primeiro mod](docs/pt-BR/getting-started.md) | Configurar uma pasta de desenvolvimento, testar um mod localmente, ler os logs |
| [Referência do metadata.json](docs/pt-BR/metadata.md) | O que cada campo significa, regras de dependências e versões, declaração de módulos |
| [Mods de localização](docs/pt-BR/localization-mods.md) | Estrutura dos arquivos de idioma, como encontrar chaves, regras para preservar tags e marcadores |
| [Mods de traje](docs/pt-BR/costume-mods.md) | Criando seu próprio traje |
| [Mods em DLL](docs/pt-BR/dll-mods.md) | Configuração do projeto, ponto de entrada, eventos da `HorayModAPI`, depuração |
| [Publicando na Oficina](docs/pt-BR/publishing.md) | Envio à Oficina, revisão de mods com código, regras de multijogador |
| [Referência da API](docs/pt-BR/api/README.md) | Tipos e membros públicos como `HorayModAPI`, `WorkshopModBase`, opções de mod e UI. Gerada automaticamente a partir do código-fonte do jogo |
| [Apêndice e FAQ](docs/pt-BR/faq.md) |  |


## Licença

O código deste repositório (exemplos e modelos) está licenciado sob a [Licença MIT](LICENSE).
A documentação pode ser citada, traduzida e redistribuída livremente; por favor, credite a fonte.
O jogo Sephiria em si e seus recursos são obras da TEAM HORAY e não estão cobertos por esta licença.
