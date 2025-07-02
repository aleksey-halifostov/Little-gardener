using UnityEngine;
using System.Collections;
using LittleGardener.Garden;

namespace LittleGardener.Animal
{
    public class AnimalWaitForEatState : AnimalState
    {
        private GardenBed _bed;
        private int _wait = 2;
        private Coroutine _coroutine;
        private int _waitTrigger = Animator.StringToHash("Wait");

        private IEnumerator WaitAndEat()
        {
            yield return new WaitForSeconds(_wait);

            if (!_bed.IsFree)
            {
                _bed.RemovePlant();
                _controller.PlayEatAudio();
            }

            _controller.SetState(new AnimalRunState(_controller, _controller.transform));
        }

        public AnimalWaitForEatState(AnimalController controller, GardenBed bed) : base(controller) 
        {
            if (bed == null)
                throw new System.ArgumentNullException(nameof(bed));

            _bed = bed;
        }

        public override void Enter()
        {
            _controller.SetAnimatorTrigger(_waitTrigger);
            _coroutine = _controller.StartCoroutine(WaitAndEat());
        }

        public override void Do() { }

        public override void Exit() 
        {
            if (_coroutine == null)
                return;

            _controller.StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}