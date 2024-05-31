
# 3DSurvival
<br/>

### 구현한 필수 기능 <br/>
1. 기본 이동 및 점프 ``Input System`` ``Rigidbody ForceMode``
2. 체력바 UI ``UI``
3. 동적 환경 조사 ``Raycast`` ``UI``
4. 점프대 ``Rigidbody ForceMode``
5. 아이템 데이터 ``ScriptableObject``
6. 아이템 사용 ``Coroutine``

### 선택 기능 <br/>
1. 추가 UI: 스태미나
2. 3인칭 시점
3. 다양한 아이템 구현 - 부스트 버섯

<br/>

# 기능 세부

### 이동 및 점프
``WASD`` 키로 이동, ``Space`` 키로 점프합니다.

### 조사 및 상호작용
카메라 중앙의 포인터로 아이템을 조사하고, ``E`` 키로 아이템과 상호작용합니다.

### 점프대
점프대 위에서 점프하면 슈퍼점프가 가능합니다.

### 스태미나 UI
움직일 때 플레이어 스태미나가 감소합니다.

### 3인칭 시점
``마우스 우클릭``으로 카메라 시점을 변경 가능합니다.

### 아이템
당근 아이템을 사용하면 체력과 포만감이 증가하고, 일정 시간 동안 이동 속도가 느려집니다.
버섯 아이템을 사용하면 체력이 감소하지만, 일정 시간 동안 이동 속도가 빨라집니다.


