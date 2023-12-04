# 요약        
"simple Optimized Scroll View"는 여러개의 슬롯을 최적화된 성능에서 사용할 수 있도록 기존의 스크롤 뷰를 약간 수정했습니다.<br/><br/>
이 툴은 여러 프로젝트에 쉽게 적용하기 위해 최대한 필수적인 부분만 구현하고자 했습니다.<br/>

# 추천
- 유니티의 스크롤 뷰와 그리드 레이아웃 그룹을 약간의 성능 향상과 함께 쉽게 대체하길 원하는 경우
- 개인 프로젝트에 맞춤형으로 수정하기 위해 기본적인 기능만 있는 코드를 원하는 경우
- 만약 슬롯의 수가 굉장히 많고 성능에 부하가 크다면, Recycling Scroll View를 사용하는 것을 추천합니다.

# 성능
"PerformanceTest.md"에 더 자세한 정보가 있습니다
### 1. 유니티 기본 스크롤 뷰
![image](https://github.com/Giant-Tori/Simple-Optimized-Scroll-View/assets/149294349/359cab04-48c7-49d3-872a-6fecaae41a1d)

### 2. Optimized Scroll View
![image](https://github.com/Giant-Tori/Simple-Optimized-Scroll-View/assets/149294349/70182318-a353-4715-8286-98f97e622eb0)

# 사용법
#### 1. Create > UI > Optimized Scroll View 에서 생성
![image](https://github.com/Giant-Tori/Simple-Optimized-Scroll-View/assets/149294349/ab486606-90ac-46d0-a10f-28442177704b)
#### 2. 인스펙터에서 슬롯 프리팹 할당
![image](https://github.com/Giant-Tori/Simple-Optimized-Scroll-View/assets/149294349/dd50bf9f-ab0b-462a-81da-703c9ea027ee)
#### 3. Optimized Scroll view의 Refresh() 호출

# 추가 기능
- content size fittter또한 코드로 구현되어 있습니다.(Refresh() 호출하면 됨)
- Grid Lay Out 기능 또한 구현 되어 있습니다.
