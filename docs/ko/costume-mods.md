# 코스튬 모드

코드 없이 PNG와 JSON만으로 코스튬을 추가하는 모드입니다. 두 가지를 만들 수 있습니다.

- **새 코스튬**: 코스튬 패널에 새 칸이 생깁니다. 외형, 스탯, 시작 아이템, 기본 무기를 정할 수 있습니다.
- **기존 코스튬의 스킨**: 분홍 토끼 같은 내장 코스튬에 외형만 하나 더 붙입니다. 스탯은 원래 코스튬 것을 따릅니다.

필요한 그림은 **가만히 있는 모습(idle)과 걷는 모습(move) 두 장**이 최소입니다. 더 그리면 더 좋아지고, 안 그린 동작은 있는 그림으로 대신합니다.


## 폴더 구조

```
my-costume-mod/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← costume 모듈 폴더
│   └── ForestCat/             ← 폴더 하나 = 코스튬 하나
│       ├── costume.json
│       ├── icon.png           ← 코스튬 패널 아이콘
│       ├── idle.png           ← 필수
│       ├── move.png           ← 필수
│       ├── idle_back.png      ← 선택
│       ├── move_back.png      ← 선택
│       └── attack.png         ← 선택
└── Localization/              ← 코스튬 이름·설명 글
    ├── ko-KR.json
    └── en-US.json
```

