using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Combat {

    public enum EntityType {
        None = 0,
        HP회복,
        HP지정,
        피해,
        효과부여,           // 효과를 부여
        효과회수,           // 효과를 일정 부분 회수
        효과제거,           // 효과를 아예 제거
    }

    [System.Flags]
    public enum EntityProperty {
        None = 0,
        고정수치 = 1 << 0,
        방어무시 = 1 << 1,
        크리티컬 = 1 << 2,
        효과면역 = 1 << 3,
    }

    public sealed partial class DataEntity  {

        private EntityType _type;
        private EntityProperty _property;

        public EntityType Type => _type;
        public EntityProperty Property => _property;

        public void AddProperty(EntityProperty property) => _property |= property;
        public void RemoveProperty(EntityProperty property) => _property &= ~property;

        #region 수치계산

        private int _데이터 = 0;            // 기본 데이터.
        private float _기본배수 = 1f;       // 크리티컬 등에 사용. (기본 피해의 1.5배 식으로)
        private int _증가량 = 0;           // 증가량.     (데미지 10 '증가' 식으로)
        private float _배수 = 1f;          // 최종 배수. (데미지 20% '증가' 식으로)
        private int _추가량 = 0;           // 배수 등에 영향받지 않는 순수 추가량. (데미지 10 '추가' 식으로)
        public void Add기본배수(float value) => _기본배수 += value - 1f;
        public void Add증가량(int value) => _증가량 += value;
        public void Add배수(float value) => _배수 += value - 1f;
        public void Add추가량(int value) => _추가량 += value;
        public void 수치초기화(int 데이터) {
            _데이터 = 데이터;
            _기본배수 = 1f;
            _증가량 = 0;
            _배수 = 1f;
            _추가량 = 0;
        }
        public int 데이터 => Mathf.FloorToInt((_데이터 * _기본배수 + _증가량) * _배수 + _추가량);             // 무조건 버린다.
        #endregion

        #region 효과

        private Effect _효과 = null;
        private bool is효과무효 = false;

        public Effect 효과 => _효과;
        public bool Is효과무효 => is효과무효;

        public void 효과면역() {
            is효과무효 = true;
            AddProperty(EntityProperty.효과면역);
        }
        public void 효과무효() {
            is효과무효 = true;
        }
        public void 효과무효취소() {
            is효과무효 = false;
            RemoveProperty(EntityProperty.효과면역);
        }

        #endregion

        #region 피해

        public void SetResultData_피해(int prev, int next) {
            Apply이전값_HP = prev;
            Apply이후값_HP = next;
        }

        public int Apply이전값_HP { get; private set; }
        public int Apply이후값_HP { get; private set; }

        public int 총피해량 => Apply이전값_HP - Apply이후값_HP;

        #endregion

    }
}
