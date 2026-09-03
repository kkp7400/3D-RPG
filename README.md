# 3D-RPG

Unity 3D 액션 RPG. 저장소 설명 자체가 "3D 개인 포트폴리오"이며, 2021년 학원 재학 시기 공개 저장소 중 개발 기간이 가장 긴(약 3개월) 프로젝트입니다. 플레이어 FSM, 2페이즈 보스전, CSV 데이터 파이프라인, 인벤토리/장비/상점 UI를 갖춘 플레이 가능한 프로토타입입니다.

`2021 · Unity 2020.3.11f1 · C# / ShaderLab · 개인 포트폴리오(majority solo) · 2021-07-01 ~ 09-27(~3개월)`

> GitHub Contributors API 실측: `kkp7400 23 / ParkByeongGu 2 / wing7sing 2 / gds1110 2 / namsangmin 1` (총 30커밋 중 본인 23커밋 = 77%). 나머지 4인의 기여는 소규모 보조 커밋으로, 자체 스크립트(46개) 대부분은 본인 작성입니다. 배포/빌드 없이 Unity 프로젝트 상태로 공개되어 있습니다.

## 데모

![demo](docs/demo.gif)

- `docs/screenshot-boss.png` — 보스전(2페이즈 전환) 화면
- `docs/screenshot-inventory.png` — 인벤토리/장비 UI

## 담당 범위

majority solo(23/30 커밋 = 77%). 저장소 내 자체 스크립트 46개(에셋스토어 96개 제외, `Assets/Script/` 32개 + `Assets/Scripts/` 14개)를 대부분 본인이 작성했습니다.

- 플레이어 12상태 명시적 FSM(`PlayerState.cs`) — Animator 연동
- 보스 AI 6상태 FSM + NavMeshAgent + 2페이즈 전환(`BossAI.cs`)
- CSV 데이터 테이블 파이프라인 자체 구현(`CSVReader.cs` 정규식 파서 + `DataBase.cs` + `CsvFileWriter.cs`)
- 태그 기반 오브젝트 풀링(`ObjectPool.cs`), 인벤토리·장비·상점·스킬 UI 일체(`UI_Inventory.cs` 등)
- PostProcessing 스택(Vignette/ChromaticAberration) 연동(`GameManager.cs`)

## 핵심 구현

### 1. `PlayerState.cs` — 12상태 명시적 FSM

```csharp
public enum Player_State
{
    Idle, Move, SpellMove, Attack, Casting, Casted, Skill, Teleport, Hit, Down, Back, Die
}

void Update()
{
    if (GameManager.instance.keyLock)
    {
        state = Player_State.Idle;
        ChangeState(Player_State.Idle);
        return;
    }
}
```

### 2. `BossAI.cs` — NavMeshAgent 기반 2페이즈 보스 FSM

6상태 FSM(`Idle/PhaseTwo/Slash/Chop/Strike/Death`)이며, `isHalf` 플래그로 HP 50% 시점에 1회만 `PhaseTwo`로 전환합니다.

```csharp
void UpdateIdle()
{
    if (HP <= 5000 && !isHalf)
    {
        ChangeState(BossAI_State.PhaseTwo);
        isHalf = true;
    }
    coolTime -= Time.deltaTime;
    if (coolTime <= 0)
    {
        int useSkill = Random.Range(2, 5);
        if (useSkill == 2) ChangeState(BossAI_State.Slash);
        else if (useSkill == 3) ChangeState(BossAI_State.Strike);
    }
}
```

### 3. `CSVReader.cs` — 정규식 기반 CSV 파서

따옴표 안의 콤마를 무시하는 정규식(`SPLIT_RE`)으로 필드를 분리해 `int`/`float`로 자동 변환합니다. `Assets/Resources/`의 실제 `ItemTable.csv`/`PlayerInfo.csv`/`SkillTable.csv`를 이 파서로 로드해 `DataBase.cs`의 `Spell`/`PlayerInfo` 클래스에 채웁니다.