```json
{
  "info": {
    "title": { "ko-KR": "숲고양이 코스튬", "en-US": "Forest Cat Costume" },
    "description": { "ko-KR": "숲고양이 코스튬을 추가합니다.", "en-US": "Adds the Forest Cat costume." }
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.forestcat",
    "version": "1.0.0",
    "author": "Your Name",
    "modules": [
      { "type": "costume", "path": "Costumes" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

`multiplayer`는 적지 않아도 됩니다. 코스튬 모드는 자동으로 `everyone`(참가자 전원 설치)으로 처리됩니다.


## 그림 규칙

- **PNG, 가로 스트립.** 한 동작의 프레임을 가로로 이어 붙인 그림 한 장입니다. 그림 높이는 프레임 높이와 같고, 폭은 프레임 폭의 배수여야 합니다. 프레임 수는 폭 ÷ 프레임 폭으로 자동 계산됩니다.
- **프레임 크기는 기본 16×16.** 내장 코스튬이 전부 이 크기입니다. `frameSize`로 바꿀 수 있고 최대 256입니다.
- **pivot은 픽셀 단위, 왼쪽 아래 기준.** 캐릭터가 땅을 딛는 점입니다. 기본값은 `[프레임 폭/2, 0]`이고, 보통 `[8, 0]` 근처면 됩니다.
- 아이콘(`icon.png`)은 코스튬 패널에 보이는 그림입니다. 크기 제한은 없지만 내장 아이콘은 18×19 정도입니다.
- `face.png`는 파티 UI에 보이는 작은 얼굴입니다. 없으면 idle의 첫 프레임을 씁니다.


## costume.json

```json
{
  "schemaVersion": 1,
  "id": "forestcat",
  "name": "Costume_ForestCat_Name",
  "flavorText": "Costume_ForestCat_FlavorText",
  "icon": "icon.png",

  "frameSize": [16, 16],
  "pivot": [8, 0],

  "animations": {
    "idle":      { "file": "idle.png", "fps": 12 },
    "move":      { "file": "move.png", "fps": 12, "footstepFrames": [1, 4] },
    "idle_back": { "file": "idle_back.png" },
    "move_back": { "file": "move_back.png", "footstepFrames": [1, 4] },
    "attack":    { "file": "attack.png", "fps": 16 }
  },

  "stats": ["MOVE_SPEED/10", "MAX_MP/-5"],
  "startingItems": [],
  "defaultWeapon": 0,
  "onlyDefaultWeapon": false
}
```

| 필드 | 필수 | 내용 |
|---|---|---|
| `schemaVersion` | 예 | 정수 `1`. |
| `id` | 예 | 소문자 영문·숫자·밑줄·하이픈, 1~31자. 게임 안에서는 `<modId>:<id>`로 등록됩니다. |
| `name` | 예 | 코스튬 이름의 **번역 키**. 글이 아니라 키입니다. 아래 [이름과 설명](#이름과-설명) 참고. |
| `flavorText` | 아니오 | 설명의 번역 키. |
| `icon` | 예 | 아이콘 PNG 경로. |
| `face` | 아니오 | 파티 UI 얼굴 PNG 경로. |
| `frameSize` | 아니오 | `[가로, 세로]` 픽셀. 기본 `[16, 16]`. |
| `pivot` | 아니오 | `[x, y]` 픽셀, 왼쪽 아래 기준. 기본 `[가로/2, 0]`. |
| `animations` | 예 | 동작별 그림. `idle`, `move`는 필수. 아래 표 참고. |
| `stats` | 아니오 | `"스탯ID/값"` 문자열 배열. 예: `MOVE_SPEED/10`, `MAX_MP/8`, `PHYSICAL_DAMAGE_AMP/-20`, `STARTING_MONEY/800`. 없는 스탯 ID는 오류. |
| `startingItems` | 아니오 | 시작 아이템 번호 배열. 없는 번호는 오류. |
| `defaultWeapon` | 아니오 | 기본 무기 번호. |
| `onlyDefaultWeapon` | 아니오 | `true`면 기본 무기만 쓸 수 있음. `defaultWeapon`이 있어야 함. |
| `body` | 아니오 | 피격 판정과 충돌 크기. 기본값이 분홍 토끼와 같으니 몸집이 많이 다를 때만 손대세요. |
| `skins` | 아니오 | 같은 코스튬의 추가 외형. 아래 참고. |

`order`는 없습니다. 모드 코스튬은 내장 코스튬 뒤에 등록 순서대로 놓입니다.

### animations

키는 소문자 동작 이름이고, 값은 `{ "file", "fps", "loop", "footstepFrames" }` 객체 또는 파일 이름 문자열입니다. `fps` 기본 12, `loop`는 공격 계열만 기본 `false`입니다. `footstepFrames`는 걷기 계열에서 발소리를 낼 프레임 번호(0부터)입니다.

| 키 | 동작 | 없으면 |
|---|---|---|
| `idle`, `move` | 정면 대기, 정면 걷기 | **오류** |
| `idle_back`, `move_back` | 뒷모습 대기, 뒷모습 걷기 | 뒤돌아보기 없음. 둘 다 있어야 켜집니다. |
| `attack` | 공격 | 공격 동작 없음 |
| `attack_back` | 뒷모습 공격 | `attack`으로 대체 |
| `greatsword_lower`, `greatsword_upper`, `greatsword_sweep` | 대검 휘두르기 3종 | `attack`으로 대체 |
| `whirlwind_ready`, `whirlwind` | 회전베기 준비·회전 | `idle`·`attack`으로 대체 |
| `airborne`, `fall`, `hitfeedback` | 공중, 낙하, 피격 | `idle`로 대체 |

동작마다 `frameSize`와 `pivot`을 따로 줄 수도 있습니다. 공격 그림만 더 큰 경우에 씁니다.

### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

`hitboxRadius`와 `hitboxOffset`은 픽셀(피격 원의 반지름과 중심), `movementRadius`는 이동 충돌 반지름(유닛), `bodyScale`은 무기를 쥐는 어깨 위치 배율입니다. 전부 생략 가능합니다.

### skins

같은 코스튬에 외형을 더 붙입니다. 각 스킨은 자기 폴더에 같은 그림 규칙으로 들어갑니다.

```json
"skins": [
  { "id": "winter", "name": "Skin_ForestCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

`frameSize`·`pivot`을 생략하면 코스튬 본체 값을 따릅니다. 스킨은 외형만 바꾸고 스탯은 바꿀 수 없습니다.


## 기존 코스튬에 스킨 추가 (skin.json)

`costume.json` 대신 `skin.json`을 둔 폴더는 "기존 코스튬의 스킨"이 됩니다.

```
Costumes/
└── PinkRabbit_Pajama/
    ├── skin.json
    ├── icon.png
    ├── idle.png
    └── move.png
```

```json
{
  "schemaVersion": 1,
  "costume": "PinkRabbit",
  "id": "pajama",
  "name": "Skin_PinkRabbit_Pajama_Name",
  "icon": "icon.png",
  "animations": { "idle": "idle.png", "move": "move.png" }
}
```

- `costume`은 대상 코스튬의 id입니다. 내장 코스튬 id(`PinkRabbit` 등) 또는 다른 모드 코스튬의 런타임 id(`someone.forestcat:forestcat`)를 적습니다.
- `stats`, `startingItems`, `defaultWeapon`, `onlyDefaultWeapon`, `body`, `skins`, `flavorText`는 쓸 수 없습니다. 전부 대상 코스튬 것을 따릅니다.
- 내장 코스튬의 기본 외형을 **덮어쓰는 기능은 없습니다.** 스킨으로 추가하면 사용자가 패널에서 고릅니다.


## 이름과 설명

`name`과 `flavorText`에는 글 대신 **번역 키**를 적고, 글은 같은 패키지의 `Localization/` 폴더에 언어별로 둡니다. 내장 코스튬과 같은 방식이라 번역자는 언어 파일 하나만 보면 됩니다.

`Localization/ko-KR.json`:

```json
{
  "Costume_ForestCat_Name": "숲고양이",
  "Costume_ForestCat_FlavorText": "숲에서 온 손님."
}
```

`Localization/en-US.json`:

```json
{
  "Costume_ForestCat_Name": "Forest Cat",
  "Costume_ForestCat_FlavorText": "A visitor from the woods."
}
```

- 키는 영문·숫자·밑줄·점·콜론·하이픈, 공백 없이. 다른 모드와 겹치지 않게 모드 이름을 섞어 지으세요.
- 키가 **어느 언어 파일에도 없으면** 그 코스튬은 오류로 빠집니다. 최소 한 언어는 넣어야 하고, 영어(`en-US`)를 같이 넣는 것을 권장합니다. 없는 언어는 영어로 폴백됩니다.
- `metadata.json`의 `modules`에 `localization` 모듈을 **함께 선언**해야 언어 파일이 읽힙니다. 위 예시처럼 두 모듈을 같이 둡니다.


## 확인하기

1. 모드 폴더를 `WorkshopLocal/`에 두고 게임을 실행합니다.
2. 게임을 시작해 마을의 코스튬 패널을 열면 내장 코스튬 뒤에 새 코스튬이 보입니다. 스킨 추가 모드라면 대상 코스튬의 스킨 목록에 보입니다.
3. 그림을 고친 뒤에는 게임을 재시작하세요. 실행 중에 다시 읽는 기능은 아직 없습니다.

한 코스튬에 오류가 있으면 그 코스튬만 빠지고 같은 패키지의 다른 코스튬은 적용됩니다. 흔한 오류는 다음과 같습니다.

- 스트립 폭이 프레임 폭의 배수가 아님. 프레임 사이에 여백을 넣지 마세요.
- `idle` 또는 `move`가 없음.
- `name` 키가 언어 파일에 없거나, `localization` 모듈을 선언하지 않음.
- 스탯 ID 오타. 대문자와 밑줄만 쓰고, 값은 `/` 뒤에 정수.


## 멀티플레이

코스튬 id와 스탯이 모든 참가자에게 공유되므로, 코스튬 모드는 **방 참가자 전원이 켜야** 합니다. 호스트가 켜면 방 구성에 들어가고, 참가자는 같은 모드를 구독·활성화한 뒤 입장합니다. 자세한 규칙은 [게시와 멀티플레이](publishing.md)를 보세요.


## 예제

내장 코스튬을 그대로 내보낸 예제 패키지가 [`examples/`](../../examples)에 추가될 예정입니다. 되는 것을 복사해서 그림만 바꾸는 것이 가장 빠른 시작입니다.
