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
3. Selecione o seu mod e pressione **Enviar para a Oficina**.
4. Para mods sem código, o envio à Oficina é a última etapa.
5. Para mods que incluem código (DLLs), assim que o envio à Oficina termina, o mod é enviado automaticamente ao **sistema de revisão** junto com o ID do item da Oficina.
6. Vá até a página da Oficina do item enviado, faça um teste final (como se inscrever nele) e depois mude a visibilidade para "Público".

Se o envio à Oficina terminou, mas apenas o envio para revisão falhou (por exemplo, por causa de um erro de rede), pressione **Enviar para a Oficina** novamente no mesmo mod. Se o conteúdo não mudou, o mod não é enviado à Oficina de novo; apenas o envio para revisão é refeito. O envio para revisão precisa ser feito com a conta Steam que criou o item da Oficina.

## Revisão de código

- O sistema de revisão descompila as DLLs e as verifica quanto a comunicação de rede, acesso a arquivos, ofuscação e comportamento malicioso. A revisão combina análise automatizada com verificações de operadores, e entendemos que nenhum sistema de revisão consegue detectar todos os riscos. Ainda assim, buscamos reduzir a disseminação de mods maliciosos com esse processo de revisão. Agradecemos a sua colaboração.
- Todo mod que contém código (DLLs) **também precisa ser enviado ao sistema de revisão de mods.** Para isso, sempre faça o envio pelo botão **Enviar para a Oficina** do jogo.
- Mods que foram enviados à Oficina, mas não ao sistema de revisão, em geral não podem ser executados pelos jogadores.
- Os jogadores que baixam um mod consultam o status de revisão dele usando o ID do item da Oficina e as DLLs instaladas.
  - **Enviado / Aprovado**: o mod é executado. Ele é executado mesmo antes de a revisão terminar.
  - **Não enviado / Rejeitado**: o mod não é executado, e o motivo aparece no painel de mods.
- Se um problema for encontrado durante a revisão, um mod pode ser rejeitado mesmo depois de publicado. No caso de um mod rejeitado, você pode ver o motivo em **Ver motivo da rejeição** no painel. As DLLs rejeitadas não podem ser publicadas novamente como estão; corrija o problema e envie de novo.
- Mesmo sem um módulo `dll`, um mod passa pela revisão se tiver `libraries` ou contiver uma DLL ou um EXE com a extensão alterada.
- Mods que passam pela revisão de código precisam ter 20 MB ou menos com a pasta compactada.

### Motivos de falha na revisão

|Mensagem|Causa|
|---|---|
|Não há registro de envio no sistema de revisão.|O mod foi enviado à Oficina, mas não foi enviado ao sistema de revisão. Envie-o novamente com o botão **Enviar para a Oficina**.|
|O sistema de revisão rejeitou a aprovação deste mod.|A equipe de operações rejeitou o mod por algum motivo. Verifique o motivo da rejeição.|
|Não foi possível obter informações do servidor de revisão.|A conexão com a internet de quem se inscreveu pode estar fora do ar, ou pode haver um problema no servidor de revisão.|
|Este mod é do sistema de revisão anterior.|O servidor de revisão foi atualizado e os dados de verificação existentes do mod foram redefinidos. Envie-o novamente.|
|Uma DLL não autorizada foi detectada no mod.|Se o conjunto de arquivos DLL do mod mudar enquanto o mod está em execução, o carregamento desse mod é restringido. Ou seja, o seguinte pode violar o sistema de revisão: o mod baixa uma DLL de fora durante a execução e carrega o assembly; o mod cria e executa a própria DLL durante a execução; o mod extrai e usa uma DLL empacotada dentro dele com um esquema de compressão próprio.|

## Visibilidade e publicação

- No primeiro envio, a visibilidade é definida como `Não listado`.
- Antes de tornar público, inscreva-se você mesmo, verifique se a cópia instalada funciona corretamente e depois mude para "Público".
- Você pode editar a descrição da página, as capturas de tela, as descrições em outros idiomas e assim por diante na página do item na Oficina Steam.

## Atualizando o seu mod

Você pode atualizar o seu mod da mesma forma que o publicou.

- Uma atualização que altera DLLs é reenviada automaticamente depois do upload e passa pela revisão de novo. Se você não alterou nenhuma DLL, o status da revisão continua o mesmo.
- Não mude o seu `modId`. O mod seria tratado como um mod diferente, o que pode quebrar dependências e muito mais.
- **Sempre aumente o `version` no `metadata.json` a cada atualização.** As versões dos mods são comparadas ao entrar em uma sala multijogador, então, se o conteúdo mudou mas a versão não, jogadores com conteúdos de mod diferentes podem acabar na mesma partida e ter problemas.

## Ticket de upload

Depois que você faz o upload, o `uploadticket.json` é adicionado à pasta do seu mod. Ele contém o ID do item da Oficina e as informações do envio para revisão, então **não o exclua.** Se você gerencia a pasta do seu mod com git, o mais seguro é fazer commit desse arquivo também. Se trocar de computador ou formatar, você vai precisar desse arquivo para continuar atualizando o mesmo item da Oficina. Sem ele, o jogo trata a pasta como um mod novo que nunca foi publicado.

O `mod-guard-manifest.json` e o `mod-guard.sig.json`, usados pelo sistema de revisão antes da 1.1.1, não são mais usados. Mesmo que continuem na pasta, eles não são enviados à Oficina, e você pode excluí-los.

Consulte o [FAQ](faq.md) para saber como recuperar o arquivo caso o perca.


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