```csharp
public static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

public static List<Dictionary<string, object>> Read(string file)
{
    var list = new List<Dictionary<string, object>>();
    TextAsset data = Resources.Load(file) as TextAsset;
    var lines = Regex.Split(data.text, LINE_SPLIT_RE);
    if (lines.Length <= 1) return list;

    var header = Regex.Split(lines[0], SPLIT_RE);
    for (var i = 1; i < lines.Length; i++)
    {
        var values = Regex.Split(lines[i], SPLIT_RE);
        // int/float 자동 변환 후 entry[header[j]]에 저장 (이하 생략)
    }
    return list;
}
```

## 구조

| 경로 | 역할 |
|---|---|
| `Assets/Script/PlayerState.cs`, `PlayerInput.cs`, `PlayerMovement.cs` | 플레이어 FSM/입력/이동 |
| `Assets/Scripts/BossAI.cs`, `BossEvent.cs`, `BossFireBall.cs`, `MonsterAI.cs` | 보스/몬스터 AI 및 패턴 |
| `Assets/Script/CSVReader.cs`, `DataBase.cs`, `CsvFileWriter.cs` | CSV 데이터 테이블 파이프라인 |
| `Assets/Script/GameManager.cs` | 전역 싱글톤(HP/MP/Exp, PostProcessVolume) |
| `Assets/Script/UI_Inventory*.cs`, `UI_Equip*.cs`, `UI_Shop*.cs`, `SkillManager.cs` | 인벤토리/장비/상점/스킬 UI |
| `Assets/Scripts/ObjectPool.cs` | 오브젝트 풀링 |
| `Assets/Resources/*.csv` | 아이템/플레이어/스킬 데이터 테이블 |
| `Assets/Asset/` | 에셋스토어 패키지(InfinityPBR/Hovl Studio 등, 96개 스크립트) |

## 실행 방법

- Unity **2020.3.11f1** (`ProjectSettings/ProjectVersion.txt` 기준). 시작 씬은 `Assets/Scenes/`.
- `Assets/Asset/` 하위 에셋스토어 패키지(캐릭터 모델·애니메이션·VFX 등)가 저장소에 포함되어 있어 별도 구매 없이 클론만으로 열립니다.

## 배운 점 / 지금이라면 다르게 할 것

- `PlayerState`/`BossAI`의 FSM이 하나의 `MonoBehaviour` 안에 `switch-case`로 전부 들어 있어 상태가 늘어날수록 클래스가 비대해집니다. 지금이라면 상태 패턴(State 클래스 분리 + 인터페이스)으로 리팩터링하겠습니다.
- CSV 파서는 직접 구현한 학습 가치가 있었지만, 실무라면 `ScriptableObject` 기반 데이터 테이블로 대체해 타입 안정성을 확보하겠습니다. `GameObject.Find("GM")` 같은 이름 문자열 조회도 싱글톤 참조/의존성 주입으로 바꾸겠습니다.

---

## English summary

A solo-majority (23/30 commits, 77%, verified via GitHub's Contributors API) 3D action-RPG built in Unity 2020.3.11f1 over the longest development span (~3 months, Jul-Sep 2021) among this author's 2021 public repos — the repo's own GitHub description reads "3D 개인 포트폴리오" (3D solo portfolio). Notable systems: a 12-state explicit player FSM (`PlayerState.cs`) driving an `Animator`, a NavMeshAgent-based boss with a 6-state FSM and a single `isHalf`-flagged phase transition at 50% HP (`BossAI.cs`), a hand-rolled CSV data pipeline with a regex-based parser (`CSVReader.cs`) backing real `.csv` data files under `Assets/Resources/`, and a tag-based object pool. Open with Unity 2020.3.11f1; asset-store packages ship in the repo so no external import is required.
