# 번역 모드

[첫 모드 만들기](getting-started.md)에서 기초적인 모드 실습을 다루었으니, 여기에선 번역 모드와 관련한 공통 규칙을 안내합니다.
[`examples/translation-sample`](../../examples/translation-sample)에 그대로 `WorkshopLocal/`에 복사해 볼 수 있는 완성 예제가 있습니다.



## 기본 원리

- 게임은 언어마다 **키 → 문자열** 테이블을 하나씩 가지고 있고, 모드를 불러올 때 원본 위에 덮어씁니다.
- 파일에는 바꾸고 싶은 키만 넣으면 됩니다.
- 파일 이름이 곧 언어입니다. `ko-KR.json`은 한국어, `en-US.json`은 영어 테이블에 덮어씌워집니다.
- 번역 모드가 여럿이면 로드 순서대로 덮어쓰므로, 같은 키는 나중에 로드되는 모드가 이깁니다.
- 번역만 바꾸는 모드는 다른 참가자에게 영향이 없으므로 `installation`은 `local_only`를 권장합니다.
- 키와 값 모두 문자열입니다. 중첩 객체나 배열은 쓸 수 없습니다.
- 파일은 **UTF-8**로 저장하세요.

```
my-translation-mod/
├── metadata.json              ← modules에 { "type": "localization", "path": "Localization" }를 기입
├── thumbnail.png
└── Localization/
    ├── ko-KR.json
```



## 번역하지 말아야 할 것

게임 텍스트에는 실행 중에 해석되는 표현들이 존재합니다. 
아래의 텍스트는 번역하지 말고 영문(대소문자 구분) 그대로 넣으세요.

| 종류 | 예 | 설명 |
|---|---|---|
| 키워드 | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | 실행 중 숫자나 이름으로 바뀝니다. 중괄호 안은 한 글자도 바꾸지 마세요. |
| 게임 태그 | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>` | 키워드 툴팁과 아이콘으로 바뀝니다. 태그 안의 이름은 대소문자까지 고정입니다. |
| 서식 태그 | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>` | 텍스트 서식입니다. 여는 태그와 닫는 태그를 짝 맞춰 두세요. |
| 줄바꿈 | `\n` | JSON 문자열 안에서 줄바꿈입니다. |




## 새 언어 추가

- 지원하지 않는 언어를 새로 추가할 수도 있습니다. 파일 이름이 새로운 언어 코드라면 게임은 그것을 새 언어로 등록하고 언어 설정 목록에 보여 줍니다.
- 원본 언어 파일은 `<게임 설치 폴더>/Sephiria_Data/StreamingAssets/Localization/`에 언어별로 있으니 번역에 참고하시기 바랍니다.
- 게임이 이미 지원하는 언어는 `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`입니다.
- **전체 키를 번역**하세요. 모드 파일에 없는 키는 빈 값이 아니라 **영어**(영어도 없으면 한국어)로 표시됩니다. 원본 `en-US.json`을 복사해 시작하는 것이 가장 쉽습니다.
- **`#`로 시작하는 설정 키**를 채우세요. `#LanguageName`은 언어 설정 목록에 보이는 이름, `#FontName`은 사용할 글꼴 이름입니다. `#Colon`, `#Bullet`, `#ParenthesisOpen` 같은 구두점 키도 그 언어 관습에 맞게 바꿉니다.
- `#FontName`은 게임에 내장된 글꼴 중에서 고를 수 있습니다. 게임에는 다음과 같은 문자 체계가 내장되어 있습니다.

| 문자 체계 | `#FontName` |
|---|---|
| 라틴 문자, 키릴 문자, 한글 | `GalCyrillic` |
| 일본어 | `JF-Dot-MPlus10` |
| 중국어 간체 | `Fusion_Pixel_10_Simple` |
| 중국어 번체 | `BoutiqueBitmap9x9` |
| 태국 문자 | `NotoSansThai` |



## 문제 해결

| 증상 | 확인할 것 |
|---|---|
| 모드는 켜졌는데 텍스트가 안 바뀜 | 키 이름이 원본과 글자 하나까지 같은지. 파일 이름이 현재 게임 언어와 같은지(`ko-KR.json`은 한국어에서만 적용). |
| 일부 키만 안 바뀜 | 다른 번역 모드가 같은 키를 뒤에서 덮어쓰고 있는지. 패널에서 순서를 바꿔 보세요. |
| 텍스트에 `{VALUE}`가 그대로 보임 | 키워드 철자가 원문과 다름. 원문에서 그대로 복사하세요. |
| 텍스트에 `<tag=…>`가 그대로 보임 | 태그 이름 철자나 대소문자가 다름. |
| 한글이 깨져 보임 | 파일이 UTF-8이 아님. UTF-8로 다시 저장하세요. |
| 폰트가 네모로 보임 | 게임에 내장되어 있지 않은 문자입니다. |
| 파일 하나가 통째로 무시됨 | 그 파일의 JSON 문법 오류. `Player.log`에 파일 이름과 함께 오류가 남습니다. |

로그 파일은 `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`에 있습니다. "모드 내 언어 파일 발견"으로 검색하면 어떤 파일이 몇 개의 키로 읽혔는지 보입니다.


## 예제
