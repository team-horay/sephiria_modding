# metadata.json 레퍼런스

모드 폴더 루트의 `metadata.json`은 모드가 **무엇인지**(게시 정보)와 **어떻게 실행되는지**(실행 명세)를 함께 담는 유일한 파일입니다. 게임은 이 파일만 보고 모드를 인식하고, 모듈을 적용하고, 다른 모드와의 의존 관계를 풀고, 창작마당 업로드 정보를 만듭니다.

이 문서는 **스키마 버전 1**을 다룹니다. 게임이 실제로 검증하는 규칙을 그대로 옮긴 것이므로, 여기 적힌 규칙에 어긋나면 모드는 로드되지 않습니다.

## 전체 구조

```json
{
  "info": {
    "title": "새 아티팩트 팩",
    "description": "아티팩트 12종을 추가합니다.",
    "preview": "thumbnail.png"
  },
  "tags": ["Content"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone", "support": "untested" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ],
    "libraries": [
      "bin/Helper.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
    ]
  }
}
```

최상위에는 세 가지가 옵니다.

| 키 | 역할 | 필수 |
|---|---|---|
| `info` | 창작마당과 게임 내 패널에 보이는 제목·설명·미리보기 | 권장 |
| `tags` | 창작마당 태그 | 선택 |
| `mod` | 실행 명세. 이게 없으면 게임은 아무것도 적용하지 않습니다 | **필수** |

알 수 없는 키는 무시됩니다. 미래 버전을 위해 필드를 미리 넣어 두어도 오류가 나지는 않지만, 오타가 조용히 묻힐 수 있으니 주의하세요.

## `info` — 게시 정보

| 필드 | 타입 | 설명 |
|---|---|---|
| `title` | 문자열 또는 언어 맵 | 모드 이름. 비어 있으면 `modId`가 대신 표시됩니다 |
| `description` | 문자열 또는 언어 맵 | 모드 설명 |
| `preview` | 문자열 | 미리보기 이미지의 상대 경로. 생략하면 루트의 `thumbnail.png`, `preview.png`, `thumbnail.jpg`, `preview.jpg`를 순서대로 찾습니다 |

### 제목과 설명을 여러 언어로 적기

`title`과 `description`은 문자열 하나로 적어도 되고, 언어 코드를 키로 하는 맵으로 적어도 됩니다.

