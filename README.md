# ThrowAndLive

## 게임 소개
```
- 4곳에서 스폰되는 몬스터들을 제거하여 최대한 오래살아남는 미니게임이다.
- 좌우 버튼을 눌러 달리기 진행 시 진행 방향으로 총알이 발사되며 몬스터를 제거할 수 있다.
- 드래그 앤 드롭으로 몬스터를 날려버릴 수 있다.
- 몬스터에 닿게 되면 게임 오버가 되며, 버틴 시간에 비례한 점수가 나온다.
```

## 코드 기법

* #### 싱글턴 패턴
  - 고유한 MainManager를 통해서만 각 매니저 컴포넌트들을 호출할 수 있다.

* #### 옵저버 패턴
  - Action을 이용하여 각 이벤트 발생 시 각 함수에게 알려준다.

* #### 상태 패턴
  - 플레이어의 상태(정지, 달리기, 점프)에 따른 상태를 구분하여 각 애니메이션을 배치하였다.
  - 원활한 테스트를 위해 키보드와 터치상태를 통합하였다.(키보드와 터치 둘 다 동작 가능)

* #### 오브젝트 풀링
  - 게임에서는 몬스터와 플레이어가 달릴 때 발사되는 총알이 있다. 각 오브젝트들은 기본 10개씩 생성되며, 스택으로 관리한다.

* #### 코루틴
  - 지정 시간마다 몬스터들을 지정 지역에서 스폰한다.
  - 지정 시간마다 달리기 시 해당 방향으로 총알이 발사된다.

## 게임 제작 후 배운 점
```
첫 게임을 통해 구조가 잡힌 상태였어서 게임 만드는 과정이 훨씬 단순화되었다. 두번째 게임 역시 기능에 초첨들 두어 만들었으며,
다양한 기술들을 접목하여 게임을 만드는데에 있어 흥미를 느꼈고 더 다양한 기술을 접목시킨 게임을 만들고 싶었다.
```

* #### 오브젝트 던지기
  - 드래그 앤 드롭으로 오브젝트를 던지게 만들기 위해 드래그 시작 시간, 위치와 드래그 끝나는 시간, 위치를 저장하여 구현하였으나
    드래그로 오브젝트를 드래그하면서 시간이 지난 후 던질 때는 속력 = 거리 / 시간 공식에서 시간이 늘어나 제대로 작동하지 않았다.
    한 프레임동안 마우스가 움직인 속력을 계산하는 방법을 시도하여 프레임마다 시작 시간이 갱신되게 하여 해당 버그를 수정하였다.
    (시간은 한 프레임의 시간이니 Time.deltaTime이 된다.)
    
* #### 미니맵 구현 및 최적화
  - 해당 게임에서는 카메라가 플레이어를 따라다니며 전체 맵을 보여주지 않고 있다. 몬스터가 얼마나 오는지 어디서 오는지를 볼 수 있도록
    미니맵 전용 카메라를 이용하여 구현하였다.
  - 해당 게임에서는 단순한 그래픽으로 미니맵에 그려지는 플레이어와 몬스터들이 그려져도 큰 부하가 없겠지만, 보통의 게임에서의 복잡한
    캐릭터를 두 번 그려지는 일이 발생하므로 부하가 발생할 수 있다. 이를 최적화하기 위해서 각 오브젝트를 대신하는 단순한 형태의
    오브젝트를 생성하여 카메라 쪽으로 배치하였고, 메인 카메라와 미니맵 카메라의 Culling Mask를 통해 필요없는 부분은 나타내지 않도록 하였다.
    
* #### UI 애니메이션
  - 제목 부분에는 색을 이용한 애니메이션을 적용하였고, 게임 시작 화면에서 캐릭터가 정지, 달리기 상태를 구현하여 밋밋하지 않게
    화면을 구성하였다.
    
* #### Update()문과 FixedUpdate()문 분리, n단 점프
  - 물리 엔진의 버그를 최소화하기 위해 키 입력은 Update()문에서 받고 AddForce()를 통해 이동과 점프하는 구간은 FixedUpdate()문에서
    구현하였다. n단 점프를 구현을 하기 위해 노력했지만 프레임 드랍이라던지 한번에 n단 점프가 되는 경우 등 여러 문제가 발생하였다.
  - 정보를 찾아보니 점프와 같은 단발성 커맨드들의 경우 Update()문과 FixedUpdate()문과의 프레임 차이로 인해 제대로 작동하지 않아서
    Update()문에 작성을 해야한다는 것을 알았다.
    
* #### TileMap 구성과 측면 마찰력
  - TileMap 구성하는 법을 배웠으며, Physics Material 2D를 통해 마찰력과 탄성력 조절하는 방법을 터득하였다.
  - 테스트 시 측면에 닿아 캐릭터가 떨어져야하는데 AddForce()를 통해 힘을 계속 받다보니 캐릭터가 벽에 붙어버리는 현상이 발생하였다.
    Platform Effector 2D를 통해 측면 마찰력을 0로 만드는 방법을 터득하였다.
    
* #### 오브젝트 위치 파악(시야각 구현)
  - 해당 게임에서는 몬스터가 플레이어의 위치를 파악하여 움직이도록 설정되어있다. 2D 미니게임이라 시야각의 구현 기능은 들어가지 않았지만,
    해당 과정을 공부하면서 벡터의 내적, 외적을 통해 오브젝트의 앞/뒤/좌/우 혹은 시야각을 통한 오브젝트의 위치 파악하는 방법을 터득하였다.

## 게임 화면
<img width="80%" src="https://user-images.githubusercontent.com/37278829/158119270-ff3c5391-4648-458f-af8e-3ddd33909d78.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/158119276-56b9fe12-97f0-4068-b83d-c4d989c01fa3.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/158119279-1b410b5c-a48b-4c71-9388-f7acba0da7a8.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/158119281-26f32554-957e-4c70-b68c-170013e63b48.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/158119288-67242951-a2cd-48e1-8070-265597b3b72b.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/158119291-d766950f-cd1a-477c-a0f5-521912f0e178.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/158119295-62921f1a-7b13-4ded-921a-c13b6f1a5d0d.png"/>
