# Seu primeiro mod

Este guia explica a estrutura básica de um mod de Sephiria e mostra, passo a passo, como criar seu primeiro mod e testá-lo dentro do jogo.

## O que você precisa

- Um bom editor de texto (como o [Visual Studio Code](https://code.visualstudio.com/))

## Encontre a pasta de instalação do jogo

Abra a pasta de instalação de Sephiria.

- Pela sua biblioteca Steam
  - `Biblioteca Steam → clique com o botão direito em Sephiria → Gerenciar → Procurar arquivos locais`
- Windows (caminho de instalação padrão)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (caminho de instalação padrão)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (a pasta que contém `Sephiria.app`)

## Crie a pasta WorkshopLocal e a pasta do seu mod

1. Crie uma pasta `WorkshopLocal/` dentro da pasta de instalação do jogo.
2. Dentro dela, crie uma pasta para o seu mod.
   - Sephiria trata cada pasta como um mod.
   - Os mods criados aqui podem ser testados no jogo e enviados à Oficina Steam.

Neste guia, você vai criar o `my-first-mod`, um mod que troca o texto do botão "Iniciar Jogo" da tela de título por "Hello World!". A estrutura final fica assim:

```
<pasta de instalação do jogo>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← um mod
    │   ├── metadata.json      ← o arquivo central que define o mod
    │   ├── thumbnail.png      ← miniatura de pré-visualização do mod (obrigatória para enviar à Oficina)
    │   └── Localization/      ← pasta lida pelo módulo de localização
    │       └── en-US.json     ← o texto a ser substituído
    └── another-mod/           ← outro mod
        └── ...
```

## O arquivo metadata.json

- Crie um `metadata.json` na pasta do seu mod e cole o conteúdo abaixo.
- `metadata.json`: o arquivo principal que define o seu mod.
- `yourname`: substitua pelo seu próprio apelido.
- Salve o arquivo em **UTF-8**. No Bloco de Notas, escolha UTF-8 como codificação em "Salvar como".
- Consulte a [Referência do metadata.json](metadata.md) para saber o que cada campo significa.

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

## Crie o arquivo de idioma

- Crie uma pasta `Localization/` dentro da pasta do seu mod.
- Dentro de `Localization/`, crie um arquivo `en-US.json` com o conteúdo abaixo.
- Para testar, você também pode renomear o arquivo com o código do seu próprio idioma.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Teste

Neste ponto, você já pode verificar se o mod funciona.

1. Inicie Sephiria.
2. Com o idioma do jogo definido como inglês, olhe para a tela de título: o botão "Start Game" agora mostra "Hello World!". Os mods em `WorkshopLocal/` vêm ativados por padrão, então você não precisa ativá-los.
3. Pressione o botão **Oficina da Steam** na tela de título para abrir o painel da Oficina. "My First Mod" deve aparecer na seção "Meus mods".

### Meu mod não é aplicado!

- Verifique se a sintaxe do JSON está correta.
- O `path` no `metadata.json` precisa corresponder exatamente ao nome real da pasta, inclusive em maiúsculas e minúsculas.
- Verifique se o `metadata.json` está codificado em UTF-8.
- Verifique se o idioma do jogo corresponde ao idioma do nome do arquivo.

## Próximos passos

1. Aprenda como os mods são definidos na [Referência do metadata.json](metadata.md).
2. [Mods de localização](localization-mods.md) e [Mods de traje](costume-mods.md) são relativamente fáceis de criar, então comece por eles.
3. [Mods em DLL](dll-mods.md) são recomendados se você tem conhecimentos básicos de programação.
4. Lance seu mod na Oficina com [Publicando na Oficina](publishing.md).
