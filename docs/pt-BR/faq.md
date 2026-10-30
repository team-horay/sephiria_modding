# FAQ


## Como vejo os logs?

Os logs do jogo são gravados no arquivo abaixo. Ele é reescrito toda vez que você inicia o jogo, e a execução anterior é mantida em `Player-prev.log`.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

As linhas relacionadas a mods começam com uma tag entre colchetes, então procure por essa tag.

| Tag | Conteúdo |
|---|---|
| `[WorkshopModManager]` | Consulta da lista de inscrições, downloads, varredura da pasta local, resumo do plano de carregamento |
| `[WorkshopModRuntime]` | Ativação de DLLs e resultados da mesclagem de localização |
| `[WorkshopUploader]` | Upload para a Oficina e envio para revisão |
| `[ModGuardClient]` | Comunicação com o servidor de revisão |
| `[WorkshopLobbyMods]` | Comparação dos conjuntos de mods em salas multijogador |
| `모드 내 언어 파일 발견` | Número de chaves lidas de cada arquivo de localização |


## O que é "Mod local (pasta AddOns)" no painel da Oficina da Steam?

Você pode ver uma entrada "Mod local (pasta AddOns)" no painel. Esse é um recurso de modding legado, então não o use para mods novos.
Mods legados não podem ser sincronizados com outros jogadores, então recomendamos removê-los.


## Quero mover a pasta WorkshopLocal para outro lugar que não seja a pasta de instalação de Sephiria.

Se você quiser manter seus mods em outra unidade ou em uma pasta de trabalho, pode passar o caminho como argumento de inicialização. Na sua biblioteca Steam, vá em Sephiria → Propriedades → Geral → Opções de inicialização e digite:

```
-workshop_local=D:\mods\my-first-mod
```

Separe vários caminhos com ponto e vírgula e coloque tudo entre aspas se algum caminho contiver espaços.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

As pastas passadas como argumento são tratadas exatamente como subpastas de `WorkshopLocal/`.


## Perdi o uploadticket.json.

O `mod-guard-manifest.json` e o `mod-guard.sig.json` não são mais usados a partir da 1.1.1, então não tem problema perdê-los. O único arquivo necessário é o `uploadticket.json`.

Se você perdeu o `uploadticket.json` (por exemplo, depois de formatar o computador), o jogo trata a pasta como um mod novo, então enviá-la assim cria um novo item na Oficina em vez de atualizar o existente. Para continuar atualizando o item existente, recupere-o nesta ordem:

1. Inscreva-se no seu próprio mod na Oficina.
2. Copie a pasta inteira que a Steam baixou (`<biblioteca Steam>/steamapps/workshop/content/2436940/<ID do item da Oficina>/`) para dentro de `WorkshopLocal/`. Qualquer nome de pasta serve.
3. Na pasta copiada, crie um `uploadticket.json` com o conteúdo abaixo. Para o número, use os dígitos depois de `id=` na URL da página da Oficina.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. Inicie o jogo, selecione o mod no painel e pressione **Enviar para a Oficina**. **O item existente é atualizado** e, se for um mod com código, ele é reenviado para revisão com o mesmo ID do item. (Atenção: se você duplicou a pasta de um mod existente para criar um mod diferente, **precisa excluir** o `uploadticket.json`. Se deixá-lo, o novo mod vai sobrescrever o item da Oficina do mod original.)