```json
"info": {
  "title": { "ko-KR": "새 아티팩트 팩", "en-US": "New Artifact Pack" },
  "description": { "default": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

- 키는 게임 언어 코드(`ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`) 또는 `default`입니다. `ko`처럼 앞 두 글자만 적어도 됩니다.
- **게임 내 패널**은 현재 게임 언어 → `default` → `en-US` → 첫 항목 순으로 고릅니다.
- **창작마당 페이지**에는 `default` → `en-US` → 첫 항목 순으로 고른 **한 언어만** 올라갑니다. 다른 언어 설명은 창작마당 페이지에서 직접 추가하세요.
- 영어(`en-US`)를 하나 넣어 두는 것을 권장합니다. `default`도 `en-US`도 없으면 업로드 전 검사가 경고를 냅니다.
- 문자열이 아닌 값, 빈 값, 빈 키는 경고 후 버려집니다. 언어 코드 꼴이 아닌 키(`korean` 등)는 어떤 언어에도 직접 맞지 않고 폴백 후보로만 쓰입니다.

### 미리보기 이미지

창작마당 업로드에는 미리보기 이미지가 **필수**입니다. PNG 또는 JPG, 1MB 미만이어야 합니다. 게임 내 패널은 패키지 루트의 `thumbnail.png`만 표시하므로, 특별한 이유가 없으면 그 이름을 쓰세요.

## `tags` — 창작마당 태그

문자열 배열입니다. 창작마당 아이템의 태그로 그대로 올라갑니다. 쉼표는 쓸 수 없고 한 태그는 255자 이하입니다.

모듈 종류에 따라 다음 태그가 **자동으로 추가**되므로 직접 적지 않아도 됩니다.

| 모듈 | 자동 태그 |
|---|---|
| `dll` | `DLL` |
| `localization` | `Localization` |
| `costume` | `Costume` |

## `mod` — 실행 명세

### 식별자와 버전

| 필드 | 타입 | 필수 | 규칙 |
|---|---|---|---|
| `schemaVersion` | 정수 | 예 | 반드시 `1`. 문자열 `"1"`은 오류, `2` 이상은 "이 게임 버전이 지원하지 않음"으로 거부 |
| `modId` | 문자열 | 예 | 소문자 영문·숫자·점·하이픈·밑줄, 2~64자, 양끝은 영문·숫자. 예: `yourname.artifact-pack` |
| `version` | 문자열 | 예 | 공백·제어문자 없는 1~64자. `1.0.0` 같은 시맨틱 버전을 권장 |
| `author` | 문자열 | 아니오 | 제작자 표시 이름 |

**`modId`는 한 번 게시하면 바꾸지 마세요.** 다른 모드가 `dependencies`에서 이 이름으로 당신의 모드를 가리키고, 멀티플레이 방 정보와 사용자의 활성 모드 목록도 이 이름으로 저장됩니다. 제목(`info.title`)은 자유롭게 바꿔도 됩니다.

같은 `modId`를 가진 패키지가 두 개 이상 설치되어 있으면 게임은 어느 하나를 고르지 않고 **전부 제외**합니다. 예외는 하나뿐입니다. 제작자가 `WorkshopLocal/`에 개발 중인 사본을 두고 창작마당에 올린 자기 모드도 구독한 경우, 로컬 사본이 이기고 창작마당 사본은 쉽니다.

### `multiplayer` — 멀티플레이 정책

```json
"multiplayer": { "installation": "everyone", "support": "untested" }
```

| 필드 | 값 | 의미 |
|---|---|---|
| `installation` | `local_only` | 내 화면·UI에만 영향. 공유 게임 상태, 아이템 ID, 통신 내용을 바꾸지 않음. 방 참가자에게 설치를 요구하지 않음 |
| | `everyone` | 방 참가자 전원이 같은 모드를 켜야 함. 콘텐츠를 추가하거나 공유 상태를 바꾸는 모드는 전부 여기 |
| | `host_only` | 호스트만 켜면 되고 다른 참가자 쪽 변경 없이 동작함을 **구현·검증까지 마친** 경우에만 |
| | `unknown` | 미선언 |
| `support` | `supported` | 멀티플레이에서 동작을 확인함 |
| | `untested` | 확인하지 않음 |
| | `unsupported` | 멀티플레이에서 동작하지 않음 |

`support`는 **제작자의 자기 선언**입니다. 개발사가 검증한 것도, 게임이 자동으로 맞춰 주는 것도 아닙니다.

생략하면 다음과 같이 정규화되고 경고가 남습니다.

| 패키지 구성 | 기본 `installation` | 기본 `support` |
|---|---|---|
| `costume` 모듈이 있고 `dll` 모듈이 없음 | `everyone` | `supported` (경고 없음) |
| `data` 모듈만 있음 | `everyone` | `untested` |
| 그 외 | `unknown` | `untested` |

알 수 없는 값(오타 포함)은 `unknown` / `untested`로 정규화되고 경고가 남습니다. 거부되지는 않지만 의도한 정책이 적용되지 않으니 로그를 확인하세요.

멀티플레이에서 어떤 모드가 방 정보에 실리고 참가자에게 요구되는지는 [게시와 멀티플레이](publishing.md)를 보세요. `local_only` 모드는 방 구성 비교에서 제외됩니다.

### `modules` — 모듈 목록

**최소 1개 필수.** 배열이고, 각 항목은 `type`과 `path`를 반드시 가집니다. 한 패키지에 여러 모듈을 섞어 넣을 수 있습니다.

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "localization", "path": "Localization" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

| `type` | `path`가 가리키는 것 | 추가 필드 | 상태 |
|---|---|---|---|
| `dll` | `.dll` **파일** | `entryClass` (필수) | 사용 가능 |
| `localization` | 언어 JSON이 든 **폴더** | 없음 | 사용 가능 |
| `costume` | 코스튬 정의가 든 **폴더** | 없음 | 준비 중 |
| `data` | 다른 모드가 읽는 데이터 **폴더** | `consumer` (선택) | 준비 중 |

규칙:

- 같은 파일이나 폴더를 두 번 선언하면 오류입니다.
- `entryClass`는 `dll`에만 둡니다. 다른 타입에 있으면 오류입니다.
- 알 수 없는 `type`이 하나라도 있으면 패키지 전체가 "지원하지 않음"으로 분류되어 **다른 모듈도 실행되지 않습니다.** 미래 타입을 미리 적어 두면 안 됩니다.
- 모듈 배열의 순서는 **같은 종류끼리만** 지켜집니다. `dll` 모듈은 게임 데이터 초기화 직전에, `localization` 모듈은 언어 데이터 초기화 때 적용되므로 둘 사이의 순서는 보장되지 않습니다.

#### `dll` 모듈

```json
{ "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
```

- `path`는 `.dll`로 끝나야 합니다.
- `entryClass`는 네임스페이스를 포함한 클래스 전체 이름입니다. 중첩 클래스는 `Ns.Outer+Inner`로 적습니다. 게임은 DLL 안을 뒤져서 진입점을 찾지 않으므로 **반드시 명시**해야 합니다.
- 같은 `entryClass`를 두 모듈에 쓰면 오류입니다.
- 진입 클래스는 `WorkshopModBase`를 상속해야 합니다. 자세한 내용은 [DLL 모드](dll-mods.md)를 보세요.
- Mono 빌드에서만 동작합니다. IL2CPP 플랫폼에서는 이 모듈이 건너뛰어집니다.

#### `localization` 모듈

```json
{ "type": "localization", "path": "Localization" }
```

- `path`는 폴더여야 하고, 그 안에 `ko-KR.json`, `en-US.json`처럼 **언어 코드 이름의 JSON 파일**을 둡니다. 게임이 쓰는 언어 코드와 파일 형식은 [번역 모드](localization-mods.md)를 보세요.
- 폴더 안의 `*.json`을 전부 읽어 게임 텍스트에 **키 단위로 덮어씁니다.** 파일 하나가 깨져도 다른 파일은 적용되고, 오류는 모듈 결과에 기록됩니다.
- 활성화 순서상 뒤에 오는 모드가 같은 키를 덮어씁니다.

#### `costume` 모듈 (준비 중)

코스튬 JSON과 스프라이트를 담은 폴더를 가리킵니다. 게임 업데이트로 배포된 뒤 별도 문서를 추가합니다. 그 전까지는 사용하지 마세요.

#### `data` 모듈 (준비 중)

게임이 내용을 해석하지 않는 범용 데이터 폴더입니다. 다른 모드(`consumer`)가 읽어서 콘텐츠로 바꿉니다.

- `consumer`를 생략하면 **이 패키지 자신과 `dependencies`에 선언된 모드 전부**가 읽을 수 있습니다.
- `consumer`를 명시하면 그 모드 하나만 읽습니다. 값은 자기 자신의 `modId`이거나 `dependencies`에 선언된 `modId`여야 합니다. 그렇지 않으면 오류입니다.

### `libraries` — 보조 DLL

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

`dll` 모듈이 참조하는 보조 어셈블리 목록입니다. 문자열 또는 `{ "path": "..." }` 객체 둘 다 됩니다.

- 각 항목은 `.dll`로 끝나야 하고, `modules`의 DLL과 겹치면 오류입니다.
- 게임은 **여기 선언된 파일만** 참조 해석에 씁니다. 폴더에 DLL을 넣어 두기만 하면 찾지 못합니다.
- 라이브러리는 진입점으로 취급되지 않습니다. `WorkshopModBase` 클래스가 들어 있어도 호출되지 않습니다.

### `dependencies` — 다른 모드에 대한 의존

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

| 필드 | 필수 | 규칙 |
|---|---|---|
| `modId` | 예 | `modId` 규칙과 같음. 자기 자신, 중복 선언은 오류 |
| `version` | 아니오 | **정확히 일치하는 버전 문자열만.** `>=2.5.3`, `^2.0`, `2.*` 같은 범위 표기는 오류로 거부됩니다. 생략하거나 빈 문자열이면 버전 무관 |
| `workshopId` | 아니오 | 창작마당 아이템 ID. 0보다 큰 십진수(문자열 또는 정수) |

의존성은 **전부 필수**입니다. 선택적 의존은 v1에 없습니다.

- 의존하는 모드가 설치되어 있지 않거나, 꺼져 있거나, 자기 자신이 실패했으면 이 모드도 **연쇄로 제외**됩니다.
- `version`을 적었는데 설치된 모드의 버전이 다르면 제외됩니다. 의존 대상이 자주 업데이트된다면 `version`을 생략하는 편이 안전합니다.
- 순환 의존(A → B → A)은 관련된 모드 전부가 제외됩니다.
- `workshopId`는 **출처 힌트**일 뿐입니다. 실제 판정은 `modId`와 `version`으로 하며, 설치된 패키지의 창작마당 ID가 힌트와 달라도 경고만 남기고 통과합니다. 사용자가 의존 모드를 찾아 구독할 수 있도록 적어 두는 용도입니다.
- 의존 모드는 항상 이 모드보다 **먼저** 활성화됩니다. 의존 관계 밖의 모드끼리는 `modId` 알파벳순으로 고정됩니다.

## 경로 규칙

`modules[].path`, `libraries[]`, `info.preview`에 적는 경로는 전부 **패키지 루트 기준 상대 경로**이고, 다음은 모두 거부됩니다.

- 절대 경로, 드라이브 문자(`C:`), 맨 앞 `/`
- `.` 또는 `..` 세그먼트, 빈 세그먼트(`a//b`)
- 패키지 폴더 밖으로 나가는 경로
- 존재하지 않는 파일·폴더
- 파일이어야 할 자리에 폴더, 폴더여야 할 자리에 파일
- 심볼릭 링크나 정션을 거치는 경로
- **실제 파일명과 대소문자가 다른 경로.** Windows에서는 동작하는 것처럼 보여도 거부됩니다. `bin/helper.dll`과 `bin/Helper.dll`은 다른 경로입니다.

구분자는 `/`를 쓰세요. `\`를 써도 `/`로 정규화되지만 JSON에서는 `\\`로 이스케이프해야 해서 실수하기 쉽습니다.

## 게임이 매니페스트를 분류하는 방식

게임은 설치된 패키지마다 `metadata.json`을 읽어 다음 넷 중 하나로 분류합니다. 분류 결과와 사유는 타이틀 화면의 창작마당 패널과 로그에서 볼 수 있습니다.

| 분류 | 조건 | 결과 |
|---|---|---|
| **없음** (NoManifest) | `metadata.json`이 없거나 `mod` 키가 없음 | 아무것도 적용하지 않음. 패널에 제목만 표시 |
| **유효** (Valid) | 모든 규칙 통과 | 로딩 계획에 들어감 |
| **지원 안 함** (Unsupported) | 구조는 맞지만 `schemaVersion`이 1이 아니거나 알 수 없는 모듈 `type`이 있음 | 적용하지 않음. 보통 게임이 모드보다 오래된 경우 |
| **잘못됨** (Invalid) | JSON 손상, `mod`가 객체가 아님, 필수 필드 누락, 타입 불일치, 경로 규칙 위반, 중복 | 적용하지 않음. 패널에 오류 사유 표시 |

**유효**로 분류되어도 다음 이유로 로딩 계획에서 빠질 수 있습니다.

- 같은 `modId`의 다른 패키지가 있음
- 의존 모드가 없거나, 버전이 다르거나, 순환함, 또는 의존 모드가 실패함
- 사용자가 패널에서 꺼 둠
- 창작마당에서 받은 `dll` 패키지인데 검수 승인 증명이 없거나 승인본과 내용이 다름. [게시와 멀티플레이](publishing.md) 참고

검증은 패키지 단위로 격리됩니다. 한 모드의 `metadata.json`이 깨져도 다른 모드에는 영향이 없습니다.

## 예제

### 번역 전용

```json
{
  "info": {
    "title": { "ko-KR": "한국어 오탈자 수정", "en-US": "Korean Typo Fixes" },
    "description": { "ko-KR": "한국어 텍스트의 오탈자를 고칩니다.", "en-US": "Fixes typos in the Korean text." }
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.ko-typo-fixes",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only", "support": "supported" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

번역만 바꾸는 모드는 다른 참가자에게 영향이 없으므로 `local_only`가 맞습니다.

### DLL 모드와 자체 번역

```json
{
  "info": {
    "title": { "ko-KR": "새 아티팩트 팩", "en-US": "New Artifact Pack" },
    "description": { "default": "Adds 12 new artifacts." }
  },
  "tags": ["Content"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone", "support": "untested" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

아이템을 추가하는 모드는 방 참가자 전원이 가져야 하므로 `everyone`입니다. 아이템 이름·설명 번역을 `localization` 모듈로 같이 들고 다닙니다.

### 공용 런타임에 의존하는 모드

```json
{
  "info": { "title": "Someone's Boss Pack" },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.boss-pack",
    "version": "1.2.0",
    "multiplayer": { "installation": "everyone", "support": "untested" },
    "modules": [
      { "type": "dll", "path": "bin/BossPack.dll", "entryClass": "BossPack.Entry" }
    ],
    "libraries": [
      "bin/BossPack.Shared.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "workshopId": "123456789" }
    ]
  }
}
```

`version`을 생략해 공용 런타임이 업데이트되어도 깨지지 않게 했습니다. 런타임의 API가 바뀌어 특정 버전이 꼭 필요하다면 그때 정확한 버전을 적으세요.

## 자주 하는 실수

| 증상 | 원인 |
|---|---|
| 패널에 모드가 보이지만 아무것도 적용되지 않음 | `mod` 키가 없음(없음으로 분류). 또는 사용자가 꺼 둠 |
| "schemaVersion은 정수여야 함" | `"schemaVersion": "1"`처럼 따옴표를 씀 |
| "대소문자가 실제 파일명과 다름" | `path`의 대소문자가 실제 파일과 다름. 탐색기가 보여 주는 이름과 똑같이 적을 것 |
| "폴더여야 하는데 파일임" | `localization` 모듈의 `path`에 `.json` 파일을 직접 적음. 폴더를 적어야 함 |
| "version '>=1.0'은 지원하지 않는 형식" | `dependencies[].version`에 범위 표기를 씀. 정확한 버전 하나만 가능 |
| "알 수 없는 모듈 type" | 오타, 또는 게임이 아직 지원하지 않는 모듈 타입을 적음 |
| 모드 두 개가 둘 다 제외됨 | 같은 `modId`를 씀. 템플릿을 복사한 뒤 `modId`를 바꾸지 않은 경우가 대부분 |
| `multiplayer` 경고가 로그에 남음 | `installation`·`support` 값 오타. 거부되진 않지만 `unknown`·`untested`로 처리됨 |
