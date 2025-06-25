using LittleGardener.Garden;
using UnityEngine;

namespace LittleGardener.Animal
{
    public class AnimalWaitForEatState : AnimalState
    {
        private GardenBed _bed;
        private float _timer = 0;
        private int _waitTrigger = Animator.StringToHash("Wait");

        public AnimalWaitForEatState(AnimalController controller, GardenBed bed) : base(controller) 
        {
            _bed = bed;

            _controller.SetAnimatorTrigger(_waitTrigger);
        }

        public override void Do()
        {
            if (_timer < 2f)
            {
                _timer += Time.deltaTime;
                return;
            }

            if (!_bed.IsFree)
            {

                _bed.RemovePlant();
                _controller.PlayEatAudio();
            }

            _controller.SetState(new AnimalRunState(_controller, _controller.transform));
        }
    }
}