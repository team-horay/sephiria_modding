# Mods de localização

[Seu primeiro mod](getting-started.md) cobriu o básico da criação de um mod, então esta página trata das regras comuns aos mods de localização.
[`examples/translation-sample`](../../examples/translation-sample) contém um exemplo completo que você pode copiar diretamente para `WorkshopLocal/` e experimentar.



## Como funciona

- O jogo tem uma tabela **chave → texto** por idioma. Quando um mod é carregado, os valores do mod sobrescrevem a tabela original.
- Seu arquivo só precisa conter as chaves que você quer alterar.
- O nome do arquivo é o idioma. `ko-KR.json` sobrescreve a tabela de coreano, e `en-US.json` sobrescreve a tabela de inglês.
- Se vários mods de localização definirem a mesma chave, o mod carregado por último vence.
- Um mod que só altera texto não afeta os outros participantes, então recomendamos definir `multiplayer.installation` no `metadata.json` como `local_only`.
- Tanto as chaves quanto os valores são strings. Objetos aninhados e arrays não são permitidos.
- Salve os arquivos em **UTF-8**.

```
my-translation-mod/
├── metadata.json              ← adicione { "type": "localization", "path": "Localization" } em modules
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## O que não traduzir

Os textos do jogo contêm expressões que são interpretadas em tempo de execução.
Não traduza os textos abaixo; mantenha-os em inglês exatamente como estão (diferenciando maiúsculas de minúsculas).

| Tipo    | Exemplo                                                                     | Descrição                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Palavras-chave   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Substituídas por números ou nomes em tempo de execução. Não altere nenhum caractere entre as chaves `{ }`.   |
| Tags do jogo | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Substituídas por dicas de palavras-chave e ícones. Os nomes dentro das tags são fixos, inclusive as maiúsculas e minúsculas. |
| Tags de formatação | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Formatação de texto. As tags de abertura e fechamento precisam estar em pares.        |
| Quebras de linha   | `\n`                                                                        | Uma quebra de linha dentro de uma string JSON.                        |




## Adicionando um novo idioma

- Você também pode adicionar um idioma que o jogo não suporta. Se o nome do arquivo for um código de idioma novo, o jogo o registra como um novo idioma e o exibe na lista de configurações de idioma.
- Os arquivos de idioma originais ficam em `<pasta de instalação do jogo>/Sephiria_Data/StreamingAssets/Localization/`, um por idioma; use-os como referência ao traduzir.
- Os idiomas que o jogo já suporta são `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH` e `tr-TR`.
- **Traduza todas as chaves.** Chaves ausentes no arquivo do seu mod não aparecem em branco; elas voltam para o **inglês** (ou para o coreano, se o inglês também estiver ausente). O jeito mais fácil de começar é copiar o `en-US.json` original.
- **Preencha as chaves de configuração que começam com `#`.** `#LanguageName` é o nome exibido na lista de configurações de idioma, e `#FontName` é a fonte a ser usada. Ajuste as chaves de pontuação, como `#Colon`, `#Bullet` e `#ParenthesisOpen`, às convenções daquele idioma.
- Em `#FontName`, você pode escolher entre as fontes embutidas no jogo. O jogo inclui fontes para os seguintes sistemas de escrita:

| Sistema de escrita | `#FontName` |
|---|---|
| Latino, cirílico, hangul | `GalCyrillic` |
| Japonês | `JF-Dot-MPlus10` |
| Chinês simplificado | `Fusion_Pixel_10_Simple` |
| Chinês tradicional | `BoutiqueBitmap9x9` |
| Tailandês | `NotoSansThai` |



## Solução de problemas

| Sintoma | O que verificar |
|---|---|
| O mod está ativado, mas o texto não muda | Se o nome da chave corresponde exatamente ao original, caractere por caractere. Se o nome do arquivo corresponde ao idioma atual do jogo (`ko-KR.json` só se aplica em coreano). |
| Só algumas chaves não mudam | Se outro mod de localização está sobrescrevendo a mesma chave depois. Tente mudar a ordem no painel. |
| `{VALUE}` aparece literalmente no texto | A palavra-chave está escrita de forma diferente do original. Copie-a diretamente do texto original. |
| `<tag=…>` aparece literalmente no texto | A grafia ou as maiúsculas e minúsculas do nome da tag estão diferentes. |
| Textos em coreano (ou em outro idioma não inglês) aparecem corrompidos | O arquivo não está em UTF-8. Salve-o novamente em UTF-8. |
| Os caracteres aparecem como quadrados | Esses caracteres não estão incluídos nas fontes embutidas do jogo. |
| Um arquivo inteiro é ignorado | Esse arquivo tem um erro de sintaxe JSON. O `Player.log` registra o erro junto com o nome do arquivo. |

O arquivo de log fica em `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`. Procure por "모드 내 언어 파일 발견" (arquivo de idioma encontrado no mod) para ver quais arquivos foram lidos e quantas chaves cada um tinha.
