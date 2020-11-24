//참고 : https://gmlwjd9405.github.io/2018/07/08/observer-pattern.html

using System;
using System.Collections.Generic;
using System.Linq;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            ScoreRecord scoreRecord = new ScoreRecord();

            // 3개까지의 점수만 출력함
            DataSheetView dataSheetView = new DataSheetView(scoreRecord, 3);
            // 최대값, 최소값만 출력함
            MinMaxView minMaxView = new MinMaxView(scoreRecord);

            // 각 통보 대상 클래스를 Observer로 추가
            scoreRecord.Attach(dataSheetView);
            scoreRecord.Attach(minMaxView);

            // 10 20 30 40 50을 추가
            for (int index = 1; index <= 5; index++)
            {
                int score = index * 10;
                Console.WriteLine("Adding " + score);
                // 추가할 때마다 최대 3개의 점수 목록과 최대/최소값이 출력됨
                scoreRecord.AddScore(score);
            }
        }
    }

    // 추상화된 통보 대상
    public interface IObserver
    {
        // 데이터 변경을 통보했을 때 처리하는 메서드
        public abstract void Update();
    }

    // 추상화된 변경 관심 대상 데이터
    // 즉, 데이터에 공통적으로 들어가야하는 메서드들 -> 일반화
    public abstract class Subject
    {
        // 추상화된 통보 대상 목록 (즉, 출력 형태에 대한 Observer)
        private List<IObserver> observers = new List<IObserver>();

        // 통보 대상(Observer) 추가
        public void Attach(IObserver observer) { observers.Add(observer); }
        // 통보 대상(Observer) 제거
        public void Detach(IObserver observer) { observers.Remove(observer); }
        // 각 통보 대상(Observer)에 변경을 통보.
        // (List<IObserver>객체들의 Update를 호출)
        public void NotifyObservers()
        {
            foreach (IObserver o in observers)
            {
                o.Update();
            }
        }
    }

    // 구체적인 변경 감시 대상 데이터
    // 출력형태 2개를 가질 때
    public class ScoreRecord : Subject
    {
        private List<int> scores = new List<int>(); // 정수를 저장함
        // 새로운 점수를 추가 (상태 변경)
        public void AddScore(int score)
        {
            scores.Add(score);  // scores 목록에 주어진 점수를 추가함
            NotifyObservers();  // scores가 변경됨을 각 통보 대상(Observer)에게 통보함
        }
        public List<int> GetScoreRecord() { return scores; }
    }

    // 통보 대상 클래스 (Update 메서드 구현)
    // 1. 출력형태: 목록 형태로 출력하는 클래스
    public class DataSheetView : IObserver
    {
        private ScoreRecord scoreRecord;
        private int viewCount;

        public DataSheetView(ScoreRecord scoreRecord, int viewCount)
        {
            this.scoreRecord = scoreRecord;
            this.viewCount = viewCount;
        }

        // 점수의 변경을 통보받음
        public void Update()
        {
            List<int> record = scoreRecord.GetScoreRecord(); // 점수를 조회함
            DisplayScores(record, viewCount); // 조회된 점수를 viewCount 만큼만 출력함
        }

        private void DisplayScores(List<int> record, int viewCount)
        {
            Console.WriteLine("List of " + viewCount + " entries: ");
            for (int i = 0; i < viewCount && i < record.Count; i++)
            {
                Console.WriteLine(record[i] + " ");
            }
            Console.WriteLine();
        }
    }

    // 통보 대상 클래스 (Update 메서드 구현)
    // 2. 출력형태: 최대값 최소값만 출력하는 클래스
    public class MinMaxView : IObserver
    {
        private ScoreRecord scoreRecord;
        // getScoreRecord()를 호출하기 위해 ScoreRecord 객체를 인자로 받음
        public MinMaxView(ScoreRecord scoreRecord)
        {
            this.scoreRecord = scoreRecord;
        }
        // 점수의 변경을 통보받음
        public void Update()
        {
            List<int> record = scoreRecord.GetScoreRecord(); // 점수를 조회함
            DisplayScores(record); // 최소값과 최대값을 출력함
        }
        // 최소값과 최대값을 출력함
        private void DisplayScores(List<int> record)
        {
            int min = record.Min();
            int max = record.Max();
            Console.WriteLine("Min: " + min + ", Max: " + max);
        }
    }
}
