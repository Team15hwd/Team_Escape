# 🚀 Fury & Sue : The Last Escape

퓨리와 수쨩, 두 캐릭터의 협력을 통해 연구소를 탈출하는 협력형 플랫포머 게임입니다.  
WASD와 화살표 키를 사용하여 각 캐릭터를 조작하고, 서로의 능력을 활용해 맵을 탈출하세요!

---

## 👽**게임 개요**
- **장르**: 2D 협력형 플랫포머
- **플레이어 방식**: 2인 협력 플레이
- **특징**:
  - **퓨리**: 엑사노바 구역만 지나갈 수 있음
  - **수쨩**: 산소 구역만 지나갈 수 있음
  - 캐릭터의 이동과 점프를 활용한 **퍼즐 및 탈출 기믹**

---

## **스토리**
> **이 수 우주 연구소, 2045년 2월 25일.**  
> 정체불명의 침입자가 연구소를 점령했다.  
> 생존 확률 0.4%, 탈출 가능성 희박.  
> 이 둘은 연구소를 탈출할 수 있을까...?

## **맵 구조**
1. **수용소** - "퓨리", 연구원 "수쨩"의 도움으로 문을 연다.
2. **연구소 내부** - 침입자들을 피해 연구소 탈출을 시도한다.
3. **우주 격납고** - 자동 방어 시스템을 무력화하고 탈출선에 탑승한다.

## **엔딩**
- **해피 엔딩**: ???
- **배드 엔딩**: ???

---

## 🎮 **조작 방법**
| 조작 키 | 플레이어1 (퓨리) | 플레이어2 (수쨩) |
|---------|-----------------|-----------------|
| 이동 | A, D | ◀️, ▶️ |
| 점프 | W | ⬆️ |
| 상호작용 | S | ⬇️ |

---

## ⚙ **시스템 구조**
/Scripts

  ├── GamePlay
  
    │   ├── StageInfo.cs 
  
  ├── Objects
  
    │   ├── DoorController.cs
  
  ├── Physics
  
    │   ├── ButtonFlag.cs  

    │   ├── Chain.cs     
  
    │   ├── CharacterController2D.cs  
  
    │   ├── LadderPlatform.cs 
  
    │   ├── MovingPlatform.cs 
  
    │   ├── Potal.cs 
  
    │   ├── Pulley.cs  
  
    │   ├── PulleyManager.cs 
    
    │   ├── TriggerCollision.cs  
    
    │   ├── TriggerController.cs 
  
  ├── Player
  
    │   ├── Player.cs  
  
  ├── Scene
  
    │   ├── SceneLoader.cs  
  
    │   ├── SceneLoadManager.cs  
  
  ├── Sound
  
    │   ├── SoundBuilder.cs
  
    │   ├── SoundData.cs  
  
    │   ├── SoundEmitter.cs  
  
    │   ├── SoundManager.cs  
  
    │   ├── SoundPlay.cs  
  
    │   ├── SoundSetting.cs  
  
  ├── UI
  
    │   ├── ClearPanelUI.cs  
  
    │   ├── FailPanelUI.cs 
  
    │   ├── GamePlayUI.cs 
  
    │   ├── RandomSprite.cs  
  
  ├── Utils
  
    │   ├── Bootstrapper.cs  
  
    │   ├── VideoScene.cs

---
## 🛠️ **설치 및 실행 방법**

1. 저장소 클론하기

  https://github.com/Team15hwd/Team_Escape

2. Unity에서 프로젝트를 열기 (권장 버전: 2022.3.17f1)

3. Assets/Scenes/StartScene.unity를 실행하여 게임 시작!

---

## 🙋‍♀️ **팀원**
**김주연** : 팀장, StoryScene, UI, TutorialMap & Map1
**순현빈** : 시스템 설계 및 스켈레톤 코드 작성, 물리 엔진 구현
**이 수** : SceneManager, SceneLoader, StoryScene, UI, CSS
**이동현** : Scene 전환, SettingSlider, CSS 애니메이션, Map3
**최종민** : 승강기 기능, Map2
