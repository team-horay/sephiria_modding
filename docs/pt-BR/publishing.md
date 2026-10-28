# Publicando na Oficina

Esta página explica como enviar seu mod à Oficina Steam, como funciona o sistema de revisão e como os mods são sincronizados entre jogadores no multijogador.


## Antes de enviar

- Para enviar um mod à Oficina, é obrigatória uma imagem de pré-visualização (PNG ou JPG, com menos de 1 MB). Defina-a com `info.preview` ou coloque um `thumbnail.png` na pasta do seu mod. Consulte a [Referência do metadata.json](metadata.md#preview) para mais detalhes.
- `info.title` pode ter até 128 caracteres e `info.description` até 7999 caracteres. Se você os escreveu em vários idiomas, a versão `en-US` é usada na página da Oficina.
- Coloque as categorias da Oficina em `tags`. Consulte a [Referência do metadata.json](metadata.md) para ver as categorias disponíveis.
- Todos os arquivos da pasta passam pela revisão, então certifique-se de que a pasta do seu mod não contenha cópias das DLLs do jogo, arquivos `.pdb` ou saídas intermediárias de build.

## Passos para publicar

1. Inicie Sephiria.
2. Abra o painel **Oficina da Steam** na tela de título e encontre o mod que você criou.
3. Selecione o seu mod.
4. Mods que incluem código (DLLs) passam pela revisão de código.
5. Mods sem código podem ser enviados à Oficina imediatamente.
6. Vá até a página da Oficina do item enviado, faça um teste final (como se inscrever nele) e depois mude a visibilidade para "Público".

## Revisão de código

- O sistema de revisão verifica todos os arquivos da pasta do seu mod. As DLLs são descompiladas e verificadas quanto a comunicação de rede, acesso a arquivos, ofuscação e comportamento malicioso; arquivos de dados, como imagens, sons e JSON, são verificados quanto à validade do formato.
- Se nenhum problema for encontrado, o mod é aprovado automaticamente; se algo parecer suspeito, um operador faz a verificação manualmente. Mods claramente maliciosos são rejeitados automaticamente.
- A revisão combina análise automatizada com verificações de operadores, e entendemos que nenhum sistema de revisão consegue detectar todos os riscos. Ainda assim, buscamos reduzir a disseminação de mods maliciosos com esse processo prévio de revisão e aprovação. Agradecemos a sua colaboração.
- Se um mod contém código ou não é determinado pelo **conteúdo** do arquivo, não pelo nome. Mesmo sem um módulo `dll`, um mod passa pela revisão se tiver `libraries` ou contiver uma DLL ou um EXE com a extensão alterada.
- Mods que passam pela revisão de código precisam ter 20 MB ou menos com a pasta compactada.

## Certificado de aprovação (apenas mods com código)

- Mods que contêm código recebem um certificado de aprovação.
- Ele certifica que os arquivos do mod são idênticos à versão aprovada.
- Mods em DLL precisam passar por essa certificação antes que os jogadores que baixarem o mod possam executar o código dele.
- Se ao menos um arquivo mudar depois da certificação, o certificado perde a validade e o mod precisa ser revisado novamente.

## Visibilidade e publicação

- No primeiro envio, a visibilidade é definida como `Não listado`.
- Antes de tornar público, inscreva-se você mesmo, verifique se a cópia instalada funciona corretamente e depois mude para "Público".
- Você pode editar a descrição da página, as capturas de tela, as descrições em outros idiomas e assim por diante na página do item na Oficina Steam.

## Atualizando o seu mod

Você pode atualizar o seu mod da mesma forma que o publicou.

- Mods em DLL precisam de um novo certificado de aprovação.
- Não mude o seu `modId`. O mod seria tratado como um mod diferente, o que pode quebrar dependências e muito mais.
- **Sempre aumente o `version` no `metadata.json` a cada atualização.** As versões dos mods são comparadas ao entrar em uma sala multijogador, então, se o conteúdo mudou mas a versão não, jogadores com conteúdos de mod diferentes podem acabar na mesma partida e ter problemas.

## Ticket de upload

Depois que você solicita a revisão e faz o upload, os arquivos abaixo são adicionados à pasta do seu mod. Eles contêm as informações da aprovação da revisão, então **não os exclua.** Se você gerencia a pasta do seu mod com git, o mais seguro é fazer commit desses três arquivos também. Se trocar de computador ou formatar, você vai precisar desses arquivos para continuar atualizando o mesmo item da Oficina.

| Arquivo | Conteúdo |
|---|---|
| `uploadticket.json` | O ticket de revisão e o ID do item da Oficina. Sem ele, o jogo trata a pasta como um mod novo que nunca foi publicado. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | O certificado de aprovação da revisão: a lista de arquivos aprovados e uma assinatura. Editá-los invalida a aprovação. |

Consulte o [FAQ](faq.md) para saber como recuperar esses arquivos caso os perca.

## Revogação da aprovação

Se uma violação das regras for encontrada depois da publicação, um operador pode revogar a aprovação. Um mod revogado não é carregado mesmo que esteja instalado, e aparece no painel como "Aprovação da revisão revogada".


## Regras de multijogador

### Configuração da sala multijogador

Quando o anfitrião cria uma sala multijogador, **a lista de mods que o anfitrião ativou** é registrada na sala.

- Mods com `installation` definido como `host_only`: os participantes não precisam instalá-los; eles só aparecem na lista.
- Mods com `multiplayer.installation` definido como `everyone` (ou não declarado): os participantes também precisam instalá-los.
- Mods de traje e mods de dados: os participantes também precisam instalá-los.

### Entrando

Se a configuração atual de mods de um jogador for diferente da configuração da sala, ele passa por estes passos: baixar → ativar → reiniciar o jogo e tentar entrar de novo.

### Mods de teste instalados em WorkshopLocal

O multijogador não fica disponível em uma partida com mods locais de `WorkshopLocal/` carregados. Para testar o multijogador, envie o seu mod à Oficina, esvazie a pasta local e use a cópia inscrita. Você pode se inscrever pelo link mesmo enquanto o item está `Não listado`.
