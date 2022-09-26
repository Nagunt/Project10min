using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Data {

    public class DataInt {

        private int _데이터 = 0;            // 기본 데이터.
        private float _기본배수 = 1f;       // 크리티컬 등에 사용. (기본 피해의 1.5배 식으로)
        private int _증가량 = 0;           // 증가량.     (데미지 10 '증가' 식으로)
        private float _배수 = 1f;          // 최종 배수. (데미지 20% '증가' 식으로)
        private int _추가량 = 0;           // 배수 등에 영향받지 않는 순수 추가량. (데미지 10 '추가' 식으로)

        public int 데이터 => Mathf.FloorToInt((_데이터 * _기본배수 + _증가량) * _배수 + _추가량);

        public DataInt(int 초기데이터 = 0) {
            수치초기화(초기데이터);
        }

        public DataInt Add기본배수(float value) {
            _기본배수 += value - 1f;
            return this;
        }
        public DataInt Add증가량(int value) {
            _증가량 += value;
            return this;
        }
        public DataInt Add배수(float value) {
            _배수 += value - 1f;
            return this;
        }
        public DataInt Add추가량(int value) {
            _추가량 += value;
            return this;
        }
        public DataInt 수치초기화(int 데이터) {
            _데이터 = 데이터;
            _기본배수 = 1f;
            _증가량 = 0;
            _배수 = 1f;
            _추가량 = 0;
            return this;
        }
    }

    public class DataFloat {

        private float _데이터 = 0;            // 기본 데이터.
        private float _기본배수 = 1f;       // 크리티컬 등에 사용. (기본 피해의 1.5배 식으로)
        private int _증가량 = 0;           // 증가량.     (데미지 10 '증가' 식으로)
        private float _배수 = 1f;          // 최종 배수. (데미지 20% '증가' 식으로)
        private int _추가량 = 0;           // 배수 등에 영향받지 않는 순수 추가량. (데미지 10 '추가' 식으로)

        public float 데이터 => (_데이터 * _기본배수 + _증가량) * _배수 + _추가량;

        public DataFloat(int 초기데이터 = 0) {
            수치초기화(초기데이터);
        }

        public DataFloat Add기본배수(float value) {
            _기본배수 += value - 1f;
            return this;
        }
        public DataFloat Add증가량(int value) {
            _증가량 += value;
            return this;
        }
        public DataFloat Add배수(float value) {
            _배수 += value - 1f;
            return this;
        }
        public DataFloat Add추가량(int value) {
            _추가량 += value;
            return this;
        }
        public DataFloat 수치초기화(int 데이터) {
            _데이터 = 데이터;
            _기본배수 = 1f;
            _증가량 = 0;
            _배수 = 1f;
            _추가량 = 0;
            return this;
        }
    }

}